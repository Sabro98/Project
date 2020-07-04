import java.awt.*;

import javax.sound.sampled.*;
import java.util.*;
import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;

import java.awt.event.*;
import java.io.*;
import javax.swing.event.*;

public class GamePanel extends JPanel {
	private CitiesPanel cities;
	private ArrayList<Player> players;
	private Dealer dealer;
	private PlayerUIPanel panel;
	private GameLogPanel logPanel;
	private Random rand = new Random();
	private ArrayList<GamePlayer> gamePlayers;
	private ArrayList<AIPlayer> AIPlayers;
	private TimerPanel timerPanel;
	private int turnSize, gameRoundSize;
	private int[] score;
	private int playerNum;
	private int gamePlayerIdx;
	private BufferedReader in;
	private BufferedWriter out;
	private int multiTurn;
	private String turnEndSoundPath;
	private String roundEndSoundPath;
	private float VolumeSize;
	
	public GamePanel(int gamePlayerIdx, int playerNum, String[] playerName) {
		setBackground(Color.white);
		this.gamePlayerIdx = gamePlayerIdx;
		in = null; out = null;
		gamePlayers = new ArrayList<GamePlayer>();
		players = new ArrayList<Player>();
		AIPlayers = new ArrayList<AIPlayer>();
		dealer = new Dealer();
		timerPanel = new TimerPanel();
		turnSize = 6;
		gameRoundSize = 4;
		score = new int[4];
		multiTurn = -1;
		this.playerNum = playerNum;
		turnEndSoundPath = "TurnEnd";
		roundEndSoundPath = "RoundEnd";
		readVolumeSize();
		Border border = new LineBorder(Color.GRAY, 2, true);
		setBorder(border);
		
		for(int i=0; i<4-playerNum; i++) {
			AIPlayers.add(new AIPlayer(this));
			AIPlayers.get(i).setName("AIPlayer"+i);
		}
		
		for(int i=0; i<playerNum; i++) {
			gamePlayers.add(new GamePlayer(this));
			gamePlayers.get(i).setName(playerName[i]);
			players.add(gamePlayers.get(i));
		}

		for(int i=playerNum; i<4; i++) players.add(AIPlayers.get(i-playerNum));
		
		java.util.Collections.shuffle(players);
		for(int i=0; i<4; i++) {
			players.get(i).setImage("images/player"+i+".png");
			players.get(i).setSpot(i);
		}
		
		cities = new CitiesPanel(players);
		
		
		setSize(450, 350);
		setLayout(new BorderLayout());
		
		add(cities, BorderLayout.CENTER);
		add(players.get(0), BorderLayout.SOUTH);
		add(players.get(1), BorderLayout.EAST);
		add(players.get(2), BorderLayout.NORTH);
		add(players.get(3), BorderLayout.WEST);
		
		for(int i=0; i<players.size(); i++) 
			for(int j=0; j<4; j++) players.get(i).AddBuildingCard(dealer.DealBuildingCard());
		logPanel = new GameLogPanel();
		panel = new PlayerUIPanel(this, this.gamePlayerIdx);
	}
	int getPlayerIdx() {
		return gamePlayerIdx;
	}

	void readVolumeSize() {
		try {					
			File file = new File("text/Volume.txt"); 
			if(file.exists()) { 
				BufferedReader inFile = new BufferedReader(new FileReader(file)); 
				VolumeSize = Float.parseFloat(inFile.readLine()); 
				inFile.close();
			}
		}catch(IOException e) {
			e.getStackTrace();
		}
	}
	void setIO(BufferedReader in, BufferedWriter out) {
		this.in = in;
		this.out = out;
	}
	
	void shufflePlayers(ArrayList<Player> newPlayers) {
		for(int i=0; i<players.size(); i++) remove(players.get(i));
		players = newPlayers;
		panel.setPlayersName();
		add(players.get(0), BorderLayout.SOUTH);
		add(players.get(1), BorderLayout.EAST);
		add(players.get(2), BorderLayout.NORTH);
		add(players.get(3), BorderLayout.WEST);
		revalidate();
		repaint();
		
		for(int i=0; i<cities.getBoards().size(); i++) {
			cities.getBoards().get(i).setPlayers(players);
			cities.getBoards().get(i).setActionListener();
			cities.renewPlayersName(players);
		}
	}
	
