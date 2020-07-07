import java.awt.*;
import java.util.*;
import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;

import java.awt.event.*;
import java.awt.image.BufferedImage;

import javax.swing.event.*;

public class CityBlockBtn extends JButton{
	private int[] playerBuilding;
	private int owner;
	private ArrayList<String> playersName;
	private ArrayList<Color> playerColors;
	private int latestOwner;
	
	public CityBlockBtn(ArrayList<Player> players) {
		playersName = new ArrayList<String>();
		for(int i=0; i<players.size(); i++) playersName.add(players.get(i).getName());
		playerBuilding = new int[4];
		this.setOpaque(true);
		owner = -1;
		playerColors = new ArrayList<Color>();
		addColors();
		Border border = new LineBorder(Color.GRAY, 1, true);
		setBorder(border);
	}
	
	void addColors() {
		playerColors.add(Color.green);
		playerColors.add(Color.blue);
		playerColors.add(Color.pink);
		playerColors.add(Color.yellow);		
	}
	
	void tempAddBlock(int playerIndex, int height) {
		playerBuilding[playerIndex] += height;
		latestOwner = owner;
		owner = playerIndex;
	}
	void MinusBlock(int playerIndex, int height) {
		playerBuilding[playerIndex] -= height;
		owner = latestOwner;
	}
	void addBlock(int playerIndex, int height) {
		if(playerIndex != -1) {			
			playerBuilding[playerIndex] += height;
			owner = playerIndex;
			this.setText(Integer.toString(playerBuilding[owner])); 
			setBackground(playerColors.get(owner)); //사용자의 색으로 block의 background를 설정
		}
	}
	
	int getMaxHeight() {
		if(owner != -1)
			return playerBuilding[owner];
		return 0;
	} 
	int getIndexHeight(int index) {
		return playerBuilding[index];
	}
	void canNotClick() {
		try {
			Thread.sleep(300);
		}catch(InterruptedException e) {
			e.getStackTrace();
		}
	}
	int getHighestBuilding() {
		if(owner != -1)
			return playerBuilding[owner];
		return -1;
	}
	int getOwner() {
		return owner;
	}
	void setOwnerBackground() {
		if(owner != -1)
			setBackground(playerColors.get(owner));
		else
			setBackground(Color.white);
	}
	boolean isAbleCity(int playerIndex, int height) {
		if(owner != -1)
			return playerBuilding[playerIndex] + height >= playerBuilding[owner];
		return true;
	}
	
	int getPlayerBuildingHeight(int idx) {
		return playerBuilding[idx];
	}
	void renewPlayersName(ArrayList<Player> players) {
		playersName.clear();
		for(Player player : players) 
			playersName.add(player.getName());
	}
	void infoPopUp() {
		new InfoDialog();
	}
	class InfoDialog extends JFrame{
		private JLabel[] labels;
		InfoDialog(){
			setBounds(100, 100, 50, 150);
			labels = new JLabel[4];
			setLayout(new GridLayout(4,1));
			for(int i=0; i<4; i++) {
				labels[i] = new JLabel(playersName.get(i)+" : "+playerBuilding[i]);
				if(i == owner) labels[i].setForeground(Color.red);
				getContentPane().add(labels[i]);
			}
			
			setVisible(true);
		}
	}
}
