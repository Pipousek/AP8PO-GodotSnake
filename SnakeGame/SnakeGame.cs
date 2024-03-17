using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

enum Direction
{
	UP,
	DOWN,
	LEFT,
	RIGHT
}

public class SnakeGame : Node
{
	const int ScreenWidth = 22;
	const int ScreenHeight = 22;
	const int InitialScore = 5;
	const int MovementIntervalMilliseconds = 500;

	Random random = new Random();
	int score;
	bool gameOver;
	List<int> xPositions = new List<int>();
	List<int> yPositions = new List<int>();
	int berryX;
	int berryY;
	Direction direction = Direction.RIGHT;
	Direction lastDirection = Direction.RIGHT;
	TileMap tileMap;
	
	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("TileMap");
		InitializeGame();
		StartCustomCoroutine();
	}

	public override void _Process(float delta) 
	{
		HandleInput();
	}
	
	private async void CustomCoroutine()
	{
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		MoveSnake();
		CheckCollisions();
		StartCustomCoroutine();
	}

	private void StartCustomCoroutine()
	{
		if (gameOver == false) {
			CustomCoroutine();
		} else {
			DisplayGameOver();
		}
	}

	void InitializeGame()
	{
		score = InitialScore;
		gameOver = false;

		xPositions.Clear();
		yPositions.Clear();

		xPositions.Add(ScreenWidth / 2);
		yPositions.Add(ScreenHeight / 2);

		PlaceBerry();
		DrawFrame();
	}
	
	void PlaceBerry()
	{
		berryX = random.Next(1, ScreenWidth - 2);
		berryY = random.Next(1, ScreenHeight - 2);
		DrawBerry();
	}
	
	void DrawBerry()
	{
		tileMap.SetCell(berryX, berryY , 14);
	}
	
	void DrawFrame()
	{
		DrawBorders();
	}
	
	void DrawBorders()
	{
		for (int i = 0; i < ScreenWidth; i++)
		{
			tileMap.SetCell(i, 0, 15);
			tileMap.SetCell(i, ScreenHeight - 1, 15);
		}
		for (int i = 1; i < ScreenHeight - 1; i++)
		{
			tileMap.SetCell(0, i, 15);
			tileMap.SetCell(ScreenWidth - 1, i, 15);
		}
	}
	
	void DrawSnake()
	{
		switch (direction) {
			case Direction.UP:
				tileMap.SetCell(xPositions[0], yPositions[0], 4);
				break;
			case Direction.DOWN:
				tileMap.SetCell(xPositions[0], yPositions[0], 8);
				break;
			case Direction.LEFT:
				tileMap.SetCell(xPositions[0], yPositions[0], 7);
				break;
			case Direction.RIGHT:
				tileMap.SetCell(xPositions[0], yPositions[0], 3);
				break;
		}
	}
	
	void MoveSnake()
	{
		int newX = xPositions[0];
		int newY = yPositions[0];

		switch (direction)
		{
			case Direction.UP:
				newY--;
				tileMap.SetCell(newX, newY, 4);
				break;
			case Direction.DOWN:
				newY++;
				tileMap.SetCell(newX, newY, 8);
				break;
			case Direction.LEFT:
				newX--;
				tileMap.SetCell(newX, newY, 7);
				break;
			case Direction.RIGHT:
				newX++;
				tileMap.SetCell(newX, newY, 3);
				break;
		}

		xPositions.Insert(0, newX);
		yPositions.Insert(0, newY);
		
		if (xPositions.Count > 2)
		{
			int xSize = xPositions.Count;
			int ySize = yPositions.Count;
			
			if (xSize > score) {
				tileMap.SetCell(xPositions[xSize - 1], yPositions[ySize - 1], -1);
				xPositions.RemoveAt(xSize - 1);
				yPositions.RemoveAt(ySize - 1);
			}
			
			int newpostailX = xPositions[xSize - 2];
			int newpostailY = yPositions[ySize - 2];
			int befnewpostailX = xPositions[xSize - 3];
			int befnewpostailY  = yPositions[ySize - 3];

			if (befnewpostailY > newpostailY) {
				tileMap.SetCell(newpostailX, newpostailY, 13);
			} else if (befnewpostailY < newpostailY) {
				tileMap.SetCell(newpostailX, newpostailY, 10);
			} else if (befnewpostailX > newpostailX) {
				tileMap.SetCell(newpostailX, newpostailY, 11);
			} else if (befnewpostailX < newpostailX) {
				tileMap.SetCell(newpostailX, newpostailY, 12);
			}
			
			
			if (lastDirection == direction) {
				if (direction == Direction.UP || direction == Direction.DOWN) {
					tileMap.SetCell(xPositions[1], yPositions[1], 6);
				} else {
					tileMap.SetCell(xPositions[1], yPositions[1], 1);
				}
			} else {
				if (xPositions[1] == xPositions[0]) {
					if (lastDirection == Direction.RIGHT) {
						if (direction == Direction.UP) {
							tileMap.SetCell(xPositions[1], yPositions[1], 9);
						} else {
							tileMap.SetCell(xPositions[1], yPositions[1], 2);
						}
					} else if (lastDirection == Direction.LEFT) {
						if (direction == Direction.UP) {
							tileMap.SetCell(xPositions[1], yPositions[1], 5);
						} else {
							tileMap.SetCell(xPositions[1], yPositions[1], 0);
						}
					}
				} else if (yPositions[1] == yPositions[0]) {
					if (lastDirection == Direction.UP) {
						if (direction == Direction.RIGHT) {
							tileMap.SetCell(xPositions[1], yPositions[1], 0);
						} else {
							tileMap.SetCell(xPositions[1], yPositions[1], 2);
						}
					} else if (lastDirection == Direction.DOWN) {
						if (direction == Direction.RIGHT) {
							tileMap.SetCell(xPositions[1], yPositions[1], 5);
						} else {
							tileMap.SetCell(xPositions[1], yPositions[1], 9);
						}
					}
				}
			}
		}
		lastDirection = direction;
	}
	
	void CheckCollisions()
	{
		int headX = xPositions[0];
		int headY = yPositions[0];

		if (headX == 0 || headX == ScreenWidth - 1 || headY == 0 || headY == ScreenHeight - 1)
			gameOver = true;

		for (int i = 1; i < xPositions.Count; i++)
		{
			if (headX == xPositions[i] && headY == yPositions[i])
			{
				gameOver = true;
				break;
			}
		}

		if (headX == berryX && headY == berryY)
		{
			score++;
			PlaceBerry();
		}
	}
	
	void HandleInput()
	{
		if (Input.IsActionPressed("ui_right")) {
			if (lastDirection != Direction.LEFT)
				direction = Direction.RIGHT;
		}
		if (Input.IsActionPressed("ui_left")) {
			if (lastDirection != Direction.RIGHT)
				direction = Direction.LEFT;
		}
		if (Input.IsActionPressed("ui_down")) {
			if (lastDirection != Direction.UP)
				direction = Direction.DOWN;
		}
		if (Input.IsActionPressed("ui_up")) {
			if (lastDirection != Direction.DOWN)
				direction = Direction.UP;
		}
	}
	
	void DisplayGameOver()
	{
		GD.Print("Game Over");
	}
}
