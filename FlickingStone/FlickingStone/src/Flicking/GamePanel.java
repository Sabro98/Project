package Flicking;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*;

public class GamePanel extends JPanel {
	private final String BACKGROUND_COL = "#E1D385";
	private final int WIDTH = 500;
	private final int HEIGHT = 500;
	private final String BLACK_STONE_PATH = "images/Blackstone.png";
	private final String WHITE_STONE_PATH = "images/WhiteStone.png";
	private final Point WHITE_INIT_POINT = new Point(250, 95);
	private final Point BLACK_INIT_POINT = new Point(250, 405);
	
	private Stone blackStone;
	private Stone whiteStone;
	private Point centerPoint;
	private Point endPoint;
	private boolean drawDirection;


	GamePanel() {
		setSize(WIDTH, HEIGHT);
		setBackground(Color.decode(BACKGROUND_COL));
		setLayout(null);
		setStones();
//		this.addMouseListener(new MouseAdapter() {
//			@Override
//			public void mouseClicked(MouseEvent e) {
//				System.out.print("x : " + e.getX() + ", ");
//				System.out.println("y : " + e.getY());
//			}
//		});
		drawDirection = false;
	}
	
	void setDrawDirection(boolean drawDirection) {
		this.drawDirection = drawDirection;
	}

	private void setStones() { // set stones information
		blackStone = new Stone(BLACK_STONE_PATH, BLACK_INIT_POINT, "up", this);
		whiteStone = new Stone(WHITE_STONE_PATH, WHITE_INIT_POINT, "down", this);
		add(blackStone);
		add(whiteStone);
		
		blackStone.addMouseMotionListener(new stoneDirectionListener(blackStone));
		whiteStone.addMouseMotionListener(new stoneDirectionListener(whiteStone));
	}

	@Override
	public void paintComponent(Graphics g) { // draw black line in boards
		super.paintComponent(g);
		for (int x = 50, y = 0; x < getWidth(); x += 50) {
			g.drawLine(x, y, x, y + getHeight());
		}
		for (int y = 50, x = 0; y < getHeight(); y += 50) {
			g.drawLine(x, y, x + getWidth(), y);
		}
		if(drawDirection) {
			g.setColor(Color.gray);
			g.drawLine((int)centerPoint.getX(), (int)centerPoint.getY(), 
					(int)endPoint.getX(), (int) endPoint.getY());
		}
	}
	
	class stoneDirectionListener implements MouseMotionListener{
		private Stone stone;
		public stoneDirectionListener(Stone stone) {
			this.stone = stone;
		}
		@Override
		public void mouseMoved(MouseEvent e) {
			// TODO Auto-generated method stub
		}
		
		@Override
		public void mouseDragged(MouseEvent e) {
			centerPoint = stone.getCenter();
			endPoint = new Point(e.getX() + stone.getX(), e.getY() + stone.getY());
			if(endPoint.getY() > centerPoint.getY() + 100) 
				endPoint = new Point((int) endPoint.getX(), (int) centerPoint.getY() + 100);
			if(endPoint.getY() < centerPoint.getY() - 100) 
				endPoint = new Point((int) endPoint.getX(), (int) centerPoint.getY() - 100);
			if(endPoint.getX() > centerPoint.getX() + 100)
				endPoint = new Point((int) centerPoint.getX() + 100, (int) endPoint.getY());
			if(endPoint.getX() < centerPoint.getX() - 100)
				endPoint = new Point((int) centerPoint.getX() - 100, (int) endPoint.getY());
			setDrawDirection(true);
			repaint();
		}
	}
}
