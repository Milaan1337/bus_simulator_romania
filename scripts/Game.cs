using Godot;
using Godot.Collections;
using Newtonsoft.Json;
using System;
using System.IO;
using File = System.IO.File;

public class Game : Node2D
{
	[Export] public int speed = 5;

	[Export] public int rotationspeed = 1;
	public Label Fps;
	public bool fps_is_on = false;
	public AudioStreamPlayer2D opendoor;
	public AudioStreamPlayer2D engine;
	public AudioStreamPlayer2D music;
	public Sprite arrow;
	public Area2D busstop;
	public Node2D bus;

	public override void _Ready()
	{
		string text = File.ReadAllText(@"save/options.json");
		var options = JsonConvert.DeserializeObject<ConfigBody>(text);

        bus = GetNode("Bus") as Node2D;

        opendoor = GetNode("Bus/OpenDoor") as AudioStreamPlayer2D;
		engine= GetNode("Bus/Engine") as AudioStreamPlayer2D;
		music= GetNode("Bus/Music") as AudioStreamPlayer2D;
		arrow = GetNode("Arrow") as Sprite;
		busstop = GetNode("/root/Game/Bus_stop/Area2D") as Area2D;
        if (options.fps_is_on)
		{
			fps_is_on = true;
			Fps = GetNode("Fps") as Label;
		}
		else
		{
			fps_is_on = false;
		}
		music.VolumeDb = options.MusicVolume;
		engine.VolumeDb = options.SoundEffectVolume;
		opendoor.VolumeDb = options.SoundEffectVolume;
	}
	public override void _Input(InputEvent esemeny)
	{
		if (Input.IsActionJustPressed("back"))
		{
			GetTree().ChangeScene("res://scenes/Menu.tscn");
		}
		if (Input.IsActionJustPressed("door_open"))
		{
			GD.Print("ajto nyitva!");
			opendoor.Play();
		}
	}
	public override void _Process(float delta)
	{
		if (fps_is_on == true)
		{
			Fps.Text = $"{1 / delta} FPS";
		}
        arrow.LookAt(busstop.Position);
    }
}