	PlayerUIPanel getUIPanel() {
		return panel;
	}
	GamePlayer getGamePlayer(int index) {
		return gamePlayers.get(index);
	}
	TimerPanel getTimerPanel() {
		return timerPanel;
	}
	CitiesPanel getCities() {
		return cities;
	}
	
	int setCitiesBtn(String spot, int playerIndex, int playerHeight) {
		int index = getBuildingCardIndex(spot, playerIndex);
		cities.setCityBoardIcon(index, playerIndex, playerHeight, players);
		return index;
	}
	
	boolean isCityClicked() {
		return !cities.getFlag();
	}
	void addBuildingCard(Player player) {
		var tempCard = dealer.DealBuildingCard();
		if(tempCard != null) player.AddBuildingCard(tempCard);
	}
	void removePlayerCard(Player player, String path) {
		player.removeCard(path);
	}
	GameLogPanel getLogPanel() {
		return logPanel;
	}
	ArrayList<GamePlayer> getGamePlayers(){
		return gamePlayers;
	}
	ArrayList<AIPlayer> getAIPlayers(){
		return AIPlayers;
	}
	
	synchronized void addMultiAction(String[] commands) {
		int playerIndex = Integer.parseInt(commands[0]);
		int boardIndex = Integer.parseInt(commands[1]);
		int cityIndex = Integer.parseInt(commands[2]);
		int height = Integer.parseInt(commands[3]);
		cities.getBoards().get(boardIndex).getButtons()[cityIndex].addBlock(playerIndex, height);
	}
	
	int getBuildingCardIndex(String path, int index) {
		int idx = 0;
		for(int i=0; i<9; i++) {
			if(path.contains(Integer.toString(i))) {
				idx = i;
				break;
			}
		}
		//바라보는 방향에 따라
		if(index == 1) {
			if(idx == 0) idx = 6;
			else if(idx == 1) idx = 3;
			else if(idx == 2) idx = 0;
			else if(idx == 3) idx = 7;
			else if(idx == 4) idx = 4;
			else if(idx == 5) idx = 1;
			else if(idx == 6) idx = 8;
			else if(idx == 7) idx = 5;
			else if(idx == 8) idx = 2;
		}else if(index == 2) {
			idx = 8 - idx;
		}else if(index == 3) {
			if(idx == 0) idx = 2;
			else if(idx == 1) idx = 5;
			else if(idx == 2) idx = 8;
			else if(idx == 3) idx = 1;
			else if(idx == 4) idx = 4;
			else if(idx == 5) idx = 7;
			else if(idx == 6) idx = 0;
			else if(idx == 7) idx = 3;
			else if(idx == 8) idx = 6;
		}
		return idx;
	}
	
	void addUsedCard(String path) {
		dealer.addUsedCard(path);
	}
	
	Dealer getDealer() {
		return dealer;
	}
	
	ArrayList<Player> getPlayers(){
		return players;
	}
	void setPlayers(ArrayList<Player> players) {
		this.players = players;
	}
	
	private void selectBlock() {
		for(var ai : AIPlayers) ai.selectBlock();
		panel.selectBlock();
	}
	
	void selectPlayerBlock() {
		panel.selectBlock();
	}
	
	void startGame() { //게임을 진행해주는 method
		GameThread th = new GameThread();
		th.start();
	}
	void setStartIcon(int index) {
		players.get(index).setPlayingIcon();
	}
	void setEndIcon(int index) {
		players.get(index).setEndIcon();
	}
	
	void setMultiNowTurn(int turn) {
		multiTurn = turn;
		panel.setNowPlayer(turn);
	}
	
	void setMultiStartTurn(int turn) {
		multiTurn = turn;
		panel.setStartPlayer(multiTurn);
	}
	
	synchronized String multiAIPlay(int index) {
		AIPlayer ai = (AIPlayer)players.get(index);
		ai.AIPlay(index, true);
		String multiLog = ai.popMultiLog();
		return multiLog;
	}
	void addLog(String log) {
		logPanel.addLog(log);
	}
	String getmultiAILog(int index) {
		return ((AIPlayer)players.get(index)).popMultiLogToAdd();
	}
	String getMultiPlayerLog(int index) {
		return cities.popLog();
	}
	synchronized String multiPlayerPlay(int playerIndex) {
		Player player = (GamePlayer)players.get(playerIndex);
		player.setPlayingIcon();
		panel.startTurn();
		while(panel.getTurnFlag()) {
			try {
				Thread.sleep(10);
			}catch(InterruptedException e) {
				e.getStackTrace();
			}
		}
		player.setEndIcon();
		while(cities.getMultiLog() == null) {
			try {
				Thread.sleep(10);							
			}catch(InterruptedException e) {
				e.getStackTrace();
			}
		}
		return cities.popMultiLog();
	}
	
