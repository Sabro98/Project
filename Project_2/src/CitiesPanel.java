import java.awt.*;
import javax.swing.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.event.*;

public class CitiesPanel extends JPanel {
	private ArrayList<CityBoard> boards;
	private boolean flag = true;
	private int highestBuilding;
	private int highestOwner;
	private String log;
	private String multiLog;
	
	public CitiesPanel(ArrayList<Player> players) {
		boards = new ArrayList<CityBoard>();
		setSize(414, 250);
		setBackground(Color.white);
		this.setLayout(new GridLayout(2, 3, 5, 5));
		for(int i=0; i<6; i++) {
			boards.add(new CityBoard(players));
			boards.get(i).setName(Integer.toString(i));
			add(boards.get(i));
		}
		highestBuilding = -1;
		highestOwner = -1;
	}
	
	boolean isMultiHigh(){
		boolean result;
		int cnt = 0;
		for(var board : boards) {
			for(var btn : board.getButtons()) {
				if((btn.getHighestBuilding() != -1) && (btn.getHighestBuilding() == highestBuilding)) 
					cnt++;
			}
		}
		if(cnt == 1) result = false;
		else result = true;
		return result;
	}
	int getLargeNumOfBuildings(ArrayList<Integer> candi) {
		int[] num = new int[4];
		for(var board : boards) {
			for(var btn : board.getButtons()) {
				if((btn.getOwner() != -1)) 
					num[btn.getOwner()]++;
			}
		}
		int maxIdx = 0;
		for(int i=0; i<candi.size(); i++) maxIdx = num[maxIdx] > num[i]? maxIdx : i;
		return maxIdx;
	}
	void setCityBoardIcon(int index, int playerIndex, int playerHeight, ArrayList<Player> players) {
		for(var board : boards) board.setBtnEnable(index, playerIndex, playerHeight);
		var th = new ClickThread(index);
		th.start();
	}

	ArrayList<CityBoard> getBoards(){
		return boards;
	}
	
	boolean getFlag() {
		return flag;
	}
	
	Stack<String> calcScore(int[] inputScore, ArrayList<Player> players) {
		int[] numBuilding = new int[4];
		Stack<String> retArr = new Stack<String>();
		int tmpHigh, tmpOwner;
		for(var board : boards) {
			var btns = board.getButtons();
			int[] buildings = new int[4];
			for(var btn : btns) {
				//최고층 빌딩
				if((tmpHigh = btn.getHighestBuilding()) != -1) {
					if(highestBuilding < tmpHigh) {
						highestBuilding = tmpHigh;
						highestOwner = btn.getOwner();
					}
				}
				//볼록당 빌딩 갯수
				if(btn.getOwner() != -1) {
					buildings[btn.getOwner()]++;
				}
				//전체 빌딩 갯수
				if((tmpOwner = btn.getOwner()) != -1)
					numBuilding[tmpOwner]++;
			}
			//블록당 갯수 최대값 찾기
			int alotBuildingIdx = 0;
			for(int i=0; i<buildings.length; i++) {
				alotBuildingIdx = buildings[alotBuildingIdx] > buildings[i]? alotBuildingIdx : i;
			}
			
			//블록당 갯수 중복 찾기
			int cnt = 0;
			for(int i=0; i<buildings.length; i++) {
				if(buildings[alotBuildingIdx] == buildings[i]) cnt++;
			}
			if(cnt == 1) {
				retArr.add(players.get(alotBuildingIdx).getName()+" 블록 중 가장 많은 빌딩으로 2점 획득!");
				inputScore[alotBuildingIdx] += 2;
			}
		}
		
		//건물 한채당 +1점
		for(int i=inputScore.length - 1; i>=0; i--) {
			retArr.add(players.get(i).getName()+" 보유한 건물 "+numBuilding[i]+"점 획득!");
			inputScore[i] += numBuilding[i];
		}
		
		//전체 최고 빌딩 중복체크
		boolean highestFlag = true;
		for(var board : boards) 
			for(var button : board.getButtons()) 
				if(highestBuilding == button.getHighestBuilding() && highestOwner != button.getOwner()) highestFlag = false;
		if(highestFlag) {
			retArr.add(players.get(highestOwner).getName()+" 게임에서 가장 높은 빌딩으로 3점 획득!");
			inputScore[highestOwner]+=3;
		}
		
		return retArr;
	}
	
