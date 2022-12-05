using Godot;
using System;

public class Nitrous : Node2D
{
	public AllVariable allVariable;
	public TextureProgress nitrousbar;
	public TextureProgress nitrousbar2;
	public Node2D car;
	public KinematicBody2D car_body;
	public Node2D car2;
	public KinematicBody2D car_body2;
	public override void _Ready()
	{
		allVariable = new AllVariable();
		if (allVariable.singleplay)
		{
			nitrousbar = GetNode("../Car/HUD/NitrousBar") as TextureProgress;
			car = GetNode("/root/Game/Car") as Node2D;
			car_body = car.GetNode("KinematicBody2D") as KinematicBody2D;
		}
		else
		{
			nitrousbar2 = GetNode("/root/Game/VBoxContainer/ViewportContainer/HUD/NitrousBar") as TextureProgress; 
			nitrousbar = GetNode("/root/Game/VBoxContainer/ViewportContainer2/HUD/NitrousBar") as TextureProgress; 
			car = GetNode("/root/Game/VBoxContainer/ViewportContainer/Viewport/Cars") as Node2D;
			car_body = car.GetNode("KinematicBody2D") as KinematicBody2D;
			car2 = GetNode("/root/Game/VBoxContainer/ViewportContainer2/Viewport/CarMulti") as Node2D;
			car_body2 = car2.GetNode("KinematicBody2D") as KinematicBody2D;
		}
	}
	public void _on_Nitrous_body_entered(object body)
	{
		switch (body)
		{
			case var value when value == car_body:
				allVariable.nitrous = 100;
				nitrousbar.Value = allVariable.nitrous;
				GD.Print("kaka");
				break;
			
			case var value when value == car_body2:
				allVariable.nitrous2 = 100;
				nitrousbar2.Value = allVariable.nitrous2;
				break;
		}
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