	void setRound(String round) {
		panel.setRound(round);
	}
	
	int getHighest() {
		return cities.getHighestBuilding();
	}
	class GameThread extends Thread{
		@Override
		public void run() {
			int Turn = rand.nextInt(4);		
			logPanel.addLog("게임 시작!!");
			logPanel.addLog("---------------------------------");
			while(gameRoundSize-- > 0) {
				setRound(Integer.toString(4 - gameRoundSize));
				logPanel.addLog((4-gameRoundSize)+"번째 라운드!");
				Turn--;
				if(Turn == -1) Turn = 3;
				panel.setStartPlayer(Turn);
				logPanel.addLog(players.get(Turn).getName()+" 부터 게임 시작!");
				//라운드 시작 전 블록 선택
				selectBlock();
				
				//라운드를 진행
				for(int i=0; i<turnSize * players.size(); i++) {
					panel.setNowPlayer(Turn);
					if(players.get(Turn).getClass() == GamePlayer.class)
						((GamePlayer)players.get(Turn)).PlayerPlay();
					else if(players.get(Turn).getClass() == AIPlayer.class)
						((AIPlayer)players.get(Turn)).AIPlay(Turn, false);
					
					playSound(turnEndSoundPath);
					Turn--;
					if(Turn == -1) Turn = 3;
				}
				playSound(roundEndSoundPath);
				logPanel.addLog((4-gameRoundSize)+"번째 라운드 종료!");
				logPanel.addLog("\n------------- 점 수 ---------------");
				//점수 계산
				Stack<String> scoreArr = cities.calcScore(score, players);
				logPanel.addLog(scoreArr);
				panel.setHighestBuilding(cities.getHighestBuilding());
				panel.setScore(score);
				logPanel.addLog("---------------------------------\n");
			}
			logPanel.addLog("게임 종료!!\n");
			calcWinner();
		}
	}
	int[] getScore() {
		return score;
	}
	void setScore(int[] scores) {
		panel.setScore(scores);
	}
	void calcWinner() {
		int[] scores = panel.getScore();
		int winner = -1;
		
		//최고 점수 계산
		int maxScoreIdx = 0;
		for(int i=0; i<scores.length; i++)
			maxScoreIdx = scores[maxScoreIdx] > scores[i]? maxScoreIdx : i;
		
		//최고점수 중복 계산
		int cnt = 0;
		ArrayList<Integer> candidate = new ArrayList<Integer>();
		for(int i=0; i<scores.length; i++)
			if(scores[i] == scores[maxScoreIdx]) {
				cnt++;
				candidate.add(i);
			}
		if(cnt == 1) {
			winner = maxScoreIdx;			
		}
		else { //최고점수 중복
			if(!cities.isMultiHigh() && candidate.contains(cities.getHighestOwner())) {
				winner = cities.getHighestOwner();
			}else {
				winner = cities.getLargeNumOfBuildings(candidate);
			}
		}
		logPanel.addLog("게임의 우승자는  "+players.get(winner).getName()+"입니다 축하합니다!");
	}
	 
	void playSound(String path) {          
	      try {
			 File soundFile = new File("sound/"+path+".wav");
			 AudioInputStream audioIn = AudioSystem.getAudioInputStream(soundFile); 
			 AudioFormat format = audioIn.getFormat();             
			 DataLine.Info info = new DataLine.Info(Clip.class, format);
			 Clip clip = (Clip)AudioSystem.getLine(info);
	         clip.open(audioIn);
	         FloatControl gainControl = (FloatControl) clip.getControl(FloatControl.Type.MASTER_GAIN);
	         gainControl.setValue(VolumeSize);
	         clip.start();
	      } catch (UnsupportedAudioFileException e) {
	         e.printStackTrace();
	      } catch (IOException e) {
	         e.printStackTrace();
	      } catch (LineUnavailableException e) {
	         e.printStackTrace();
	      }
	}
}
