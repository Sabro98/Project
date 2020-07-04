using HowWear.Model;
using HowWear.ViewModel.Commands;
using HowWear.ViewModel.Converter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup.Localizer;
using System.Windows.Media.TextFormatting;
using System.Xml;

namespace HowWear.ViewModel
{
    class MainVM
    {
        public CurrentWeatherInfo Weather { get; set; }
        public ForecastWeatherInfo FWeather { get; set; }
        public ClothesSetInfo ClothesInfo { get; set; }
        public MainBtnCommand MainCommand { get; set; }
        public DeleteColCombiCommand DeletecolCommand { get; set; }
        public int Cnt { get; set; }
        public int ClothesCnt { get; set; }

        public MainVM()
        {
            Weather = new CurrentWeatherInfo();
            FWeather = new ForecastWeatherInfo();
            ClothesInfo = new ClothesSetInfo();
            MainCommand = new MainBtnCommand(this);
            DeletecolCommand = new DeleteColCombiCommand(this);
            Cnt = 0;
            ClothesCnt = 0;
            
            ClothesInfo.ValueDic = setDictValue();

            //옷들 사이의 연결관계 만들기
            ClothesInfo.OuterBytop = outerBytopSet();
            ClothesInfo.TopBytop = topBytopSet();
            ClothesInfo.TopBybottom = topBybottomSet();
            ClothesInfo.TopSolo = topSoloSet();
            ClothesInfo.TopBybottomCol = topBybottomColSet();
            ClothesInfo.OuterByTopCol = outerBytopColSet();
            ClothesInfo.OuterByBottom = OuterByBottomSet();
            ClothesInfo.TopByskirt = topByskirtSet();
            ClothesInfo.OuterBySkirt = outerByskirtSet();
            ClothesInfo.TopBySkirtCol = topByskirtColSet();

            RenewFiles();
            GetWeather();
            GetFWeather();
            SetClothes();
        }

        //유효한 도시인지 Check
        public static bool TestAPI(string location)
        {
            var tmp = CurrentWeatherAPI.GetWeatherInformation(location);
            if (tmp.Name != null) return true;
            return false;
        }
        public void GetWeather()
        {
            StreamReader reader = new StreamReader("text/location.txt");
            string location = reader.ReadLine();
            reader.Close();

            var weather = CurrentWeatherAPI.GetWeatherInformation(location);
            Weather.Name = weather.Name;
            Weather.Main = weather.Main;
            Weather.Wind = weather.Wind;
            Weather.Weather = weather.Weather;
            Weather.Feeling = (int)(13.12 + 0.6215 * Weather.Main.Temp - 11.37 * Math.Pow(Weather.Wind.Speed*3.6, 0.16) + 0.3965 * Math.Pow(Weather.Wind.Speed*3.6, 0.16) * Weather.Main.Temp);
            Weather.Condition = weather.Weather[0].Main;
        }

        public void SetInfo(int cnt)
        {
            FWeather.FirstInfo = FWeather.list[cnt];
            FWeather.FirstInfo.Time = GetTime(cnt);
            FWeather.FirstInfo.FFeeling = (int)(13.12 + 0.6215 * FWeather.FirstInfo.Main.Temp - 11.37 * Math.Pow(FWeather.FirstInfo.Wind.Speed * 3.6, 0.16) + 0.3965 *
                Math.Pow(FWeather.FirstInfo.Wind.Speed * 3.6, 0.16) * FWeather.FirstInfo.Main.Temp);
            FWeather.FirstInfo.ClothesProp = RepresentClothes(1, FWeather.FirstInfo.FFeeling);

            FWeather.SecondInfo = FWeather.list[cnt + 1];
            FWeather.SecondInfo.Time = GetTime(cnt + 1);
            FWeather.SecondInfo.FFeeling = (int)(13.12 + 0.6215 * FWeather.SecondInfo.Main.Temp - 11.37 * Math.Pow(FWeather.SecondInfo.Wind.Speed * 3.6, 0.16) + 0.3965 *
                Math.Pow(FWeather.SecondInfo.Wind.Speed * 3.6, 0.16) * FWeather.SecondInfo.Main.Temp);
            FWeather.SecondInfo.ClothesProp = RepresentClothes(2, FWeather.SecondInfo.FFeeling);

            FWeather.ThirdInfo = FWeather.list[cnt + 2];
            FWeather.ThirdInfo.Time = GetTime(cnt + 2);
            FWeather.ThirdInfo.FFeeling = (int)(13.12 + 0.6215 * FWeather.ThirdInfo.Main.Temp - 11.37 * Math.Pow(FWeather.ThirdInfo.Wind.Speed * 3.6, 0.16) + 0.3965 *
                Math.Pow(FWeather.ThirdInfo.Wind.Speed * 3.6, 0.16) * FWeather.ThirdInfo.Main.Temp);
            FWeather.ThirdInfo.ClothesProp = RepresentClothes(3, FWeather.ThirdInfo.FFeeling);
        }

