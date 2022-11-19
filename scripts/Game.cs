using Godot;
using System;

public class Game : Node2D
{
    [Export] public int speed = 5;

    [Export] public int rotationspeed = 1;
    public override void _Ready()
    {
        
    }
    public override void _Input(InputEvent esemeny)
    {
        if (Input.IsActionJustPressed("back"))
        {
            GetTree().ChangeScene("res://scenes/Menu.tscn");
        }
        if (Input.IsActionPressed("left"))
        {

        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
