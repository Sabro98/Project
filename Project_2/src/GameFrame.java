import javax.swing.*;
import java.io.*;
import java.awt.*;

public class GameFrame extends JFrame {
	private GamePanel gamePanel;
	private PlayerUIPanel UI;
	private GameLogPanel gameLogPanel;
	private TimerPanel timerPanel;
	
	public GameFrame(int gamePlayerIdx, int playerNum, String[] playerName) {
		super("ManhattanGame");
		Container c = this.getContentPane();
		setBounds(100, 50, 1100, 690);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.setLayout(null);
		this.setResizable(false);
		c.setBackground(Color.white);
		
		gamePanel = new GamePanel(gamePlayerIdx, playerNum, playerName);
		gamePanel.setBounds(5, 0, gamePanel.getWidth(), gamePanel.getHeight());
		c.add(gamePanel);
		
		UI = gamePanel.getUIPanel();
		UI.setBounds(5, 360, UI.getWidth(), UI.getHeight());
		c.add(UI);
		
		timerPanel = gamePanel.getTimerPanel();
		timerPanel.setBounds(485, 150, timerPanel.getWidth(), timerPanel.getHeight());
		add(timerPanel);

		gameLogPanel = gamePanel.getLogPanel();
		gameLogPanel.setBounds(650, 25, gameLogPanel.getWidth(), gameLogPanel.getHeight());
		add(gameLogPanel);
		
		
		setVisible(true);
	}
	
	public GameFrame(int gamePlayerIdx, int playerNum, String[] playerName, BufferedReader in, BufferedWriter out) {
		this(gamePlayerIdx, playerNum, playerName);
		gamePanel.setIO(in, out);
	}
	void startGame() {
		gamePanel.startGame();
	}
	GamePanel getGamePanel() {
		return gamePanel;
	}
}
