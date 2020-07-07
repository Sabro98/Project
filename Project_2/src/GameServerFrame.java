import java.net.*;
import java.io.*;
import java.util.*;
import java.awt.*;
import javax.swing.*;
import java.awt.event.*;
import javax.swing.event.*;

public class GameServerFrame extends JFrame {
	private serverThread serverTh;
	private JTextArea textArea;
	private JButton makeBtn;
	private JButton startBtn;
	private boolean recruitFlag;
	private Container c;
	private ArrayList<BufferedReader> inArr;
	private ArrayList<BufferedWriter> outArr;
	private ArrayList<String> playerNames;
	private int playerCap;
	private GameThread gameTh;
	
	GameServerFrame(){
		recruitFlag = true;
		setBounds(300, 300, 300, 300);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setLayout(new FlowLayout());
		c = getContentPane();
		c.setBackground(Color.white);
		
		makeBtn = new JButton("Create");
		makeBtn.setFocusPainted(false);
		makeBtn.setFont(new Font("Helvetica", Font.PLAIN, 20));
		makeBtn.setContentAreaFilled(false);
		makeBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				JButton btn = (JButton)e.getSource();
				btn.setEnabled(false);
				makeServer();
			}
		});
		makeBtn.addMouseListener(new FocusListener());
		c.add(makeBtn);
		
		textArea = new JTextArea(7, 20);
		textArea.setEditable(false);
		c.add(textArea);
		
		startBtn = new JButton("Start");
		startBtn.setFocusPainted(false);
		startBtn.setFont(new Font("Helvetica", Font.PLAIN, 15));
		startBtn.setContentAreaFilled(false);
		startBtn.addMouseListener(new FocusListener());
		startBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				JButton btn = (JButton)e.getSource();
				if(recruitFlag) recruitFlag = false;
				btn.setEnabled(false);
				textArea.setText(textArea.getText()+"게임을 시작합니다!!\n");
				for(int i=0; i<outArr.size(); i++) {
					try {
						playerCap = playerNames.size();
						//플레이어 정보 전달
						var out = outArr.get(i);
						out.write(Integer.toString(i)+" "+ Integer.toString(outArr.size())+"\n");
						out.flush();
						
						//플레이어 이름 전달
						StringBuilder names = new StringBuilder();
						for(int j=0; j<playerCap- 1; j++) names.append(playerNames.get(j)+" ");
						names.append(playerNames.get(playerNames.size()-1)+"\n");
						out.write(names.toString());
						out.flush();
					}catch(IOException e1) {
						e1.getStackTrace();
					}
				}
				gameTh = new GameThread();
				gameTh.start();
			}
		});
		c.add(startBtn);
		
		setVisible(true);
	}
	
	class FocusListener extends MouseAdapter{
		@Override
		public void mouseEntered(MouseEvent e) {
			JButton btn = (JButton)e.getSource();
			btn.setForeground(Color.red);
		}
		@Override
		public void mouseExited(MouseEvent e) {
			JButton btn = (JButton)e.getSource();
			btn.setForeground(Color.black);
		}
	}
	void makeServer() {
		serverTh = new serverThread();
		serverTh.start();
	}
	
	class serverThread extends Thread{
		ServerSocket listener;
		Socket[] socket;
		private int cnt;
		serverThread(){
			inArr = new ArrayList<BufferedReader>();
			outArr = new ArrayList<BufferedWriter>();
			playerNames = new ArrayList<String>();
			listener = null; socket = null;
			socket = new Socket[4];
			cnt = 0;
		}
		@Override
		public void run() {
			try {
				listener = new ServerSocket(9999);
				textArea.setText(textArea.getText()+"연결 대기중...\n");
				while(recruitFlag) {
					socket[cnt] = listener.accept();
					inArr.add(new BufferedReader(new InputStreamReader(socket[cnt].getInputStream())));
					outArr.add(new BufferedWriter(new OutputStreamWriter(socket[cnt].getOutputStream())));
					String name = inArr.get(cnt).readLine();
					for(String tmpName : playerNames) {
						if(tmpName.equals(name)) {
							startBtn.setEnabled(false);
							textArea.setText(textArea.getText()+"중복되는 이름이 있으면 안됩니다...\n");
							textArea.setText(textArea.getText()+"잠시 후 종료합니다..\n");
							try {
								Thread.sleep(new Random().nextInt(2000)+1000);
							}catch(InterruptedException e) {
								e.getStackTrace();
							}
							System.exit(0);
						}
					}
					playerNames.add(name);
					textArea.setText(textArea.getText()+playerNames.get(cnt)+" 연결 완료!!\n");
					cnt++;
					if(cnt == 4) recruitFlag = false;
				}
			}catch(IOException e) {
				e.getStackTrace();
			}			
		}
	}
	
	private class GameThread extends Thread{
		@Override
		public void run() {
			
			//플레이어 순서 섞기
			sendCommand(getShuffleCommand());
			
			//시작턴 정하기
			String setStartTurn = getStartTurn();
			
			int nowTurn = Integer.parseInt(setStartTurn.split(" ")[1]);
			int startTurn = nowTurn;
			
			int gameCount = 4;
			sendCommand("Log 게임 시작!!");
			
			//게임 사이클
			while(gameCount-- > 0) {
				//시작 턴 설정
				sendCommand("StartTurn "+--startTurn);
				sendCommand("SetRound "+(4-gameCount));
				if(startTurn == -1) startTurn = 3;
				nowTurn--;
				if(nowTurn == -1) nowTurn = 3;
				//블록 선택
				sendCommand("SelectBlock");
				for(int i=0; i<playerCap; i++) {
					try {
						inArr.get(i).readLine();
					}catch(IOException e) {
						e.getStackTrace();
					}
				}
				sendCommand("SelectBlockAI",0);
				
				
				sendCommand("Log "+(4-gameCount)+"번째 라운드!");
				//한 턴 진행
				for(int j=0; j<6*4; j++) {
					sendCommand("NowPlay "+nowTurn);
					sendCommand("SetIcon Start "+nowTurn);
					boolean playerFlag = false;
					int playingIdx = -1;
					for(int i=0; i<playerCap; i++) {
						try {
							if(inArr.get(i).readLine().equals("MyTurn")) {
								playerFlag = true;
								playingIdx = i;
							}
						}catch(IOException e) {
							e.getStackTrace();
						}
					}
					if(playerFlag) {
						sendCommand("YourTurn", playingIdx);
						try {
							String result = inArr.get(playingIdx).readLine();
							for(int i=0; i<playerCap; i++) {
								if(i != playingIdx) {
									sendCommand("Add "+result, i);
								}
							}
							sleep(100);
							String addLog = inArr.get(playingIdx).readLine();
							sendCommand("Log "+addLog);
						}catch(IOException e) {
							e.getStackTrace();
						}catch(InterruptedException e) {
							e.getStackTrace();
						}
					}else { //AI턴
						sendCommand("PlayAI "+nowTurn,0);
						try{
							String result = inArr.get(0).readLine();	
							for(int i=1; i<playerCap; i++) {
								//playerIdx, boardIdx, CityIdx, height
								sendCommand("Add "+result, i);
							}
							sleep(100);
							String addLog = inArr.get(0).readLine();
							for(int i=1; i<playerCap; i++)
								sendCommand("Log "+addLog, i);
						}catch(IOException e) {
							e.getStackTrace();
						}catch(InterruptedException e) {
							e.getStackTrace();
						}
					}
					sendCommand("PlaySound turnEnd");
					sendCommand("SetIcon End "+nowTurn);
					nowTurn--;
					if(nowTurn == -1) nowTurn = 3;
				}
				sendCommand("PlaySound roundEnd");
				sendCommand("Log "+(4-gameCount)+"번째 라운드 종료!");
				sendCommand("Log ------------- 점 수 ---------------");
				//점수 계산
				sendCommand("CalcScore",0);
				try {
					int stackCap = Integer.parseInt(inArr.get(0).readLine());
					for(int i=0; i<stackCap; i++) {
						String log = inArr.get(0).readLine();
						sendCommand("Log "+log);
					}					
					//점수 할당
					sendCommand("GetScore", 0);
					int scoreLen = Integer.parseInt(inArr.get(0).readLine());
					int[] scores = new int[scoreLen];
					for(int i=0; i<scoreLen; i++)
						scores[i] = Integer.parseInt(inArr.get(0).readLine());
					sendCommand("SetScore "+scores[0] + " " +scores[1]+" "+scores[2]+" "+scores[3]);
					sendCommand("GetHighest", 0);
					int highest = Integer.parseInt(inArr.get(0).readLine().split(" ")[1]);
					sendCommand("SetHighest "+highest);
				} catch (NumberFormatException e) {
					e.printStackTrace();
				} catch (IOException e) {
					e.printStackTrace();
				};
				sendCommand("Log ---------------------------------");
			}
			sendCommand("ShowResult");
			sendCommand("END Game");
		}
	}
	
	void sendCommand(String command) {
		try {
			for(BufferedWriter out : outArr) {
				out.write(command+"\n");
				out.flush();
			}			
		}catch(IOException e) {
			e.getStackTrace();
		}
	}
	void sendCommand(String command, int idx) {
		try {
			outArr.get(idx).write(command+"\n");
			outArr.get(idx).flush();
		}catch(IOException e) {
			e.getStackTrace();
		}
	}
	String getNowTurn(int turn) {
		return new String("NowPlay "+turn);
	}
	
	String getStartTurn() {
		Random rand = new Random();
		int ret = rand.nextInt(4);
		return new String("StartTurn "+ret);
	}
	
	String getShuffleCommand() {
		ArrayList<Integer> tmpInteger = new ArrayList<Integer>();
		for(int i=0; i<4; i++) tmpInteger.add(i);
		java.util.Collections.shuffle(tmpInteger);
		StringBuilder shuffleCommand = new StringBuilder("Shuffle ");
		for(int i=0; i<3; i++) shuffleCommand.append(tmpInteger.get(i)+" ");
		shuffleCommand.append(tmpInteger.get(3));
		return shuffleCommand.toString();
	}
}
