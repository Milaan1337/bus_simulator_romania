using Godot;
using System;

public class Bus : Node2D
{
    [Export] public int speed = 1;
    [Export] public float rotationspeed = 0.5f;
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("left"))
        {
            GlobalRotation -= delta * rotationspeed;
        }

        if (Input.IsActionPressed("right"))
        {
            GlobalRotation += delta * rotationspeed;
        }
    }
}
