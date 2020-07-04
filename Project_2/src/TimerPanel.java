import java.awt.*;
import java.util.*;
import javax.swing.*;
import java.awt.event.*;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

import javax.swing.event.*;

public class TimerPanel extends JPanel implements Runnable{
	private JLabel timeLabel;
	private int remainTimeInit;
	private float remainTime;
	private Thread th;
	private JLabel clockLabel;
	public TimerPanel() {
		setSize(70, 100);
		setBackground(Color.white);
		try {					
			File file = new File("text/TimeLimit.txt"); 
			if(file.exists()) { 
				BufferedReader inFile = new BufferedReader(new FileReader(file)); 
				remainTimeInit = Integer.parseInt(inFile.readLine()); 
				inFile.close();
			}
			}catch(IOException e) {
				remainTimeInit = 30;
			}
		remainTime = remainTimeInit;
		timeLabel = new JLabel(String.format("%.2f", (float)remainTimeInit));
		timeLabel.setFont(new Font("Helvetica", Font.BOLD, 25));
		
		clockLabel = new JLabel(new ImageIcon("images/timer.png"));
		
		add(clockLabel);
		add(timeLabel);
	}
	
	void runTimer() {
		th = new Thread(this);
		th.start();
	}
	
	void stopTimer() {
		remainTime = 0;
	}
	
	boolean isThAlive() {
		return th.isAlive();
	}
			
	@Override
	public void run() {
		remainTime = remainTimeInit;
		timeLabel.setText(String.format("%.2f", (float)remainTimeInit));
		while(remainTime > 0) {
			if(remainTime < 5) timeLabel.setForeground(Color.red);
			timeLabel.setText(String.format("%.2f", remainTime));
			remainTime -= 0.04;
			try {
				Thread.sleep(40);
			}catch(InterruptedException e) {
				e.getStackTrace();
			}
		}
		timeLabel.setForeground(Color.black);
		timeLabel.setText(String.format("%.2f", (float)remainTimeInit));
	}	
}
