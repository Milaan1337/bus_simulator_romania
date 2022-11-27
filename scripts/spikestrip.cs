using Godot;
using System;

public class spikestrip : Node2D
{
    public AllVariable allVariable;
    public TextureProgress hpbar;
    public Camera2D camera;
    public int shake = 1;
    public Random rnd;
    public int first;
    public int second;

    public override void _Ready()
    {
        hpbar = GetNode("../Car/HUD/HpBar") as TextureProgress;
        camera = GetNode("/root/Game/Car/KinematicBody2D/Camera2D") as Camera2D;
        allVariable = new AllVariable();
    }
    public void _on_Area2D_body_entered(object body)
    {
        allVariable.hp -= 34;
        hpbar.Value = allVariable.hp;
        rnd = new Random();
        first = rnd.Next(-100, 100);
        second = rnd.Next(-100, 100);
    }

    public override void _Process(float delta)
    {
        if (allVariable.hp <= 0)
        {
            GetTree().ChangeScene("res://scenes/Timeout.tscn");
        }
        //camera.SetOffset(new Vector2(first * shake, second * shake));
    }
}
