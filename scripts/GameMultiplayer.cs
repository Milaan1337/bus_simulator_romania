using Godot;
using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using File = System.IO.File;

public class GameMultiplayer : Node2D
{
	public AllVariable allVariable;
	public KinematicBody2D firstplayer;
	public TextureProgress firstcarnitrous;
	public TextureProgress secondnitrous;
	public TextureProgress firsthp;
	public TextureProgress secondhp;
	public AudioStreamPlayer2D music;
	public AudioStreamPlayer2D engine;
	public Panel pausepanel;
	public Label time;
	public Timer timer;
	public TimeSpan t;



	public string text;

	
	public override void _Ready()
	{
		firstplayer = GetNode("VBoxContainer/ViewportContainer/Viewport/Cars/KinematicBody2D") as KinematicBody2D;
		firstplayer.Set("type","player1");
		GD.Print(firstplayer);
		
		firstcarnitrous = GetNode("VBoxContainer/ViewportContainer/HUD/NitrousBar") as TextureProgress;
		secondnitrous = GetNode("VBoxContainer/ViewportContainer2/HUD/NitrousBar") as TextureProgress;
		firsthp = GetNode("VBoxContainer/ViewportContainer/HUD/HpBar") as TextureProgress;
		secondhp = GetNode("VBoxContainer/ViewportContainer2/HUD/HpBar") as TextureProgress;
		music = GetNode("VBoxContainer/ViewportContainer/HUD/Music") as AudioStreamPlayer2D;
		engine = GetNode("VBoxContainer/ViewportContainer/HUD/Engine") as AudioStreamPlayer2D;
		pausepanel = GetNode("VBoxContainer/ViewportContainer/HUD/PausePanel") as Panel;
		time = GetNode("VBoxContainer/ViewportContainer2/HUD/Time") as Label;
		timer = GetNode("VBoxContainer/Timer") as Timer;
		
		text = File.ReadAllText(@"save/options.json");
		var get_options = JsonConvert.DeserializeObject<ConfigBody>(text);
		music.VolumeDb = get_options.MusicVolume;
		engine.VolumeDb = get_options.SoundEffectVolume;
		allVariable = new AllVariable();
	}

	public void _on_return_pressed()
	{
		pausepanel.Visible = false;
		allVariable.pauseon = false;
		GetTree().Paused = false;
	}

	public void _on_quit_pressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeScene("res://scenes/Menu.tscn");
	}

	public override void _Process(float delta)
	{
		t = TimeSpan.FromSeconds((int)timer.WaitTime - (int)timer.TimeLeft);
		time.Text = Convert.ToString(t);
	}
}