        public string GetTime(int cnt)
        {
            char[] tmpChar = FWeather.list[cnt].dt_txt.ToCharArray();
            string tmp = tmpChar[11].ToString() + tmpChar[12].ToString();
            int tmp_int = Int32.Parse(tmp);
            tmp_int += 9;
            if (tmp_int > 24) tmp_int -= 24;
            return tmp_int.ToString() + "시";
        }
        public void GetFWeather()
        {
            StreamReader reader = new StreamReader("text/location.txt");
            string location = reader.ReadLine();
            reader.Close();

            Cnt = 0;
            var weather = ForecastWeatherAPI.GetWeatherInformation(location);
            FWeather.list = weather.list;
            SetInfo(Cnt);
        }


        private List<Pair<string, string>> outers;
        private List<Pair<string, string>> tops;  
        private List<Pair<string, string>> bottoms;
        private List<Pair<string, string>> skirts;
        private List<string> FavCol; private List<string> FavClothes;
        private string ch;
        private string gender;
        public List<Pair<List<string>, int>> ClothesSet { get; set; }
        public List<List<string>> HateCombination { get; set; }

        public void RenewFiles()
        {

            outers = new List<Pair<string, string>>();
            tops = new List<Pair<string, string>>();
            bottoms = new List<Pair<string, string>>();
            skirts = new List<Pair<string, string>>();
            FavCol = new List<string>();
            FavClothes = new List<string>();
            HateCombination = new List<List<string>>();

            //파일을 읽어서 추가
            ReadFile(outers, $"Text/outer.txt");
            ReadFile(tops, $"Text/top.txt");
            ReadFile(bottoms, $"Text/bottom.txt");
            ReadFile(skirts, $"Text/skirt.txt");
            ReadFile(FavCol, $"Text/favoriteColor.txt");
            ReadFile(FavClothes, $"Text/favoriteClothes.txt");
            using (StreamReader reader = new StreamReader("Text/characteristic.txt"))
            {
                ch = reader.ReadLine();
            }
            using (StreamReader reader = new StreamReader("Text/gender.txt"))
            {
                gender = reader.ReadLine();
            }
            using (StreamReader reader = new StreamReader("Text/hateCombination.txt"))
            {
                while(reader.Peek() > 0)
                {
                    List<string> tmp = new List<string>();
                    string[] values = reader.ReadLine().Split(',');
                    foreach(var str in values)
                    {
                        if (str.Equals(" ")) tmp.Add(null);
                        else tmp.Add(str.Remove(str.Length-1, 1));
                    }
                    HateCombination.Add(tmp);
                }
            }
        }
        public void SetClothes()
        {
            ClothesCnt = 0;
            ClothesSet = new List<Pair<List<string>, int>>();

            //아우터 + 상의1 + 상의2
            MatchingOuterTopDouble(ClothesSet);
            //아우터 + 상의
            MatchingOuterTopOnly(ClothesSet);
            //상의1 + 상의2
            MatchingDoubleTop(ClothesSet);
            //상의
            MatchTopOnly(ClothesSet);

            if (gender.Equals("female"))
            {
                //아우터 + 상의1 + 상의2
                MatchingOuterTopDoubleS(ClothesSet);
                //아우터 + 상의
                MatchingOuterTopOnlyS(ClothesSet);
                //상의1 + 상의2
                MatchingDoubleTopS(ClothesSet);
                //상의
                MatchTopOnlyS(ClothesSet);
            }

            ClothesSet = SortClothes(ClothesSet);
            MakeLook(ClothesCnt);
        }


        private string RepresentClothes(int cnt, int temp)
        {
            string ret = null;
            List<Pair<List<string>, int>> tmpClothesSet = new List<Pair<List<string>, int>>();

            MatchingOuterTopDouble(tmpClothesSet, cnt);
            //아우터 + 상의
            MatchingOuterTopOnly(tmpClothesSet, cnt);
            //상의1 + 상의2
            MatchingDoubleTop(tmpClothesSet, cnt);
            //상의
            MatchTopOnly(tmpClothesSet, cnt);

            if (gender.Equals("female"))
            {

                //아우터 + 상의1 + 상의2
                MatchingOuterTopDoubleS(tmpClothesSet);
                //아우터 + 상의
                MatchingOuterTopOnlyS(tmpClothesSet);
                //상의1 + 상의2
                MatchingDoubleTopS(tmpClothesSet);
                //상의
                MatchTopOnlyS(tmpClothesSet);
            }
            tmpClothesSet = SortClothes(tmpClothesSet);

            if (tmpClothesSet.Count == 0) return "no";
            if(temp < 14 && tmpClothesSet[0].First[0] != null)
            {
                ret = tmpClothesSet[0].First[0];
            }
            else
            {
                ret = tmpClothesSet[0].First[1];
            }
            return ret;
        }

