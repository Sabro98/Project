import java.awt.*;
import javax.swing.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.event.*;


public class PlayerUIPanel extends JPanel {
	private ArrayList<String> BuildingCardSet;
	private HashMap<String, Integer> BuildingBlockSet;
	private int selectedCard;
	private GamePanel gamePanel;
	private GamePlayer gamePlayer;
	private BuildingCardPanel cardPanel;
	private SelectedBlockPanel selectedPanel;
	private BuildingBlockPanel blockPanel;
	private boolean cardFlag;
	private boolean blockFlag;
	private boolean turnFlag;
	private JLabel infoLabel;
	private TimerThread timerTh;
	private int cardIdx;
	private boolean timerFlag;
	
	private String startPlayer, nowPlayer, round;
	private JLabel startPlayerLabel, nowPlayerLabel, roundLabel;
	
	private JLabel[] scoreLabel;
	private int[] score;

	private JLabel highestLabel;
	private int highestBuilding;
	private Random rand;
	private int gamePlayerIndex;
	
	private String[] playerNames;
	
	public PlayerUIPanel(GamePanel gamePanel, int gamePlayerIdx) {
		this.gamePanel = gamePanel;
		gamePlayer = this.gamePanel.getGamePlayer(gamePlayerIdx);
		BuildingBlockSet = gamePlayer.getBuildingBlockSet();
		infoLabel = new JLabel("Select six Block!");
		infoLabel.setFont(new Font("Helvetica", Font.BOLD, 13));
		scoreLabel = new JLabel[4];
		score = new int[4];
		rand = new Random();
		playerNames = new String[4];
		for(int i=0; i<gamePanel.getPlayers().size(); i++) playerNames[i] = gamePanel.getPlayers().get(i).getName();
		
		cardFlag = false;
		turnFlag = false;
		blockFlag = false;
		setLayout(null);
		setSize(1075, 370);
		setBackground(new Color(232,232,232));
		
		cardPanel = new BuildingCardPanel();
		selectedPanel = new SelectedBlockPanel();
		blockPanel = new BuildingBlockPanel();
		
		startPlayer = "";
		nowPlayer = "";
		round = "";
		startPlayerLabel = new JLabel("Start : " + startPlayer);
		startPlayerLabel.setFont(new Font("Helvetica", Font.BOLD, 13));	
		nowPlayerLabel = new JLabel("Now : " + nowPlayer);
		nowPlayerLabel.setFont(new Font("Helvetica", Font.BOLD, 13));	
		roundLabel = new JLabel("Round : " + round);	
		roundLabel.setFont(new Font("Helvetica", Font.BOLD, 13));
		
		cardPanel.setBounds(0, 15, 255, 250);
		selectedPanel.setBounds(300, 35, 170, 220);
		blockPanel.setBounds(500, 28, 150, 220);
		infoLabel.setBounds(330, 0, 120, 30);
		infoLabel.setVisible(false);
		
		startPlayerLabel.setBounds(700, 50, 170, 50);
		nowPlayerLabel.setBounds(700, 120, 170, 50);
		roundLabel.setBounds(700, 190, 150, 50);
		
		highestLabel = new JLabel("Highest : "+highestBuilding);
		highestLabel.setBounds(870, 0, 200, 50);
		highestLabel.setFont(new Font("Helvetica", Font.BOLD, 18));
		
		//점수
		for(int i=0; i<scoreLabel.length; i++) {
			scoreLabel[i] = new JLabel(playerNames[i]+" : "+score[i]+"점");
			scoreLabel[i].setBounds(870, 55 + i*55, 150, 50);
			add(scoreLabel[i]);
		}
		
		add(cardPanel);
		add(selectedPanel);
		add(blockPanel);
		add(infoLabel);
		add(startPlayerLabel);
		add(nowPlayerLabel);
		add(roundLabel);
		add(highestLabel);
	}
	
	void setPlayersName() {
		for(int i=0; i<gamePanel.getPlayers().size(); i++) playerNames[i] = gamePanel.getPlayers().get(i).getName();
		//점수
		for(int i=0; i<scoreLabel.length; i++) {
			scoreLabel[i].setText(playerNames[i]+" : "+score[i]+"점");
		}
	}
	
	void selectRandomBtn(int idx) {
		gamePanel.getCities().selectRandomCity(idx);
	}
	
	class CheckThread extends Thread{		
		@Override
		public void run() {
			while(true) {
				if(gamePanel.isCityClicked()) {
					setTimerFlag(false);
					gamePanel.getTimerPanel().stopTimer();
					gamePanel.addBuildingCard(gamePlayer);
					cardPanel.renewCard();
					turnFlag = false;
					break;
				}
				try {
					Thread.sleep(50);
				}catch(InterruptedException e) {
					e.getStackTrace();
				}
			}
		}
	}
	
	void setHighestBuilding(int highestB) {
		highestBuilding = highestB;
		highestLabel.setText("Highest : "+highestBuilding);
	}
	
