import java.awt.*;
import java.io.*;
import javax.swing.*;
import java.awt.event.*;
import java.io.InputStreamReader;

import javax.swing.event.*;
import java.util.*;

public class MainFrame extends JFrame {
	private JButton singleBtn;
	private JButton multiMakeBtn;
	private JButton multiJoinBtn;
	private JButton settingBtn;
	private JButton exitBtn;
	private Container c;
	private ArrayList<JButton> btns;
	MainFrame(){
		super("ManhattanGame");
		setBounds(300, 300, 300, 300);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		c = getContentPane();
		c.setLayout(new GridLayout(5, 1, 3, 3));
		btns = new ArrayList<JButton>();
		c.setBackground(Color.white);
		
		
		singleBtn = new JButton("Single Mode");
		singleBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				dispose();
				String[] name = new String[1];
				//사용자 이름
				try {					
					File file = new File("text/PlayerName.txt"); 
					if(file.exists()) { 
						BufferedReader inFile = new BufferedReader(new FileReader(file)); 
						name[0] = inFile.readLine(); 
						inFile.close();
					}
				}catch(IOException e1) {
					name[0] = "GamePlayer";
				}
				new GameFrame(0, 1, name).startGame();
			}
		});
		
		c.add(singleBtn);
		
		multiMakeBtn = new JButton("Create GameServer");
		multiMakeBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				dispose();
				new GameServerFrame();
			}
		});
		c.add(multiMakeBtn);
		
		multiJoinBtn = new JButton("Join Game");
		multiJoinBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				dispose();
				new GameClientFrame();
			}
		});
		c.add(multiJoinBtn);
		
		settingBtn = new JButton("Setting");
		settingBtn.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				new SettingFrame();
			}
		});
		c.add(settingBtn);
		
		exitBtn = new JButton("Exit");
		exitBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				System.exit(0);
			}
		});
		c.add(exitBtn);
		
		btns.add(singleBtn);
		btns.add(multiMakeBtn);
		btns.add(multiJoinBtn);
		btns.add(settingBtn);
		btns.add(exitBtn);
		
		for(JButton btn : btns) {
			btn.setFont(new Font("Helvetica", Font.PLAIN, 20));
			btn.setContentAreaFilled(false);
			btn.setBorder(null);
			btn.setFocusPainted(false);
			btn.addMouseListener(new MouseAdapter() {
				@Override
				public void mouseEntered(MouseEvent e) {
					btn.setForeground(Color.red);
				}
				@Override
				public void mouseExited(MouseEvent e) {
					btn.setForeground(Color.black);
				}
			});
		}
		
		
		setVisible(true);
	}
	public static void main(String[] args) {
		new MainFrame();
	}
}
