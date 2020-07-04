import java.util.*;

public class Dealer {
	private ArrayList<String> BuildingCardPathList;
	private HashMap<String, Integer> BuildingCardSet;
	private int buildingCardSum;
	private Stack<String> BuildingUsedCardList;
	
	public Dealer() {
		BuildingCardPathList = new ArrayList<String>();
		BuildingUsedCardList = new Stack<String>();
		for(int i=0; i<9; i++) BuildingCardPathList.add("images/BuildingCard"+i+".png");
		buildingCardSum = 45;
		BuildingCardSet = new HashMap<String, Integer>();
		for(int i=0; i<BuildingCardPathList.size(); i++)
			BuildingCardSet.put(BuildingCardPathList.get(i), 5);
	}
	
	void renewCard() {
		buildingCardSum = BuildingUsedCardList.size();
		while(!BuildingUsedCardList.isEmpty()) {
			var path = BuildingUsedCardList.pop();
			BuildingCardSet.put(path, BuildingCardSet.get(path) + 1);
		}
	}
	String DealBuildingCard(){
		if(buildingCardSum == 0)
			renewCard();			
		Random rand = new Random();
		String Path = null;
		while(true) {
			int index = rand.nextInt(9);
			Path = BuildingCardPathList.get(index);
			if(BuildingCardSet.get(Path) != 0) {
				var value = BuildingCardSet.get(Path);
				BuildingCardSet.put(Path, --value);
				break;
			}
		}
		buildingCardSum--;
		return Path;
	}
	void addUsedCard(String path) {
		BuildingUsedCardList.add(path);
	}
}
