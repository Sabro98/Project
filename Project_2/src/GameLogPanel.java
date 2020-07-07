import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.event.*;

public class GameLogPanel extends JPanel{
	private JTextArea logArea;
	private JScrollPane scroll;
	public GameLogPanel() {
		setSize(425, 320);
		setBackground(Color.white);
		Border border = new LineBorder(Color.black, 2, true);
		setBorder(border);
		logArea = new JTextArea(16, 37);
		logArea.setEditable(false);
		logArea.setFont(new Font("Helvetica", Font.PLAIN, 12));
		logArea.setSize(getX(), getY());
		scroll = new JScrollPane(logArea);
		scroll.setBorder(null);
		add(scroll);
	}	
	void addLog(String log) {
		logArea.setText(logArea.getText()+log+"\n");
		updateScroll();
	}
	void addLog(Stack<String> log) {
		while(!log.isEmpty())
			logArea.setText(logArea.getText()+log.pop()+"\n");
		updateScroll();
	}
	void updateScroll() {
		logArea.setCaretPosition(logArea.getDocument().getLength());
	}
}
