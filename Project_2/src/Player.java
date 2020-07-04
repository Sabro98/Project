import java.awt.*;
import javax.swing.*;
import java.awt.event.*;
import javax.swing.event.*;
import java.util.*;

public class Player extends JLabel {
	private ImageIcon img;
	protected ArrayList<String> BuildingCardSet;
	protected HashMap<String, Integer> BuildingBlockSet;
	private ArrayList<String> BuildingBlockPathList;	
	private int allocatedSpot;
	
	public Player() {
		BuildingCardSet = new ArrayList<String>();
		BuildingBlockSet = new HashMap<String, Integer>();
		BuildingBlockPathList = new ArrayList<String>();
		allocatedSpot = -1;
		
		//BuildingBlock√ﬂ∞°
		for(int i=1; i<=4; i++) BuildingBlockPathList.add("images/BuildingBlock"+i+".png");
		BuildingBlockSet.put(BuildingBlockPathList.get(0), 12);
		BuildingBlockSet.put(BuildingBlockPathList.get(1), 6);
		BuildingBlockSet.put(BuildingBlockPathList.get(2), 4);
		BuildingBlockSet.put(BuildingBlockPathList.get(3), 2);
		
		this.setHorizontalAlignment(SwingConstants.CENTER);
		
	}
	
	void setImage(int index) {
		
	}
	
	void setImage(String imgPath) {
		img = new ImageIcon(imgPath);
		this.setIcon(img);
		this.setToolTipText(getName());
	}
	void setSpot(int s) {
		allocatedSpot = s;
	}
	int getSpot() {
		return allocatedSpot;
	}
	void AddBuildingCard(String buildingCard) {
		BuildingCardSet.add(buildingCard);
	}
	void setBuildingBlockSet(HashMap<String, Integer> block) {
		BuildingBlockSet = block;
	}
	void removeCard(String path) {
		for(int i=0; i<BuildingCardSet.size(); i++) {
			if(BuildingCardSet.get(i).equals(path)) {
				BuildingCardSet.remove(i);
				break;
			}
		}
	}
	void setPlayingIcon() {
		this.setIcon(new ImageIcon("images/player.png"));
	}
	void setEndIcon() {
		this.setIcon(img);
	}
	ArrayList<String> getBuildingCardSet(){
		return BuildingCardSet;
	}
	HashMap<String, Integer> getBuildingBlockSet(){
		return BuildingBlockSet;
	}
	String getBuildingCardIdx(int idx) {
		return BuildingCardSet.get(idx);
	}
}
