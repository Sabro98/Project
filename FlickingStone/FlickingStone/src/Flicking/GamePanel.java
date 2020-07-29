package Flicking;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*;

public class GamePanel extends JPanel {
	private final String BACKGROUND_COL = "#E1D385";
	private final int WIDTH = 500;
	private final int HEIGHT = 500;
	private final Point WHITE_POINT = new Point(250, 95);
	private final Point BLACK_POINT = new Point(250, 405);
	private Stone blackStone;
	private Stone whiteStone;

	GamePanel() {
		setSize(WIDTH, HEIGHT);
		setBackground(Color.decode(BACKGROUND_COL));
		this.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				System.out.print("x : " + e.getX() + ", ");
				System.out.println("y : " + e.getY());
			}
		});
		setLayout(null);
		setStones();
	}
	
	private void setStones() {
		blackStone = new Stone("images/BlackStone.png");
		blackStone.setBounds(BLACK_POINT.x - blackStone.getWidth()/2, BLACK_POINT.y - blackStone.getHeight()/2,
				blackStone.getWidth(), blackStone.getHeight());
		add(blackStone);
		
		whiteStone = new Stone("images/WhiteStone.png");
		whiteStone.setBounds(WHITE_POINT.x - whiteStone.getWidth()/2, WHITE_POINT.y - whiteStone.getHeight()/2
				, whiteStone.getWidth(), whiteStone.getHeight());
		add(whiteStone);
		
	}
	@Override
	public void paintComponent(Graphics g) {
		super.paintComponent(g);
		for (int x = 50, y = 0; x < getWidth(); x += 50) {
			g.drawLine(x, y, x, y + getHeight());
		}
		for (int y = 50, x = 0; y < getHeight(); y += 50) {
			g.drawLine(x, y, x + getWidth(), y);
		}
	}
}
