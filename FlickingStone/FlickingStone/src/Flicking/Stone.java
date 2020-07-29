package Flicking;
import javax.swing.JLabel;
import javax.swing.ImageIcon;

public class Stone extends JLabel {
	private ImageIcon icon;
	
	Stone(String path){
		icon = new ImageIcon(path);
	}
}
