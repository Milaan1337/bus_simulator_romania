using Godot;
using System;

public class Nitrous : Node2D
{
	public AllVariable allVariable;
	public TextureProgress nitrousbar;
	public override void _Ready()
	{
		allVariable = new AllVariable();
		nitrousbar = GetNode("../Car/HUD/NitrousBar") as TextureProgress;
	}
	public void _on_Nitrous_body_entered(object body)
	{
		allVariable.nitrous = 100;
		nitrousbar.Value = allVariable.nitrous;
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

