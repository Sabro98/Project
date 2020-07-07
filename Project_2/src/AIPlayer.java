import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.*;

public class AIPlayer extends Player{
	private ArrayList<String> BuildingBlockPathList;
	private ArrayList<String> selectedList;
	private GamePanel gamePanel;
	private Random rand = new Random();
	private int[] propArr;
	private int AIDelay;
	private String multiLog;
	private String multiLogToAdd;
	
	AIPlayer(GamePanel gamePanel){
		BuildingBlockPathList = new ArrayList<String>();
		selectedList = new ArrayList<String>();
		for(int i=1; i<=4; i++) BuildingBlockPathList.add("images/BuildingBlock"+i+".png");
		propArr = new int[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
								2, 2, 2, 2, 2, 2, 
									3, 3, 3, 3,
										4, 4};
		this.gamePanel = gamePanel;
		multiLog = null;
		multiLogToAdd = null;
		try { //AIDelay�� ���� �κ�					
			File file = new File("text/AIDelay.txt"); 
			if(file.exists()) { 
				BufferedReader inFile = new BufferedReader(new FileReader(file)); 
				AIDelay = Integer.parseInt(inFile.readLine());
				inFile.close();
			}
		}catch(IOException e) {
			AIDelay = 500;
		}
	}
	
	void selectBlock() {
		var blocks = BuildingBlockSet;
		while(selectedList.size() < 6) {
			int index = propArr[rand.nextInt(propArr.length)] - 1; //Ȯ���� ����
			String path = BuildingBlockPathList.get(index);
			if(blocks.get(path) == 0) continue;	//�ܿ� ����� 0����� �ٽ� ����
			selectedList.add(path);
			blocks.put(path, blocks.get(path)-1);	//����� ������ 1�� �ٿ���
		}
	}
	
	void chooseCity(CitiesPanel panel, int boardIdx, int cityIdx, int playerIndex, int height) {
		panel.getBoards().get(boardIdx).chooseCity(cityIdx, playerIndex, height);
	}
	
	boolean isAbleCity(CitiesPanel panel, int boardIdx, int cityIdx, int playerIndex, int height) {
		return panel.getBoards().get(boardIdx).isAbleCity(cityIdx, playerIndex, height);
	}
	
	
	
	int chooseBlock() {
		int index = rand.nextInt(selectedList.size());
		String height = selectedList.get(index);
		int returnHeight = 0;
		if(height.contains("1")) returnHeight = 1;
		if(height.contains("2")) returnHeight = 2;
		if(height.contains("3")) returnHeight = 3;
		if(height.contains("4")) returnHeight = 4;
		return returnHeight;
	}
	int getBlockHeight(String block) {
		int returnHeight = 0;
		if(block.contains("1")) returnHeight = 1;
		else if(block.contains("2")) returnHeight = 2;
		else if(block.contains("3")) returnHeight = 3;
		else if(block.contains("4")) returnHeight = 4;
		return returnHeight;
	}
	void removeBlock(int index) {
		for(int i=0; i<selectedList.size(); i++){
			if(selectedList.get(i).contains(Integer.toString(index))) {				
				selectedList.remove(i);
				break;
			}
		}
	}
	int getListSize() {
		return selectedList.size();
	}
	String popMultiLog() {
		String ret = null;
		if(multiLog != null) {
			ret = multiLog;
			multiLog = null;
		}
		return ret;
	}
	String popMultiLogToAdd() {
		String ret = null;
		if(multiLogToAdd != null) {
			ret = multiLogToAdd;
			multiLogToAdd = null;
		}
		return ret;
	}
	
	void tempSelectCity(CitiesPanel panel, int boardIdx, int cityIdx, int index, int height) {
		panel.getBoards().get(boardIdx).tempChooseCity(cityIdx, index, height);
	}
	void cancleSelectCity(CitiesPanel panel, int boardIdx, int cityIdx, int index, int height) {
		panel.getBoards().get(boardIdx).minusChooseCity(cityIdx, index, height);
	}
	
	void AIPlay(int index, boolean flag) { //�ΰ������� �ൿ method, true -> ��Ƽ����
		AIPlayer ai = this;
		ai.setPlayingIcon();
		
		ArrayList<ArrayList<Integer>> candi = new ArrayList<ArrayList<Integer>>(); //������ ��� ����
		ArrayList<String> cardList = new ArrayList<String>(); //�� ����� ī�� ����
		ArrayList<Integer> scores = new ArrayList<Integer>(); //�� ����� �ջ� ���� ����
		for(int i=0; i<BuildingCardSet.size(); i++) {
			for(int j=0; j<selectedList.size(); j++) {
				for(int k=0; k<6; k++) {
					String card = getBuildingCardIdx(i);
					int height = getBlockHeight(selectedList.get(j));
					int cityIdx = gamePanel.getBuildingCardIndex(card, index);
					if(ai.isAbleCity(gamePanel.getCities(), k, cityIdx, index, height)) {
						ai.tempSelectCity(gamePanel.getCities(), k, cityIdx, index, height);
						ArrayList<Integer> tmpList = new ArrayList<Integer>();
						tmpList.add(k); tmpList.add(cityIdx); tmpList.add(height);
						cardList.add(card);
						candi.add(tmpList);
						scores.add(gamePanel.getCities().calcAIScore(gamePanel.getScore(), gamePanel.getPlayers(), index));
						ai.cancleSelectCity(gamePanel.getCities(), k, cityIdx, index, height);
					}
				}
			}
		}
		
		//�ְ� ���� Ž��
		int maxIdx = 0;
		for(int i=0; i<scores.size(); i++)
			if(scores.get(i) > maxIdx)
				maxIdx = i;
		String card = cardList.get(maxIdx);
		ArrayList<Integer> maxSelect = candi.get(maxIdx);
		int boardIdx = maxSelect.get(0);
		int cityIdx = maxSelect.get(1);
		int height = maxSelect.get(2);
	
		gamePanel.getDealer().addUsedCard(card);
		ai.removeCard(card);
		ai.removeBlock(height);
		String tempCard = gamePanel.getDealer().DealBuildingCard();
		if(tempCard != null) {
			ai.AddBuildingCard(tempCard);
		}
		try {
			Thread.sleep(AIDelay);
		}catch(InterruptedException e) {
			e.getStackTrace();
		}
		ai.chooseCity(gamePanel.getCities(), boardIdx, cityIdx, index, height);
		
		gamePanel.getLogPanel().addLog(new String(gamePanel.getPlayers().get(index).getName()+" : "+(Integer.toString(boardIdx+1)+"��° Block�� ( "+
									(cityIdx)/3 + ", " + cityIdx%3 + " ) "+"��° ���ÿ� "
										+gamePanel.getCities().getPlayerBuildingHeight(boardIdx, cityIdx, index)+"�� �ǹ��� ����")));
		if(flag) {	//��Ƽ ���
			multiLogToAdd = new String(gamePanel.getPlayers().get(index).getName()+" : "+(Integer.toString(boardIdx+1)+"��° Block�� ( "+
					(cityIdx)/3 + ", " + cityIdx%3 + " ) "+"��° ���ÿ� "+gamePanel.getCities().getPlayerBuildingHeight(boardIdx, cityIdx, index)+"�� �ǹ��� ����"));
			//�����ؾ� �ϴ� ����
			//playerIdx, boardIdx, cityIdx, height, ���� card, ���� block
			multiLog = new String(index + " " + boardIdx + " " +cityIdx+ " " + height);
		}
		ai.setEndIcon();
	}
	
}
