using Godot;
using Newtonsoft.Json;
using System;
using System.IO;
using File = System.IO.File;

public class Timeout : Node2D
{
    public Label earn;
    public override void _Ready()
    {
        string text = File.ReadAllText(@"save/options.json");
        var get_options = JsonConvert.DeserializeObject<ConfigBody>(text);
        earn = GetNode("Earn") as Label;
        earn.Text = $"You earned ${get_options.Money_in_game}";
    }
    public void _on_BackToMenu_pressed() 
    {
        GetTree().ChangeScene("res://scenes/Menu.tscn");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
