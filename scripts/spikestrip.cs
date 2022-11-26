using Godot;
using System;

public class spikestrip : Node2D
{
    public AllVariable allVariable;
    public TextureProgress hpbar;
    public override void _Ready()
    {
        hpbar = GetNode("../Car/HUD/HpBar") as TextureProgress;
        allVariable = new AllVariable();
    }
    public void _on_Area2D_body_entered(object body)
    {
        allVariable.hp -= 50;
        hpbar.Value = allVariable.hp;
    }

    public override void _Process(float delta)
    {
        if (allVariable.hp == 0)
        {
            GetTree().ChangeScene("res://scenes/Timeout.tscn");
        }    
    }
}
