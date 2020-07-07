import java.net.*;
import java.io.*;
import java.util.*;
import java.awt.*;
import javax.swing.*;
import java.awt.event.*;
import javax.swing.event.*;


public class GameClientFrame extends JFrame{
	private Container c;
	private JLabel nameLabel;
	private JTextField nameTextField;
	private JTextField textField;
	private JButton joinBtn;
	private JTextArea joinLog;
	private BufferedReader in;
	private BufferedWriter out;
	private String address;
	private joinThread joinTh;
	private GameFrame gameFrame;
	private String name;
	private int playerIdx;
	private int playerCap;
	
	GameClientFrame(){
		setBounds(300, 300, 270, 300);
		c = getContentPane();
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setLayout(new FlowLayout());
		
		nameLabel = new JLabel("Name : ");
		nameLabel.setFont(new Font("Helvetica", Font.PLAIN, 15));
		c.add(nameLabel);
		
		nameTextField = new JTextField(10);
		nameTextField.setText("GamePlayer"+new Random().nextInt(100));
		c.add(nameTextField);
		
		textField = new JTextField(10);
		c.add(textField);
		
		joinBtn = new JButton("Join!");
		joinBtn.setFocusPainted(false);
		joinBtn.setFont(new Font("Helvetica", Font.PLAIN, 15));
		joinBtn.setContentAreaFilled(false);
		joinBtn.addMouseListener(new MouseAdapter() {
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
		});
		joinBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				JButton btn = (JButton)e.getSource();
				joinTh = new joinThread();
				joinTh.start();
				btn.setEnabled(false);
			}
		});
		c.add(joinBtn);
		
		joinLog = new JTextArea(7, 20);
		joinLog.setEditable(false);
		c.add(joinLog);
		
		in = null; out = null;
		address = new String();

		c.setBackground(Color.white);
		setVisible(true);
	}
	
	class joinThread extends Thread {
		private Socket socket;
		joinThread(){
			socket = null;
		}
		@Override
		public void run() {			
			try {
				joinLog.setText(joinLog.getText()+"접속 시도....\n");
				address = textField.getText();
				socket = new Socket(address, 9999);
				textField.setEnabled(false);
				in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
				out = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
				name = nameTextField.getText();
				joinLog.setText(joinLog.getText()+address+"접속 완료!!\n");
				address = new String();
				out.write(nameTextField.getText()+"\n");
				nameTextField.setEditable(false);
				textField.setEditable(false);
				out.flush();
				
				//게임 시작
				String[] commands = in.readLine().split(" ");
				playerIdx = Integer.parseInt(commands[0]);
				playerCap = Integer.parseInt(commands[1]);
				String[] names = in.readLine().split(" ");
				gameFrame = new GameFrame(playerIdx, playerCap, names, in, out);
				dispose();
				//게임 시작 method
				new GameThread().start();
			}catch(IOException e) {
				in = null;
				joinLog.setText(joinLog.getText()+"접속 실패!!\n");
				joinLog.setText(joinLog.getText()+"실패 주소 : "+address+"\n");
				address = new String();
				textField.setText("");
				joinBtn.setEnabled(true);
			}
		}
	}
	
	class GameThread extends Thread{
		private String command;
		private GamePanel gamePanel;
		GameThread(){
			command = new String();
			gamePanel = gameFrame.getGamePanel();
		}
		@Override
		public void run() {
			try {
				while(!(command = in.readLine()).equals("END Game")) {
					if(command.contains("Shuffle")) gamePanel.shufflePlayers(shufflePlayers(gamePanel.getGamePlayers(), gamePanel.getAIPlayers(), command));
					else if(command.contains("SelectBlock")) {
						if(command.contains("AI")) {
							ArrayList<Player> players = gamePanel.getPlayers();
							for(int i=0; i<4; i++) {
								if(players.get(i).getClass() == AIPlayer.class) {
									((AIPlayer)players.get(i)).selectBlock();
								}
							}
						}else {							
							gamePanel.selectPlayerBlock();
							sendMessage("BlockComplete");
						}
					}
					else if(command.contains("YourTurn")) {
						String result = gamePanel.multiPlayerPlay(playerIdx);
						//result를 가지고 다른 곳에 블록을 놓음
						//index, board, city, height 정보
						sendMessage(result);
						
						String resultLog = gamePanel.getMultiPlayerLog(playerIdx);
						sendMessage(resultLog);
					}else if(command.contains("PlayAI")) {
						String result = gamePanel.multiAIPlay(getAIIndex(command));
						sendMessage(result);
						sendMessage(gamePanel.getmultiAILog(getAIIndex(command)));
					}
					else if(command.contains("SetIcon")) {
						if(command.contains("Start")) gamePanel.setStartIcon(Integer.parseInt(command.split(" ")[2]));
						else gamePanel.setEndIcon(Integer.parseInt(command.split(" ")[2]));
							
					}
					else if(command.contains("StartTurn")) gamePanel.setMultiStartTurn(getTurn(command));
					else if(command.contains("SetRound")) gamePanel.setRound(command.split(" ")[1]);
					else if(command.contains("NowPlay")) {
						gamePanel.setMultiNowTurn(getTurn(command));
						if(getTurn(command) == playerIdx) sendMessage("MyTurn");
						else sendMessage("NotTurn");
					}
					else if(command.contains("Add ")) gamePanel.addMultiAction(getAddInfoList(command));
					else if(command.contains("Log")) gamePanel.addLog(filterLogCommand(command));
					else if(command.contains("CalcScore")) {
						Stack<String> scores = gamePanel.getCities().calcScore(gamePanel.getScore(), gamePanel.getPlayers());
						sendMessage(Integer.toString(scores.size()));
						while(!scores.isEmpty())
							sendMessage(scores.pop());
					}else if(command.contains("GetScore")) {
						int[] scores = gamePanel.getScore();
						sendMessage(Integer.toString(scores.length));
						for(int score : scores) sendMessage(Integer.toString(score));
					}else if(command.contains("SetScore")) {
						String[] commands = command.split(" ");
						int[] scores = new int[4];
						for(int i=1; i<=4; i++) 
							scores[i-1] = Integer.parseInt(commands[i]);
						gamePanel.setScore(scores);
					}else if(command.contains("ShowResult")) {
						gamePanel.calcWinner();
					}else if(command.contains("PlaySound")) {
						if(command.contains("turnEnd")) 
							gamePanel.playSound("TurnEnd");
						else
							gamePanel.playSound("RoundEnd");
					}else if(command.contains("GetHighest")) {
						int highest = gamePanel.getHighest();
						sendMessage("Highest "+highest);
					}else if(command.contains("SetHighest")) {
						gamePanel.getUIPanel().setHighestBuilding(Integer.parseInt(command.split(" ")[1]));
					}else {
						System.out.println("존재 안하는 명령어");
						System.out.println(command);
					}
				}
			}catch(IOException e) {
				e.getStackTrace();
			}
		}
	}
	
	String filterLogCommand(String command) {
		command = command.replace("Log ", "");
		return command;
	}
	
	int getAIIndex(String command) {
		String[] commands = command.split(" ");
		int ret = Integer.parseInt(commands[1]);
		return ret;
	}
	
	synchronized void sendMessage(String message) {
		try {
			out.write(message+"\n");
			out.flush();
		}catch(IOException e) {
			e.getStackTrace();
		}
	}
	
	String[] getAddInfoList(String command) {
		String[] splitCmd = command.split(" ");
		String[] ret = new String[splitCmd.length-1];
		for(int i=0; i<splitCmd.length-1; i++) {
			ret[i] = splitCmd[i+1];
		}
		return ret;
	}
	
	int getTurn(String command) {
		String[] commands = command.split(" ");
		return Integer.parseInt(commands[1]);
	}
	
	ArrayList<Player> shufflePlayers(ArrayList<GamePlayer> originalGP, ArrayList<AIPlayer> originalAI, String command){
		ArrayList<Player> original = new ArrayList<Player>();
		for(int i=0; i<originalGP.size(); i++) original.add(originalGP.get(i));
		for(int i=0; i<originalAI.size(); i++) original.add(originalAI.get(i));
		ArrayList<Player> ret = new ArrayList<Player>();
		String[] commands = command.split(" ");
		for(int i=0; i<4; i++) ret.add(original.get(Integer.parseInt(commands[i+1])));
		for(int i=0; i<ret.size(); i++) {			
			ret.get(i).setImage("images/player"+i+".png");
			ret.get(i).setSpot(i);
		}
		
		for(int i=0; i<ret.size(); i++) {
			if(ret.get(i).getName().equals(name)) {
				playerIdx = i;
				break;
			}
		}
		
		return ret;
	}
}
