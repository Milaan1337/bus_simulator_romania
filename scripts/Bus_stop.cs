using Godot;
using System;

public class Bus_stop : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public float x;
	public float y;
	public Vector2 position;
	public Area2D bus_stop;
	public Node2D bus;
	public KinematicBody2D bus_body;
	public Sprite circle;
	public Random rng;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		setPos();
	}

	public void setPos(){
		rng = new Random();
		x = rng.Next(0,400);
		y = rng.Next(-400,0);
		position = new Vector2(x,y);
		bus_stop = GetNode("Area2D") as Area2D;
		circle = GetNode("Sprite") as Sprite;
		bus = GetNode("/root/Game/Bus") as Node2D;
		bus_body = bus.GetNode("KinematicBody2D") as KinematicBody2D;
		bus_stop.Position = position;
		circle.Position = position;
	}

	public void _on_Area2D_body_entered(object body)
	{
		if (body == bus_body){
			setPos();
			GD.Print("cigo");
		}else{
			GD.Print("nem cigo");
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}



