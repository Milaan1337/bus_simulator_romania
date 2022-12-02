using Godot;
using System;

public class GameMultiplayer : Node2D
{
	public KinematicBody2D firstplayer;

	
	public override void _Ready()
	{
		firstplayer = GetNode("VBoxContainer/ViewportContainer/Viewport/Cars/KinematicBody2D") as KinematicBody2D;
		firstplayer.Set("type","player1");
		GD.Print(firstplayer);

	}

//  public override void _Process(float delta)
//  {
//      
//  }
}
