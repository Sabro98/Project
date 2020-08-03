package Flicking;

import java.awt.event.*;
import java.awt.*;
import javax.swing.*;
import javax.swing.event.*;

public class Stone extends JLabel {
	private final int FPS = 60;
	private final int ONE_SEC = 1000;
	private final int DELAY = ONE_SEC / FPS;
	private GamePanel panel;
	private int moveCnt;
	private int stonePower;
	private int moveSize;
	private int moveDirection;
	private ImageIcon icon;
	private Timer moveTimer;
	private boolean drawDirection;
	

	Stone(String path, Point init_point, String direction, GamePanel panel) {
		icon = new ImageIcon(path);
		setSize(icon.getIconWidth(), icon.getIconHeight());
		setIcon(icon);

		setLocation(init_point);
		setBounds(getX() - getWidth() / 2, getY() - getHeight() / 2, getWidth(), getHeight());
		setStoneMovement(direction);
		
		this.panel = panel;
	}

	private void setStoneMovement(String direction) {
		stonePower = 100;
		moveCnt = 0;
		moveSize = 15;
		if (direction.equalsIgnoreCase("up"))
			moveDirection = -1;
		else if (direction.equalsIgnoreCase("down"))
			moveDirection = 1;

		addMouseListener(new MouseAdapter() {
			@Override
			public void mouseReleased(MouseEvent e) {
				moveTimer.start();
				panel.setDrawDirection(false);
				panel.repaint();
			}
		});

		moveTimer = new Timer(DELAY, new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				moveCnt++;
				moveStone(0, moveDirection * moveSize);
				moveSize --;
				if (moveCnt == 15) {
					moveTimer.stop();
					moveCnt = 0;
					moveSize = 15;
				}
			}
		});
	}
	
	void setDrawDirection(boolean draw) {
		drawDirection = draw;
	}
	
	boolean getDrawDirection() {
		return drawDirection;
	}
	
	Point getCenter() {
		Point center = new Point(getX() + getWidth()/2, getY() + getHeight()/2);
		return center;
	}
	
	int getStonePower() {
		return stonePower;
	}

	void moveStone(int xSize, int ySize) {
		setLocation(getX() + xSize, getY() + ySize);
	}

	public void paintComponent(Graphics g) {
		super.paintComponent(g);
	}

}
