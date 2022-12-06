using Godot;
using Newtonsoft.Json;
using System;
using System.IO;
using File = System.IO.File;

public class Timeout : Node2D
{
    public Label timelabel;
    public Label personal_record;
    private AllVariable allVariable;
    private Label winner;
    
    public override void _Ready()
    {
        allVariable = new AllVariable();
        string text = File.ReadAllText(@"save/times.json");
        var get_options = JsonConvert.DeserializeObject<ConfigBody>(text);
        timelabel = GetNode("Earn") as Label;
        personal_record = GetNode("personal_record") as Label;
        timelabel.Text = $"Your time: {TimeSpan.FromSeconds(allVariable.time)}";
        personal_record.Text = $"Your best time: {TimeSpan.FromSeconds((get_options.max_sec))}";   
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