	int calcAIScore(int[] inputScore, ArrayList<Player> players, int AIindex) {
		int[] numBuilding = new int[4];
		int[] tmpScore = new int[4];
		int highestBuilding = -1;
		for(int i=0; i<inputScore.length; i++) tmpScore[i] = inputScore[i];		
		int tmpHigh = -1, tmpOwner = -1;
		for(var board : boards) {
			var btns = board.getButtons();
			int[] buildings = new int[4];
			for(var btn : btns) {
				//최고층 빌딩
				if((tmpHigh = btn.getHighestBuilding()) != -1) {
					if(highestBuilding < tmpHigh) {
						highestBuilding = tmpHigh;
						highestOwner = btn.getOwner();
					}
				}
				//볼록당 빌딩 갯수
				if(btn.getOwner() != -1) {
					buildings[btn.getOwner()]++;
				}
				//전체 빌딩 갯수
				if((tmpOwner = btn.getOwner()) != -1)
					numBuilding[tmpOwner]++;
			}
			//블록당 갯수 최대값 찾기
			int alotBuildingIdx = 0;
			for(int i=0; i<buildings.length; i++) {
				alotBuildingIdx = buildings[alotBuildingIdx] > buildings[i]? alotBuildingIdx : i;
			}
			
			//블록당 갯수 중복 찾기
			int cnt = 0;
			for(int i=0; i<buildings.length; i++) {
				if(buildings[alotBuildingIdx] == buildings[i]) cnt++;
			}
			if(cnt == 1) {
				tmpScore[alotBuildingIdx] += 2;
			}
		}
		
		//건물 한채당 +1점
		for(int i=tmpScore.length - 1; i>=0; i--) {
			tmpScore[i] += numBuilding[i];
		}
		
		//전체 최고 빌딩 중복체크
		boolean highestFlag = true;
		for(var board : boards) 
			for(var button : board.getButtons()) 
				if(highestBuilding == button.getHighestBuilding() && highestOwner != button.getOwner()) highestFlag = false;
		if(highestFlag) {
			tmpScore[highestOwner]+=3;
		}
		
		return tmpScore[AIindex];
	}
	
	int getHighestBuilding() {
		return highestBuilding;
	}
	int getHighestOwner() {
		return highestOwner;
	}
	String popLog() {
		String ret = log;
		log = null;
		return ret;
	}
	String getLog() {
		return log;
	}
	
	String getMultiLog() {
		return multiLog;
	}
	String popMultiLog() {
		String ret = multiLog;
		multiLog = null;
		return ret;
	}

	int getPlayerBuildingHeight(int boardIdx, int cityIdx, int playerIdex) {
		return boards.get(boardIdx).getButtons()[cityIdx].getPlayerBuildingHeight(playerIdex);
	}
	
	void selectRandomCity(int idx) {
		Random rand = new Random();
		int boardIdx;
		while(true) {			
			boardIdx = rand.nextInt(4);
			if(boards.get(boardIdx).isEnableBtn(idx) && boards.get(boardIdx).isAbletoClick(idx)) break;
		}
		boards.get(boardIdx).clickBtn(idx);
	}
	
	void renewPlayersName(ArrayList<Player> players) {
		for(CityBoard board : boards) board.renewPlayersName(players);
	}
	
	class ClickThread extends Thread{
		private int index;
		ClickThread(int i){
			index = i;
		}
		@Override
		public void run() {
			flag = true;
			while(flag) {
				for(var board : boards) {
					if(board.getIsClicked(index)) {
						for(var tmp : boards) {
							tmp.setBtnDefault(index);
							if(tmp.getLog() != null) log = tmp.popLog();
							if(tmp.getMultiLog() != null) multiLog = tmp.popMultiLog();
						}
						
						flag = false;
						break;
					}
				}
				try {
					Thread.sleep(50);
				}catch(InterruptedException e) {
					e.getStackTrace();
				}
			}
		}
	}
}