        private List<Pair<List<string>, int>> SortClothes(List<Pair<List<string>, int>> ClothesSet)
        {
            int temp = Weather.Feeling;
            bool flag = true;
            if (temp <= 5)
            {
                flag = true;
            }
            else if ((6 <= temp && temp <= 9))
            {
                flag = true;
            }
            else if ((10 <= temp && temp <= 11))
            {
                flag = true;
            }
            else if (12 <= temp && temp <= 16)
            {
                if (ch.Equals("cold"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else if (17 <= temp && temp <= 19)
            {
                if (ch.Equals("cold"))
                {
                    flag = true;
                }
                else
                {

                    flag = false;
                }
            }
            else if (20 <= temp && temp <= 22)
            {

                flag = false;
            }
            else if (23 <= temp && temp <= 26)
            {
                flag = false;
            }
            else if (27 <= temp)
            {
                flag = false;
            }
            if (flag)
            {
                foreach(var list in ClothesSet)
                {
                    string[] topWords = list.First[1].Split(' ');
                    if (FavCol.Contains(topWords[0])) list.Second+=100;
                    if (FavClothes.Contains(topWords[1])) list.Second+=100;
                }
                ClothesSet = ClothesSet.OrderByDescending(x => x.Second).ToList();
            }
            else
            {
                foreach (var list in ClothesSet)
                {
                    string[] topWords = list.First[1].Split(' ');
                    if (FavCol.Contains(topWords[0])) list.Second-=100;
                    if (FavClothes.Contains(topWords[1])) list.Second-=100;
                }
                ClothesSet = ClothesSet.OrderBy(x => x.Second).ToList();
            }
            return ClothesSet;
        }

        public void MakeLook(int num)
        {
            if(ClothesSet.Count != 0)
            {
                ClothesInfo.Outer = ClothesSet[num].First[0];
                ClothesInfo.Top1 = ClothesSet[num].First[1];
                ClothesInfo.Top2 = ClothesSet[num].First[2];
                ClothesInfo.Bottom = ClothesSet[num].First[3];
            }
            else
            {
                ClothesInfo.Outer = null;
                ClothesInfo.Top1 = "종류 부족";
                ClothesInfo.Top2 = null;
                ClothesInfo.Bottom = null;
            }
        }

        private void ReadFile(List<Pair<string, string>> arr, string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (reader.Peek() >= 0)
                {
                    string[] tmp = reader.ReadLine().Split(' ');
                    if (tmp[0].Equals("")) break;
                    arr.Add(new Pair<string, string>(tmp[0], tmp[1]));
                }
            }
        }
        private void ReadFile(List<string> arr, string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (reader.Peek() >= 0)
                {
                    string tmp = reader.ReadLine();
                    if (tmp[0].Equals("")) break;
                    arr.Add(tmp);
                }
            }
        }

        private List<Pair<Pair<string, string>, Pair<string, string>>> OuterInnerList()
        {
            List<Pair<Pair<string, string>, Pair<string, string>>> ret = new List<Pair<Pair<string, string>, Pair<string, string>>>();
            foreach(var topOuter in tops)
            {
                foreach(var topInner in tops)
                {
                    if (ClothesInfo.TopBytop[topOuter.Second].Contains(topInner.Second) &&
                        (topInner.First.Equals("white") || topInner.First.Equals("black")) &&
                        topOuter.First != topInner.First)
                    {
                        ret.Add(new Pair<Pair<string, string>, Pair<string, string>>(topOuter, topInner));
                    }
                }
            }
            return ret;
        }

        private bool IsTopBottomMatch(Pair<string, string> top, Pair<string, string> bottom)
        {
            bool flag;
            if (ClothesInfo.TopBybottomCol[top.First].Contains(bottom.First) &&
                    ClothesInfo.TopBybottom[top.Second].Contains(bottom.Second))
            {
                flag = true;
            }
            else flag = false;
            return flag;
        }
        private bool IsTopSkirtMatch(Pair<string, string> top, Pair<string, string> skirt)
        {
            bool flag;
            if (ClothesInfo.TopBySkirtCol[top.First].Contains(skirt.First) &&
                    ClothesInfo.TopByskirt[top.Second].Contains(skirt.Second))
            {
                flag = true;
            }
            else flag = false;
            return flag;
        }
        private bool IsOuterBottomMatch(Pair<string, string> outer, Pair<string, string> bottom)
        {
            bool flag;
            if (ClothesInfo.TopBybottomCol[outer.First].Contains(bottom.First) &&
                    ClothesInfo.OuterByBottom[outer.Second].Contains(bottom.Second))
            {
                flag = true;
            }
            else flag = false;
            return flag;
        }
        private bool IsOuterSkirtMatch(Pair<string, string> outer, Pair<string, string> skirt)
        {
            bool flag;
            if (ClothesInfo.TopBySkirtCol[outer.First].Contains(skirt.First) &&
                    ClothesInfo.OuterBySkirt[outer.Second].Contains(skirt.Second))
            {
                flag = true;
            }
            else flag = false;
            return flag;
        }

        private void MatchingOuterTopDouble(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            var topSet = OuterInnerList();
            foreach(var outer in outers)
            {
                foreach(var top in topSet)
                {
                    if(ClothesInfo.OuterByTopCol[outer.First].Contains(top.First.First) &&
                        ClothesInfo.OuterBytop[outer.Second].Contains(top.First.Second))
                    {
                        foreach(var bottom in bottoms)
                        {
                            if (IsOuterBottomMatch(outer, bottom))
                            {
                                var values = ClothesInfo.ValueDic;
                                int sum = values[outer.Second] + values[top.First.Second] + values[top.Second.Second] + values[bottom.Second];
                                if (!IsHate(outer, top.First, top.Second, bottom) && MatchingClothes(sum, cnt))
                                {
                                    List<string> tmp = new List<string>() 
                                    {
                                        $"{outer.First} {outer.Second}", $"{top.First.First} {top.First.Second}",
                                        $"{top.Second.First} {top.Second.Second}", $"{bottom.First} {bottom.Second}" 
                                    };
                                    ClothesSet.Add(new Pair<List<string>, int> (tmp, sum));
                                }
                            }
                            
                        }
                    }
                }
            }
        }

        private void MatchingOuterTopOnly(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            foreach(var outer in outers)
            {
                foreach(var top in tops)
                {
                    if(ClothesInfo.OuterByTopCol[outer.First].Contains(top.First) &&
                        ClothesInfo.OuterBytop[outer.Second].Contains(top.Second))
                    {
                        foreach(var bottom in bottoms)
                        {
                            if (ClothesInfo.TopSolo.Contains(top.Second) &&
                                IsOuterBottomMatch(outer, bottom))
                            {
                                var values = ClothesInfo.ValueDic;
                                int sum = values[outer.Second] + values[top.Second] + values[bottom.Second];
                                if (!IsHate(outer, top, null, bottom) && MatchingClothes(sum, cnt))
                                {
                                    var tmp = new List<string>() 
                                    {
                                        $"{outer.First} {outer.Second}", $"{top.First} {top.Second}",
                                             null, $"{bottom.First} {bottom.Second}" 
                                    };
                                    ClothesSet.Add(new Pair<List<string>, int>(tmp, sum));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MatchingDoubleTop(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            var topSet = OuterInnerList();
            foreach(var top in topSet)
            {
                foreach(var bottom in bottoms)
                {
                    if (IsTopBottomMatch(top.First, bottom))
                    {
                        var values = ClothesInfo.ValueDic;
                        int sum = values[top.First.Second] + values[top.Second.Second] + values[bottom.Second];
                        if (!IsHate(null, top.First, top.Second, bottom) && MatchingClothes(sum, cnt))
                        {
                            var tmp = new List<string>() 
                            {
                                null, $"{top.First.First} {top.First.Second}",
                                 $"{top.Second.First} {top.Second.Second}", $"{bottom.First} {bottom.Second}" 
                            };
                            ClothesSet.Add(new Pair<List<string>, int>(tmp, sum));
                        }
                    }
                }
            }
        }

        private void MatchTopOnly(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            foreach(var top in tops)
            { 
                foreach (var bottom in bottoms)
                {
                    if (ClothesInfo.TopSolo.Contains(top.Second) && IsTopBottomMatch(top, bottom))
                    {
                        int sum = ClothesInfo.ValueDic[top.Second] + ClothesInfo.ValueDic[bottom.Second];
                        if(!IsHate(null, top, null, bottom) && MatchingClothes(sum, cnt))
                        {
                            var tmp = new List<string>() 
                            {
                                null, $"{top.First} {top.Second}",
                                null, $"{bottom.First} {bottom.Second}" 
                            };
                            ClothesSet.Add(new Pair<List<string>, int>(tmp, sum));
                        }
                    }
                }
            }
        }

        private void MatchingOuterTopDoubleS(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            var topSet = OuterInnerList();
            foreach (var outer in outers)
            {
                foreach (var top in topSet)
                {
                    if (ClothesInfo.OuterByTopCol[outer.First].Contains(top.First.First) &&
                        ClothesInfo.OuterBytop[outer.Second].Contains(top.First.Second))
                    {
                        foreach (var skirt in skirts)
                        {
                            if (IsOuterSkirtMatch(outer, skirt))
                            {
                                var values = ClothesInfo.ValueDic;
                                int sum = values[outer.Second] + values[top.First.Second] + values[top.Second.Second] + values[skirt.Second];
                                if (!IsHate(outer, top.First, top.Second, skirt) && MatchingClothes(sum, cnt))
                                {
                                    List<string> tmp = new List<string>()
                                    {
                                        $"{outer.First} {outer.Second}", $"{top.First.First} {top.First.Second}",
                                        $"{top.Second.First} {top.Second.Second}", $"{skirt.First} {skirt.Second}"
                                    };
                                    ClothesSet.Add(new Pair<List<string>, int>(tmp, sum));
                                }
                            }

                        }
                    }
                }
            }
        }

        private void MatchingOuterTopOnlyS(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            foreach (var outer in outers)
            {
                foreach (var top in tops)
                {
                    if (ClothesInfo.OuterByTopCol[outer.First].Contains(top.First) &&
                        ClothesInfo.OuterBytop[outer.Second].Contains(top.Second))
                    {
                        foreach (var skirt in skirts)
                        {
                            if (ClothesInfo.TopSolo.Contains(top.Second) &&
                                IsOuterSkirtMatch(outer, skirt))
                            {
                                var values = ClothesInfo.ValueDic;
                                int sum = values[outer.Second] + values[top.Second] + values[skirt.Second];
                                if (!IsHate(outer, top, null, skirt) && MatchingClothes(sum, cnt))
                                {
                                    var tmp = new List<string>()
                                    {
                                        $"{outer.First} {outer.Second}", $"{top.First} {top.Second}",
                                             null, $"{skirt.First} {skirt.Second}"
                                    };
                                    ClothesSet.Add(new Pair<List<string>, int>(tmp, sum));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MatchingDoubleTopS(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            var topSet = OuterInnerList();
            foreach (var top in topSet)
            {
                foreach (var skirt in skirts)
                {
                    if (IsTopSkirtMatch(top.First, skirt))
                    {
                        var values = ClothesInfo.ValueDic;
                        int sum = values[top.First.Second] + values[top.Second.Second] + values[skirt.Second];
                        if (!IsHate(null, top.First, top.Second, skirt) && MatchingClothes(sum, cnt))
                        {
                            var tmp = new List<string>()
                            {
                                null, $"{top.First.First} {top.First.Second}",
                                 $"{top.Second.First} {top.Second.Second}", $"{skirt.First} {skirt.Second}"
                            };
                            ClothesSet.Add(new Pair<List<string>, int>(tmp, sum));
                        }
                    }
                }
            }
        }

        private void MatchTopOnlyS(List<Pair<List<string>, int>> ClothesSet, int cnt = -1)
        {
            foreach (var top in tops)
            {
                foreach (var skirt in skirts)
                {
                    if (ClothesInfo.TopSolo.Contains(top.Second) && IsTopSkirtMatch(top, skirt))
                    {
                        int sum = ClothesInfo.ValueDic[top.Second] + ClothesInfo.ValueDic[skirt.Second];
                        if (!IsHate(null, top, null, skirt) && MatchingClothes(sum, cnt))
                        {
                            var tmp = new List<string>()
                            {
                                null, $"{top.First} {top.Second}",
                                null, $"{skirt.First} {skirt.Second}"
                            };
                            ClothesSet.Add(new Pair<List<string>, int>(tmp, sum));
                        }
                    }
                }
            }
        }

        private bool IsHate(Pair<string, string> outer, Pair<string, string> top1, Pair<string, string> top2, Pair<string, string> bottom)
        {
            bool ret = false;
            List<string> clothes = new List<string>();

            if (outer == null) clothes.Add(null);
            else clothes.Add($"{outer.First} {outer.Second}");

            if (top1 == null) clothes.Add(null);
            else clothes.Add($"{top1.First} {top1.Second}");

            if (top2 == null) clothes.Add(null);
            else clothes.Add($"{top2.First} {top2.Second}");

            if (bottom == null) clothes.Add(null);
            else clothes.Add($"{bottom.First} {bottom.Second}");

            foreach (var list in HateCombination)
            {
                int cnt = 0;
                for (int i=0; i<4; i++)
                {
                    if (clothes[i] == null && list[i] == null) cnt++;
                    else if (clothes[i] == null || list[i] == null) continue;
                    else if (clothes[i].Equals(list[i])) cnt++;
                }
                if(cnt == 4)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }
        private bool MatchingClothes(int sum, int cnt)
        {
            int temp = 0;
            if(cnt != -1)
            {
                if(cnt == 1) temp = FWeather.FirstInfo.FFeeling;
                else if(cnt == 2) temp = FWeather.SecondInfo.FFeeling;
                else if(cnt == 3) temp = FWeather.ThirdInfo.FFeeling;
            }
            else
            {
                temp = Weather.Feeling;
            }
            string ch;
            using(StreamReader reader = new StreamReader("Text/characteristic.txt")){
                ch = reader.ReadLine();
            }
            bool flag = false;
            if (temp <= 5 && sum >= 60) flag = true;
            else if ((6 <= temp && temp <= 9) && (53 <= sum && sum <= 59)) flag = true;
            else if ((10 <= temp && temp <= 11) && (43 <= sum && sum <= 52)) flag = true;
            else if (12 <= temp && temp <= 16)
            {
                if (ch.Equals("cold"))
                {
                    if (36 <= sum && sum <= 42) flag = true;
                }
                else
                {
                    if (32 <= sum && sum <= 36) flag = true;
                }
            }
            else if (17 <= temp && temp <= 19)
            {
                if (ch.Equals("cold"))
                {
                    if (26 <= sum && sum <= 30) flag = true;
                }
                else
                {
                    if (17 <= sum && sum <= 26) flag = true;
                }
            }
            else if (20 <= temp && temp <= 22)
            {
                if (ch.Equals("hot"))
                {
                    if (8 <= sum && sum <= 13) flag = true;
                }
                else
                {
                    if (15 <= sum && sum <= 26) flag = true;
                }
            }
            else if (23 <= temp && temp <= 26)
            {
                if (ch.Equals("hot"))
                {
                    if (sum <= 6) flag = true;
                }
                else
                {
                    if (8 <= sum && sum <= 13) flag = true;
                }
            }
            else if (27 <= temp && sum <= 6) flag = true;
            return flag;
        }

        //Dictionary 정의
        private Dictionary<string, int> setDictValue()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>
            {
                //아우터
                { "long-padding", 36 },
                { "short-padding", 33 },
                { "long-coat", 23 },
                { "short-coat", 22 },
                { "vest-padding", 20 },
                { "cardigan", 19 },
                { "fleece", 17 },
                { "zip_up_hoodie", 17 },
                { "jacket", 15 },

                //상의
                { "knit", 21 },
                { "hood-T", 19 },
                { "mantoman", 18 },
                { "vest", 16 },
                { "turtleneck", 19 },
                { "shirt", 9 },
                { "long-T", 6 },
                { "short-shirt", 3 },
                { "short-T", 3 },

                //하의
                { "jean", 10 },
                { "slacks", 8 },
                { "jogger", 7 },
                { "casual-jean", 5 },
                { "short-pants", 3 },

                //스커트
                { "long-flared_skirt", 8 },
                { "long-H_skirt", 8 },
                { "short-flared_skirt", 3 },
                { "short-H_skirt", 3 }
            };
            return dic;
        }

        private Dictionary<string, List<string>> OuterByBottomCol()
        {
            //아우터 : 베이지, 브라운, 노랑, 초록, 네이비, 흰, 회, 검
            //바지 : 베에지, 브라운, 연청, 진청, 카키, 흰, 회, 검

            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            tmp = new List<string>
            {
                "beige", 
            };
            ret.Add("long-padding", tmp); ret.Add("short-padding", tmp);

            return ret;
        }
        private Dictionary<string, List<string>> outerBytopSet()
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            tmp = new List<string>
            {
                "knit", "hood-T", "mantoman", "vest", "shirt"
            };
            ret.Add("long-padding", tmp); ret.Add("short-padding", tmp);

            tmp = new List<string>
            {
                "knit", "mantoman", "vest", "shirt", "turtleneck", "long-T"
            };
            ret.Add("long-coat", tmp); ret.Add("short-coat", tmp);

            tmp = new List<string>
            {
                "mantoman", "vest", "turtleneck", "shirt", "long-T"
            };
            ret.Add("cardigan", tmp);

            tmp = new List<string>
            {
                "mantoman", "long-T", "short-T"
            };
            ret.Add("fleece", tmp); ret.Add("zip_up_hoodie", tmp); ret.Add("jacket", tmp);

            tmp = new List<string>
            {
                "hood-T", "mantoman", "turtleneck", "long-T"
            };
            ret.Add("vest-padding", tmp);

            return ret;
        }
        private Dictionary<string, List<string>> topBytopSet()
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            //니트,맨투맨 관련
            tmp = new List<string>
            {
                "short-T", "long-T", "shirt"
            };
            ret.Add("knit", tmp);
            ret.Add("mantoman", tmp);

            //후드티 관련
            tmp = new List<string>
            {
                "short-T", "long-T"
            };
            ret.Add("hood-T", tmp);
            //vest
            tmp = new List<string>
            {
                "turtleneck", "shirt", "long-T"
            };
            ret.Add("vest", tmp);

            //shirt
            tmp = new List<string>
            {
                "long-T", "short-T"
            };
            ret.Add("shirt", tmp);

            //긴팔, 목티 관련
            tmp = new List<string>
            {
                "short-T"
            };
            ret.Add("turtleneck", tmp); ret.Add("long-T", tmp); ret.Add("short-shirt", tmp);

            tmp = new List<string>()
            {
                null
            };
            ret.Add("short-T", tmp);

            return ret;
        }

        private Dictionary<string, List<string>> topBybottomSet()
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            tmp = new List<string>
            {
                "jean", "slacks"
            };
            tmp.Sort();
            ret.Add("knit", tmp); ret.Add("hood-T", tmp); ret.Add("vest", tmp);
            ret.Add("mantoman", tmp);

            tmp = new List<string>
            {
                    "jean", "slacks", "casual-jean"
            };
            tmp.Sort();
            ret.Add("turtleneck", tmp); ret.Add("shirt", tmp);

            tmp = new List<string>
            {
                "jean", "slacks", "jogger", "casual-jean", "short-pants"
            };
            tmp.Sort();
            ret.Add("short-shirt", tmp);

            tmp = new List<string>
            {
                "jean", "slacks", "jogger", "short-pants"
            };
            tmp.Sort();
            ret.Add("long-T", tmp); ret.Add("short-T", tmp);

            return ret;
        }

        private List<string> topSoloSet()
        {
            List<string> ret = new List<string>
            {
                "hood-T", "mantoman", "turtleneck", "long-T", "short-T", "short-shirt"
            };
            ret.Sort();
            return ret;
        }

        private Dictionary<string, List<string>> topByskirtSet()
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;
            tmp = new List<string>
            {
                //스커트
                "long-flared_skirt",
                "long-H_skirt",
                "short-flared_skirt",
                "short-H_skirt"
            };
            tmp.Sort();
            ret.Add("knit", tmp); ret.Add("hood-T", tmp); ret.Add("vest", tmp);
            ret.Add("mantoman", tmp); ret.Add("shirt", tmp); ret.Add("long-T", tmp);
            ret.Add("short-shirt", tmp); ret.Add("short-T", tmp); ret.Add("turtleneck", tmp);

            return ret;
        }

        private Dictionary<string, List<string>> outerByskirtSet()
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            tmp = new List<string>
            {
                //스커트
                "long-flared_skirt",
                "long-H_skirt",
                "short-flared_skirt",
                "short-H_skirt"
            };
            tmp.Sort();
            ret.Add("cardigan", tmp); ret.Add("jacket", tmp); ret.Add("long-coat", tmp);
            ret.Add("short-coat", tmp);

            tmp = new List<string>
            {
                //스커트
                "long-flared_skirt",
                "long-H_skirt"
            };
            tmp.Sort();
            ret.Add("long-padding", tmp); ret.Add("short-padding", tmp);

            tmp = new List<string>
            {
                null
            };
            ret.Add("vest-padding", tmp); ret.Add("fleece", tmp); ret.Add("zip_up_hoodie", tmp);
            return ret;
        }

        private Dictionary<string, List<string>> topBybottomColSet() //상의 색상에 맞춰 하의 색상을 추천
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            tmp = new List<string>
            {
                "black", "deep_blue", "khaki"
            };
            tmp.Sort();
            ret.Add("red", tmp);

            tmp = new List<string>
            {
                "light_blue", "deep_blue", "beige", "khaki", "black"
            };
            tmp.Sort();
            ret.Add("orange", tmp);

            tmp = new List<string>
            {
                "light_blue", "deep_blue", "khaki", "black"
            };
            tmp.Sort();
            ret.Add("beige", tmp);

            tmp = new List<string>
            {
                "beige", "khaki", "white"
            };
            tmp.Sort();
            ret.Add("aqua", tmp); ret.Add("skyblue", tmp);

            tmp = new List<string>
            {
                "black"
            };
            tmp.Sort();
            ret.Add("purple", tmp);

            tmp = new List<string>
            {
                "light_blue", "deep_blue", "khaki", "black", "beige"
            };
            tmp.Sort();
            ret.Add("gray", tmp); ret.Add("black", tmp);

            tmp = new List<string>
            {
                "light_blue", "deep_blue", "beige", "black"
            };
            tmp.Sort();
            ret.Add("green", tmp);

            tmp = new List<string>
            {
                "beige", "black", "white"
            };
            tmp.Sort();
            ret.Add("blue", tmp);

            tmp = new List<string>
            {
                "deep_blue", "khaki", "black"
            };
            tmp.Sort();
            ret.Add("yellow", tmp);

            tmp = new List<string>
            {
                "light_blue", "khaki", "black", "beige", "white"
            };
            tmp.Sort();
            ret.Add("navy", tmp);

            tmp = new List<string>
            {
                "light_blue", "deep_blue", "khaki", "black", "beige"
            };
            tmp.Sort();
            ret.Add("white", tmp);

            tmp = new List<string>
            {
                "deep_blue", "light_blue", "black", "white"
            };
            tmp.Sort();
            ret.Add("pink", tmp);

            tmp = new List<string>
            {
                "deep_blue", "beige", "khaki", "black"
            };
            tmp.Sort();
            ret.Add("brown", tmp);

            return ret;
        }
        private Dictionary<string, List<string>> topByskirtColSet() //상의 색상에 맞춰 하의 색상을 추천
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            tmp = new List<string>
            {
                "black", "blue"
            };
            tmp.Sort();
            ret.Add("red", tmp);

            tmp = new List<string>
            {
                "skyblue", "beige", "khaki", "black"
            };
            tmp.Sort();
            ret.Add("orange", tmp);

            tmp = new List<string>
            {
                "skyblue", "blue", "black", "navy"
            };
            tmp.Sort();
            ret.Add("beige", tmp);

            tmp = new List<string>
            {
                "beige", "white"
            };
            tmp.Sort();
            ret.Add("aqua", tmp); ret.Add("skyblue", tmp);

            tmp = new List<string>
            {
                "black"
            };
            tmp.Sort();
            ret.Add("purple", tmp);

            tmp = new List<string>
            {
                "skyblue", "blue", "black", "beige", "navy"
            };
            tmp.Sort();
            ret.Add("gray", tmp); ret.Add("black", tmp);

            tmp = new List<string>
            {
                "skyblue", "beige", "black", "navy"
            };
            tmp.Sort();
            ret.Add("green", tmp);

            tmp = new List<string>
            {
                "beige", "black", "white"
            };
            tmp.Sort();
            ret.Add("blue", tmp);

            tmp = new List<string>
            {
                "black"
            };
            tmp.Sort();
            ret.Add("yellow", tmp);

            tmp = new List<string>
            {
                "skyblue", "black", "beige", "white"
            };
            tmp.Sort();
            ret.Add("navy", tmp);

            tmp = new List<string>
            {
                "skyblue", "blue", "black", "beige"
            };
            tmp.Sort();
            ret.Add("white", tmp);

            tmp = new List<string>
            {
                "black", "white"
            };
            tmp.Sort();
            ret.Add("pink", tmp);

            tmp = new List<string>
            {
                "beige", "black", "navy"
            };
            tmp.Sort();
            ret.Add("brown", tmp);

            return ret;
        }

        private Dictionary<string, List<string>> outerBytopColSet()
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;

            tmp = new List<string>()
            {
                "white", "black", "gray", "orange top"
            };
            ret.Add("navy", tmp);

            tmp = new List<string>()
            {
                "black", "khaki", "gray"
            };
            ret.Add("black", tmp);

            tmp = new List<string>()
            {
                "white", "black", "mint"
            };
            ret.Add("brown", tmp);

            tmp = new List<string>()
            {
                "black"
            };
            ret.Add("gray", tmp);

            tmp = new List<string>()
            {
                "black", "white", "gray"
            };
            ret.Add("khaki", tmp);

            tmp = new List<string>()
            {
                "black", "white"
            };
            ret.Add("beige", tmp);

            tmp = new List<string>()
            {
                "black", "gray", "blue", "skyblue", "brown", "aqua"
            };
            ret.Add("white", tmp);

            return ret;
        }

        //바지
        //type : jean, slacks, jogger, casual-jean, short-pants
        private Dictionary<string, List<string>> OuterByBottomSet()
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            List<string> tmp;
            tmp = new List<string>()
            {
                "jean", "slakcs"
            };
            ret.Add("cardigan", tmp); ret.Add("long-coat", tmp); ret.Add("short-coat", tmp);
            ret.Add("long-padding", tmp); ret.Add("short-padding", tmp); ret.Add("jacket", tmp);
            ret.Add("fleece", tmp); ret.Add("vest-padding", tmp); ret.Add("zip_up_hoodie", tmp);

            //cardigan, long-coat, short-coat, long-padding, short-padding, jacket, fleece, vest-padding

            return ret;
        }

        //bottom col : "black", "blue", "khaki", light_blue, beige
        //outer col : white, beige, khaki, gray, brown, black
        
        //
    }
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };

}
