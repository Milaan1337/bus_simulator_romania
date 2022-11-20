using Godot;
using System;

public class Game : Node2D
{
    [Export] public int speed = 5;

    [Export] public int rotationspeed = 1;
    Camera2D camera;
    public override void _Ready()
    {
        camera = GetNode("Camera2D") as Camera2D;
    }
    public override void _Input(InputEvent esemeny)
    {
        if (Input.IsActionJustPressed("back"))
        {
            GetTree().ChangeScene("res://scenes/Menu.tscn");
        }
    }
}