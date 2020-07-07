import java.util.ArrayList;

public class GamePlayer extends Player{
	private String selectedBlock;
	private ArrayList<String> selectedList;
	private GamePanel panel;
	public GamePlayer(GamePanel panel) {
		selectedBlock = new String();
		selectedList = new ArrayList<String>();
		this.panel = panel;
	}
	void addSelectedBlock(String block) {
		selectedList.add(block);
	}
	void removeSelectedBlock(String block) {
		selectedList.remove(block);
	}
	ArrayList<String> getSelectedList() {
		return selectedList;
	}
	void setBlockHeight(String cityBlock) {
		selectedBlock = cityBlock;
	}
	int getBlockHeight() {
		int height = 0;
		if(selectedBlock.contains("1")) height = 1;
		if(selectedBlock.contains("2")) height = 2;
		if(selectedBlock.contains("3")) height = 3;
		if(selectedBlock.contains("4")) height = 4;
		return height;
	}
	void PlayerPlay() {
		GamePlayer player = this;
		PlayerUIPanel uiPanel = panel.getUIPanel();
		player.setPlayingIcon();
		uiPanel.startTurn();
		while(uiPanel.getTurnFlag()) {
			try {
				Thread.sleep(10);
			}catch(InterruptedException e) {
				e.getStackTrace();
			}
		}
		player.setEndIcon();
		while(panel.getCities().getLog() == null) {
			try {
				Thread.sleep(10);							
			}catch(InterruptedException e) {
				return;
			}
		}
		panel.getLogPanel().addLog(panel.getCities().popLog());
	}
}