	void setScore(int[] score) {
		this.score = score;
		for(int i=0; i<score.length; i++) {
			scoreLabel[i].setText(playerNames[i]+" : "+score[i]+"점");
		}
	}
	
	int[] getScore() {
		return score;
	}
	
	void setCardFlag(boolean f) {
		cardFlag = f;
	}
	void setInfoToCard() {
		infoLabel.setText("Select CityCard!");
		infoLabel.setVisible(true);
	}
	void setTurnFlag(boolean f) {
		turnFlag = f;
	}
	void startTurn() {
		setBlockFlag(true);
		setTurnFlag(true);
		setInfoToBlock();
		startTimer();
		setTimerFlag(true);
		timerTh = new TimerThread();
		timerTh.start();
	}
	void setTimerFlag(boolean f) {
		timerFlag = f;
	}
	void startTimer() {
		gamePanel.getTimerPanel().runTimer();
	}
	boolean getTurnFlag() {
		return turnFlag;
	}
	void selectBlock() {
		blockPanel.selectBlock();
	}
	void setBlockFlag(boolean f) {
		blockFlag = f;
	}
	
	void setInfoToBlock(){
		infoLabel.setText("Select CityBlock!");
		infoLabel.setVisible(true);
	}
	void setInfoToSelct() {
		infoLabel.setText("Select six Block!");
	}
	void setStartPlayer(int startP) {
		if(startP != -1) {
			startPlayer = gamePanel.getPlayers().get(startP).getName();
			startPlayerLabel.setText("Start : "+startPlayer);		
		}
	}
	
	void setNowPlayer(int nowP) {
		nowPlayer = gamePanel.getPlayers().get(nowP).getName();
		nowPlayerLabel.setText("Now : " + nowPlayer);
	}
	
	void setRound(String round) {
		this.round = round;
		roundLabel.setText("Round : " + round);
	}
	
	class TimerThread extends Thread{
		@Override
		public void run() {
			while(timerFlag) {
				if(!gamePanel.getTimerPanel().isThAlive()) {
					if(blockFlag) { //블록x, 카드x
						//블록 선택
						selectedPanel.selectRandomBlock();
						//카드 선택
						int idx = cardPanel.selectRandomCard();
						//블록 선택
						selectRandomBtn(idx);
					}else if(cardFlag) { //블록o, 카드x
						int idx = cardPanel.selectRandomCard();
						selectRandomBtn(idx);
					}else {	//볼록o, 카드o
						selectRandomBtn(cardIdx);
					}
					setBlockFlag(false);
					setCardFlag(false);
					setTurnFlag(false);
					break;
				}
				try {
					sleep(50);
				}catch(InterruptedException e) {
					e.getStackTrace();
				}
			}
		}
	}
	
	class BuildingCardPanel extends JPanel{
		private ArrayList<JLabel> cardLabels;
		BuildingCardPanel() {
			setSize(500, 340);
			this.setLayout(new GridLayout(2, 2, 3, 3));
			this.setBackground(new Color(232,232,232));
			renewCard();
		}
		synchronized void renewCard() {
			removeAll();
			cardLabels = new ArrayList<JLabel>();
			BuildingCardSet = gamePlayer.getBuildingCardSet();
			int size = 0;
			for(var path : BuildingCardSet) {
				int sizeT = size++;
				JLabel tmp = new JLabel(new ImageIcon(path));
				tmp.setName(path);
				tmp.addMouseListener(new MouseAdapter() {
					@Override
					public void mousePressed(MouseEvent e) {
						if(cardFlag) {	
							try {
								JLabel clicked = (JLabel)e.getSource();
								cardIdx = selectCard(clicked);
								cardLabels.set(sizeT, null);								
							}catch(IndexOutOfBoundsException e1) {
							}
						}
					}
				});
				cardLabels.add(tmp);
			}
			for(var tmp : cardLabels) add(tmp);
			revalidate();
			repaint();
		}
		
		int selectCard(JLabel clicked) {
			String path = clicked.getName();
			int idx = gamePanel.setCitiesBtn(path, gamePlayer.getSpot(), gamePlayer.getBlockHeight());
			gamePanel.addUsedCard(path);
			repaint();
			cardFlag = false;
			gamePlayer.removeCard(path);
			CheckThread th = new CheckThread();
			th.start();
			setInfoToSelct();
			infoLabel.setVisible(false);
			remove(clicked);
			return idx;
		}
		
