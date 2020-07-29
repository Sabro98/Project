package Flicking;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*;

public class GameFrame extends JFrame {
	private GamePanel gamePanel = new GamePanel();

	GameFrame() {
		setBounds(150, 150, 500, 522);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		add(gamePanel);

		setVisible(true);
	}

	public static void main(String[] args) {
		new GameFrame();
	}
}
