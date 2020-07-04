import java.awt.*;
import java.io.*;
import javax.swing.*;
import java.awt.event.*;

import javax.swing.event.*;


import java.util.*;

public class SettingFrame extends JFrame {
	private Container cp;
	private JButton changeName;
	private JButton changeVolume;
	private JButton changeDelay;
	private ArrayList<JButton> btns;
	
	SettingFrame(){
		super("setting");
		setBounds(500, 300, 220, 250);
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		setLayout(new GridLayout(4, 1, 5, 5));
		cp = getContentPane();
		cp.setBackground(Color.white);
		btns = new ArrayList<JButton>();
		
		changeName = new JButton("Change Name");
		changeName.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				setContentPane(new NameSetting());
				revalidate();
				repaint();
			}
		});
		cp.add(changeName);
		btns.add(changeName);
		
		changeVolume = new JButton("Adjust Volume");
		changeVolume.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				setContentPane(new VolumeSetting());
				revalidate();
				repaint();
			}
		});
		cp.add(changeVolume);
		btns.add(changeVolume);

		changeDelay = new JButton("Set AIDelay");
		changeDelay.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				setContentPane(new DelaySetting());
				revalidate();
				repaint();
			}
		});
		add(changeDelay);
		btns.add(changeDelay);
		
		changeDelay = new JButton("Set TimeLimit");
		changeDelay.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				setContentPane(new TimeLimitSetting());
				revalidate();
				repaint();
			}
		});
		add(changeDelay);
		btns.add(changeDelay);
		
		for(JButton btn : btns) {
			btn.setFont(new Font("Helvetica", Font.PLAIN, 15));
			btn.setContentAreaFilled(false);
			btn.setBorder(null);
			btn.setFocusPainted(false);
			btn.addMouseListener(new MouseAdapter() {
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
		}
		
		setVisible(true);
	}
	
	class NameSetting extends JPanel{
		private JTextField nameField;
		private JLabel nameLabel;
		private JButton okBtn;
		private String originalName;
		NameSetting(){
				//사용자 이름
			try {					
				File file = new File("text/PlayerName.txt"); 
				if(file.exists()) { 
					BufferedReader inFile = new BufferedReader(new FileReader(file)); 
					originalName = inFile.readLine(); 
					inFile.close();
				}
			}catch(IOException e1) {
				originalName = "GamePlayer";
			}
			setBounds(500, 500, 100, 100);
			nameField = new JTextField(10);
			nameLabel = new JLabel("Name : ");
			nameField.setText(originalName);
			nameLabel.setFont(new Font("Helvetica", Font.PLAIN, 10));
			setBackground(Color.white);
			setOpaque(true);
			
			okBtn = new JButton("Okay");
			okBtn.setFont(new Font("Helvetica", Font.PLAIN, 15));
			okBtn.setContentAreaFilled(false);
			okBtn.addActionListener(new ActionListener() {
				
				@Override
				public void actionPerformed(ActionEvent e) {
					if(nameField.getText().equals("")) {
						JOptionPane.showMessageDialog(null, "이름을 입력해주세요!", "ERROR", JOptionPane.ERROR_MESSAGE);
					}else {						
						try {
							File file = new File("text/PlayerName.txt");
							if(file.exists()) {
								FileWriter writer = new FileWriter(file);
								writer.write(nameField.getText());
								writer.close();
								JOptionPane.showMessageDialog(null, "변경 완료!");
							}
						}catch(IOException e1) {
							e1.getStackTrace();
						}
						dispose();
					}
				}
			});
			setLayout(new FlowLayout());
			add(nameLabel);
			add(nameField);
			add(okBtn);
		}
	}
	
	class VolumeSetting extends JPanel{
		private JLabel sliderValue;
		private JSlider slider;
		private JButton okBtn;
		private int value;
		VolumeSetting(){
			try {					
			File file = new File("text/Volume.txt"); 
			if(file.exists()) { 
				BufferedReader inFile = new BufferedReader(new FileReader(file)); 
				value = (int)Float.parseFloat(inFile.readLine()); 
				inFile.close();
			}
			}catch(IOException e) {
				e.getStackTrace();
			}
			setBackground(Color.white);
			setOpaque(true);
			
			slider = new JSlider(JSlider.HORIZONTAL, -6, 6, value);
			slider.setMajorTickSpacing(6); //큰 눈금 간격 5로 설정
			slider.setMinorTickSpacing(1); //작은 눈금 간격 1로 설정
			slider.setPaintTicks(true); //눈금을 표시한다.
			slider.setPaintLabels(true); //값을 레이블로 표시한다.
			slider.setBackground(Color.white);
			slider.addChangeListener(new ChangeListener() {
				
				@Override
				public void stateChanged(ChangeEvent e) {
					sliderValue.setText(Integer.toString(slider.getValue()));
				}
			});
			
			sliderValue = new JLabel(Integer.toString(slider.getValue()));
			sliderValue.setFont(new Font("Helvetica", Font.BOLD, 10));
			
			add(sliderValue);
			add(slider);
			okBtn = new JButton("Okay");
			okBtn.setFont(new Font("Helvetica", Font.PLAIN, 15));
			okBtn.setContentAreaFilled(false);
			okBtn.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					try {
						File file = new File("Text/Volume.txt");
						FileWriter writer = new FileWriter(file);
						writer.write(Float.toString((float)slider.getValue()));
						writer.close();
					}catch(IOException e1) {
						e1.getStackTrace();
					}
					dispose();
				}
			});
			add(okBtn);
		}
	}
	class DelaySetting extends JPanel {
		private JSlider slider;
		private int value;
		private JLabel valueLabel;
		private JButton okBtn;
		DelaySetting(){
			try {					
				File file = new File("text/AIDelay.txt"); 
				if(file.exists()) { 
					BufferedReader inFile = new BufferedReader(new FileReader(file)); 
					value = (int)Float.parseFloat(inFile.readLine()); 
					inFile.close();
				}
				}catch(IOException e) {
					e.getStackTrace();
				}
			setBackground(Color.white);
			setOpaque(true);
			slider = new JSlider(JSlider.HORIZONTAL, 100, 1000, value);
			slider.setMajorTickSpacing(450); //큰 눈금 간격 5로 설정
			slider.setMinorTickSpacing(100); //작은 눈금 간격 1로 설정
			slider.setPaintTicks(true); //눈금을 표시한다.
			slider.setPaintLabels(true); //값을 레이블로 표시한다.
			slider.setBackground(Color.white);
			slider.addChangeListener(new ChangeListener() {
				
				@Override
				public void stateChanged(ChangeEvent e) {
					valueLabel.setText(Integer.toString(slider.getValue()) + " ms");
				}
			});
			valueLabel = new JLabel(Integer.toString(slider.getValue()) + " ms");
			valueLabel.setFont(new Font("Helvetica", Font.BOLD, 10));

			okBtn = new JButton("Okay");
			okBtn.setFont(new Font("Helvetica", Font.PLAIN, 15));
			okBtn.setContentAreaFilled(false);
			okBtn.addActionListener(new ActionListener() {
				
				@Override
				public void actionPerformed(ActionEvent e) {
					try {
						File file = new File("Text/AIDelay.txt");
						FileWriter writer = new FileWriter(file);
						writer.write(Integer.toString(slider.getValue()));
						writer.close();
					}catch(IOException e1) {
						e1.getStackTrace();
					}
					dispose();
				}
			});
			
			add(valueLabel);
			add(slider);
			add(okBtn);
		}
	}
	class TimeLimitSetting extends JPanel {
		private JSlider slider;
		private int value;
		private JLabel valueLabel;
		private JButton okBtn;
		TimeLimitSetting(){
			try {					
				File file = new File("text/TimeLimit.txt"); 
				if(file.exists()) { 
					BufferedReader inFile = new BufferedReader(new FileReader(file)); 
					value = (int)Float.parseFloat(inFile.readLine()); 
					inFile.close();
				}
				}catch(IOException e) {
					e.getStackTrace();
				}
			setBackground(Color.white);
			setOpaque(true);
			slider = new JSlider(JSlider.HORIZONTAL, 10, 30, value);
			slider.setMajorTickSpacing(10); //큰 눈금 간격 5로 설정
			slider.setMinorTickSpacing(5); //작은 눈금 간격 1로 설정
			slider.setPaintTicks(true); //눈금을 표시한다.
			slider.setBackground(Color.white);
			slider.setPaintLabels(true); //값을 레이블로 표시한다.
			slider.addChangeListener(new ChangeListener() {
				
				@Override
				public void stateChanged(ChangeEvent e) {
					valueLabel.setText(Integer.toString(slider.getValue()) + " sec");
				}
			});
			valueLabel = new JLabel(Integer.toString(slider.getValue())+" sec");
			valueLabel.setFont(new Font("Helvetica", Font.BOLD, 10));

			okBtn = new JButton("Okay");
			okBtn.setFont(new Font("Helvetica", Font.PLAIN, 15));
			okBtn.setContentAreaFilled(false);
			okBtn.addActionListener(new ActionListener() {
				
				@Override
				public void actionPerformed(ActionEvent e) {
					try {
						File file = new File("Text/TimeLimit.txt");
						FileWriter writer = new FileWriter(file);
						writer.write(Integer.toString(slider.getValue()));
						writer.close();
					}catch(IOException e1) {
						e1.getStackTrace();
					}
					dispose();
				}
			});
			add(valueLabel);
			add(slider);
			add(okBtn);
		}
	}
}
