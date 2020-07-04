import java.awt.*;

import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;

import java.awt.event.*;
import java.awt.image.BufferedImage;
import java.util.ArrayList;

import javax.swing.event.*;

public class CityBoard extends JPanel {
	private ImageIcon img;
	private JLabel imgLabel;
	private String Defaultpath;
	private int width, height;
	private CityBlockBtn[] buttons;
	private int playerIndex, playerHeight;
	private String logStr;
	private String multiLogStr;
	private ArrayList<Player> players;
	public CityBoard(ArrayList<Player> players) {
		setLayout(new GridLayout(3, 3, 1, 1));
		width = 118; height = 110;
		setSize(width, height);
		buttons = new CityBlockBtn[9];
		playerIndex = -1;
		playerHeight = -1;
		logStr = null;
		multiLogStr = null;
		this.players = players;
		Border border = new LineBorder(Color.GRAY, 2, true);
		setBorder(border);
		
		for(int i=0; i<9 ;i++) {
			buttons[i] = new CityBlockBtn(players);
			buttons[i].setBackground(Color.white);
			buttons[i].setOpaque(true);
			buttons[i].setEnabled(false);
			buttons[i].setName(Integer.toString(i));
			add(buttons[i]);
			
			buttons[i].addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					CityBlockBtn btn = (CityBlockBtn)e.getSource();
					if(playerIndex != -1 && btn.getMaxHeight() <= btn.getIndexHeight(playerIndex) + playerHeight) {						
						btn.addBlock(playerIndex, playerHeight);
						btn.setEnabled(false);
						logStr = new String(players.get(playerIndex).getName()+" : "+(Integer.toString(Integer.parseInt(getName())+1)+"번째 Block의 ( "
								+((Integer.parseInt(btn.getName()))/3) +", "+((Integer.parseInt(btn.getName()))%3)+" ) "
								+"번째 도시에 "+btn.getPlayerBuildingHeight(playerIndex)+"층 건물을 쌓음"));
						multiLogStr = new String(playerIndex + " " + getName() + " " + btn.getName()) + " " + playerHeight;
						playerIndex = -1;
						playerHeight = -1;
					}
				}
			});
			
			buttons[i].addMouseListener(new MouseAdapter() {
				@Override
				public void mousePressed(MouseEvent e) {
					CityBlockBtn btn = (CityBlockBtn)e.getSource();
					if(!btn.isEnabled())
						btn.infoPopUp();
				}
				
			});
		}
	}
	
	void setActionListener() {
		for(int i=0; i<9; i++) {
			buttons[i].addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					CityBlockBtn btn = (CityBlockBtn)e.getSource();
					if(btn.getMaxHeight() <= btn.getIndexHeight(playerIndex) + playerHeight) {						
						btn.setBackground(Color.white);
						btn.addBlock(playerIndex, playerHeight);
						logStr = new String(players.get(playerIndex).getName()+" : "+(Integer.toString(Integer.parseInt(getName())+1)+"번째 Block의 ( "
												+((Integer.parseInt(btn.getName()))/3) +", "+((Integer.parseInt(btn.getName()))%3)+" ) "
													+"번째 도시에 "+btn.getPlayerBuildingHeight(playerIndex)+"층 건물을 쌓음"));
						multiLogStr = new String(playerIndex + " " + getName() + " " + btn.getName()) + " " + playerHeight;
						playerIndex = -1;
						playerHeight = -1;
						btn.setEnabled(false);
					}
				}
			});
		}
	}
	void setPlayers(ArrayList<Player> players) {
		this.players = players;
	}
	void setBtnEnable(int index, int playerIndex, int playerHeight) {
		buttons[index].setEnabled(true);
		if(isAbleCity(index, playerIndex, playerHeight))
			buttons[index].setBackground(Color.red);
		else
			buttons[index].setBackground(Color.black);
		this.playerIndex = playerIndex;
		this.playerHeight = playerHeight;
	}
	
	void clickBtn(int index) {
		CityBlockBtn btn = buttons[index];
		if(btn.getBackground() == Color.red) {
			btn.setOwnerBackground();
			btn.addBlock(playerIndex, playerHeight);
			logStr = new String(players.get(playerIndex).getName()+" : "+(Integer.toString(Integer.parseInt(getName())+1)+"번째 Block의 ( "
					+((Integer.parseInt(btn.getName()))/3) +", "+((Integer.parseInt(btn.getName()))%3)+" ) "
					+"번째 도시에 "+btn.getPlayerBuildingHeight(playerIndex)+"층 건물을 쌓음"));
			multiLogStr = new String(playerIndex + " " + getName() + " " + btn.getName()) + " " + playerHeight;
			playerIndex = -1;
			playerHeight = -1;
			btn.setEnabled(false);
		}
	}
	
	
	String popMultiLog() {
		String tmp = multiLogStr;
		multiLogStr = null;
		return tmp;
	}
	String getMultiLog() {
		return multiLogStr;
	}
	
	boolean isAbletoClick(int index) {
		boolean flag;
		if(buttons[index].getBackground() == Color.red)
			flag = true;
		else
			flag = false;
		return flag;
	}
	void setBtnDefault(int index) {
		buttons[index].setEnabled(false);
		buttons[index].setOwnerBackground();
	}
	boolean getIsClicked(int index) {
		return !(buttons[index].isEnabled());
	}
	void tempChooseCity(int index, int playerIndex, int height) {
		buttons[index].tempAddBlock(playerIndex, height);
	}
	void minusChooseCity(int index, int playerIndex, int height) {
		buttons[index].MinusBlock(playerIndex, height);
	}
	void chooseCity(int index, int playerIndex, int height) {
		buttons[index].addBlock(playerIndex, height);
	}
	boolean isEnableBtn(int cityIdx) {
		return buttons[cityIdx].isEnabled();
	}
	boolean isAbleCity(int index, int playerIndex, int height) {
		return buttons[index].isAbleCity(playerIndex, height);
	}
	CityBlockBtn[] getButtons() {
		return buttons;
	}
	String popLog() {
		String ret = new String();
		ret = logStr;
		logStr = null;
		return ret;
	}
	String getLog() {
		return logStr;
	}
	void clearLog() {
		logStr = null;
		multiLogStr = null;
	}
	void renewPlayersName(ArrayList<Player> players) {
		for(CityBlockBtn botton : buttons) botton.renewPlayersName(players);
	}
}
