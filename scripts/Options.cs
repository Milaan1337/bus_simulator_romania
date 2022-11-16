using Godot;
using System;

public class Options : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public override void _Input(InputEvent esemeny)
    {
        if (Input.IsActionJustPressed("back"))
        {
            GetTree().ChangeScene("res://scenes/Menu.tscn");
        }
    }
    public void _on_TextureButton_pressed()
    {
        GetTree().ChangeScene("res://scenes/Menu.tscn");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
