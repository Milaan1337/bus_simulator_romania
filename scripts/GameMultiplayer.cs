using Godot;
using System;

public class GameMultiplayer : Node2D
{

	public Viewport firstviewport;
	public Camera2D firstcamera;
	public KinematicBody2D firstplayer;
	public Viewport secondviewport;
	public Camera2D secondcamera;
	public KinematicBody2D secondplayer;

	
	public override void _Ready()
	{
		firstplayer = GetNode("VBoxContainer/ViewportContainer/Viewport/Cars/KinematicBody2D") as KinematicBody2D;
		firstplayer.Set("type","player1");
		GD.Print(firstplayer);
		/*
		firstviewport = GetNode("$HBoxContainer/ViewportContainer/Viewport") as Viewport;
		firstcamera = GetNode("$HBoxContainer/ViewportContainer/Viewport/Camera2D") as Camera2D;
		secondviewport = GetNode("$HBoxContainer/ViewportContainer2/Viewport") as Viewport;
		secondcamera = GetNode("$HBoxContainer/ViewportContainer2/Viewport/Camera2D") as Camera2D;
		secondplayer = GetNode("$HBoxContainer/ViewportContainer/Viewport/CarMultiPlayer") as KinematicBody2D;

		firstviewport.World2d = secondviewport.World2d;
		var remote_transform = new RemoteTransform2D();
*/
	}

//  public override void _Process(float delta)
//  {
//      
//  }
}
