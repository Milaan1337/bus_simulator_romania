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
    private Label endlabel;
    private int timebase = 10;
    
    public override void _Ready()
    {
        allVariable = new AllVariable();
        string text = File.ReadAllText(@"save/times.json");
        var get_options = JsonConvert.DeserializeObject<ConfigBody>(text);
        endlabel = GetNode("/root/Timeout/endlabel") as Label;
        timelabel = GetNode("Earn") as Label;
        personal_record = GetNode("personal_record") as Label;
        float nemegesz = (float)timebase / (float)allVariable.maplength;
        timelabel.Text = $"Your time: {((float)timebase / (float)allVariable.maplength) * allVariable.time} sec";
        personal_record.Text = $"Your best time: {((float)timebase / (float)allVariable.maplength) * get_options.max_sec} sec";
        endlabel.Text = $"Legközelebb biztos jobban sikerül.";
        }
    public void _on_BackToMenu_pressed() 
    {
        GetTree().ChangeScene("res://scenes/Menu.tscn");
    }


//  public override void _Process(float delta)
//  {
//      
//  }
}