		int selectRandomCard() {
			JLabel clicked = cardLabels.get(rand.nextInt(cardLabels.size()));
			while(clicked == null) {
				int cnt = 0;
				for(int i=0; i<6; i++) {
					//모든 공간에 못두는 경우 방지
					if(gamePanel.getCities().getBoards().get(0).getButtons()[gamePanel.getBuildingCardIndex(clicked.getName(), gamePanel.getPlayerIdx())].getBackground()
							== Color.red)
						break;
					else
						cnt++;
				}
				if(cnt == 6) continue;
				clicked = cardLabels.get(rand.nextInt(cardLabels.size()));
			}			
			String path = clicked.getName();
			int returnIdx = gamePanel.setCitiesBtn(path, gamePlayer.getSpot(), gamePlayer.getBlockHeight());
			gamePanel.addUsedCard(path);
			gamePlayer.removeCard(path);
			infoLabel.setVisible(false);
			remove(clicked);
			revalidate();
			repaint();
			for(int i=0; i<cardLabels.size(); i++) {
				if(cardLabels.get(i) != null && cardLabels.get(i).equals(clicked)) {
					cardLabels.set(i, null);
					break;
				}
			}
			gamePanel.addBuildingCard(gamePlayer);
			cardPanel.renewCard();
			setInfoToSelct();
			return returnIdx;
		}
	}
	
	class SelectedBlockPanel extends JPanel{
		private ArrayList<JButton> selectedBlocks;
		SelectedBlockPanel(){
			this.setSize(300, 100);
			this.setLayout(new GridLayout(2, 3, 0, 0));
			selectedBlocks = new ArrayList<JButton>();
			setBackground(new Color(232,232,232));
		}
		
		void addBlock(String selectedBlock, int index) {
			JButton select = new JButton(new ImageIcon(selectedBlock));
			select.setBorderPainted(false); 
			select.setContentAreaFilled(false); 
			select.setFocusPainted(false);
			select.setOpaque(false);
			select.setName(selectedBlock);
			gamePlayer.addSelectedBlock(selectedBlock);
			selectedBlocks.add(select);
			select.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					if(blockFlag) {	
						JButton clicked = (JButton)e.getSource();
						selectBlock(clicked);
					}
				}
			});
			add(select);
		}
		
		void selectBlock(JButton clicked) {
			gamePlayer.setBlockHeight(clicked.getName());
			gamePlayer.removeSelectedBlock(clicked.getName());
			remove(clicked);
			repaint();
			setBlockFlag(false);
			setCardFlag(true);
			setInfoToCard();
			for(int i=0; i<selectedBlocks.size(); i++) {
				if(selectedBlocks.get(i) != null && selectedBlocks.get(i).equals(clicked)) {
					selectedBlocks.set(i, null);
					break;
				}
			}
			isNull();
		}
		
		void isNull() {
			for(int i=0; i<selectedBlocks.size(); i++) {
				if(selectedBlocks.get(i) != null) return;
			}
			selectedBlocks.clear();
		}
		
		void selectRandomBlock() {
			JButton clicked = selectedBlocks.get(rand.nextInt(selectedBlocks.size()));
			while(clicked == null)
				clicked = selectedBlocks.get(rand.nextInt(selectedBlocks.size()));
			selectBlock(clicked);
		}
		void clearList() {
			selectedBlocks.clear();
		}
	}
	
	class BuildingBlockPanel extends JPanel{
		private ArrayList<JButton> blocks;
		private ArrayList<JLabel> numbers;
		private ArrayList<String> selectedList;
		private int cnt;
		BuildingBlockPanel(){
			setSize(300, 340);
			this.setBackground(new Color(232,232,232));
			this.setLayout(new GridLayout(2, 4, 3, 3));
			blocks = new ArrayList<JButton>();
			numbers = new ArrayList<JLabel>();
			
			var keys = BuildingBlockSet.keySet();
			
			//각각 블록들의 정보 처리
			for(var key : keys) {
				var block = new JButton(new ImageIcon(key));
				block.setBorderPainted(false); 
				block.setContentAreaFilled(false); 
				block.setFocusPainted(false); 
				block.setOpaque(false);
				block.setEnabled(false);
				block.setName(key);
				
				block.addActionListener(new ActionListener() {
					@Override
					public void actionPerformed(ActionEvent e) {
						if(BuildingBlockSet.get(key) != 0) {
							BuildingBlockSet.put(key, BuildingBlockSet.get(key) - 1);
							selectedList.add(key);
							for(var label : numbers) {
								if(label.getName().equals(key)) {
									label.setText(" x "+BuildingBlockSet.get(key).toString());
								}
							}
							selectedPanel.addBlock(key, cnt++);
						}
					}
				});
				
				blocks.add(block);
				var numberLabel = new JLabel(" x "+BuildingBlockSet.get(key).toString());
				numberLabel.setName(key);
				numbers.add(numberLabel);
			}
			for(int i=0; i<blocks.size(); i++) {
				add(blocks.get(i));
				add(numbers.get(i));
			}
		}
		
		void selectBlock() {
			infoLabel.setVisible(true);
			cnt = 0;
			selectedList = new ArrayList<String>();
			for(JButton block : blocks) block.setEnabled(true);
			
			while(true) {	//6개의 블록 선택을 기다리는 반복문
				if(cnt == 6) break;
				try {
					Thread.sleep(50);					
				}catch(InterruptedException e) {
					e.getStackTrace();
				}
			}
			
			for(JButton block : blocks) block.setEnabled(false);
			infoLabel.setVisible(false);
		}
		
	}
}