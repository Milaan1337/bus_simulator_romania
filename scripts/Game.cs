using Godot;
using Godot.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using File = System.IO.File;

public class Game : Node2D
{
	[Export] public int speed = 5;

	[Export] public int rotationspeed = 1;
	public Label Fps;
	public bool fps_is_on = false;
	public AudioStreamPlayer2D engine;
	public AudioStreamPlayer2D music;
	public Area2D busstop;
	public Node2D bus;
	public Timer timer;
	public Label time;
	public int money;
	public TimeSpan t;
    public string text = File.ReadAllText(@"save/options.json");

    public override void _Ready()
	{
        var get_options = JsonConvert.DeserializeObject<ConfigBody>(text);

        JObject options = new JObject(
            new JProperty("MainVolume", (int)get_options.MainVolume),
            new JProperty("MusicVolume", (int)get_options.MusicVolume),
            new JProperty("UIVolume", (int)get_options.UIVolume),
            new JProperty("SoundEffectVolume", (int)get_options.SoundEffectVolume),
            new JProperty("Money", (int)get_options.Money + money),
            new JProperty("Fps_is_on", (bool)get_options.fps_is_on),
            new JProperty("Money_in_game", (int)money),
            new JProperty("Name", (string)get_options.Name));

        File.WriteAllText(@"save/options.json", options.ToString());

        using (StreamWriter file = File.CreateText(@"save/options.json"))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            options.WriteTo(writer);
        }

        bus = GetNode("Bus") as Node2D;
		engine = GetNode("Bus/Engine") as AudioStreamPlayer2D;
		music = GetNode("Bus/Music") as AudioStreamPlayer2D;
		busstop = GetNode("/root/Game/Bus_stop/Area2D") as Area2D;
		timer = GetNode("Timer") as Timer;
		time = GetNode("Bus/HUD/Time") as Label;

        if (get_options.fps_is_on)
		{
			fps_is_on = true;
			Fps = GetNode("Bus/HUD/Fps") as Label;
		}
		else
		{
			fps_is_on = false;
		}
		music.VolumeDb = get_options.MusicVolume;
		engine.VolumeDb = get_options.SoundEffectVolume;
	}
	public override void _Input(InputEvent esemeny)
	{
		if (Input.IsActionJustPressed("back"))
		{
			GetTree().ChangeScene("res://scenes/Menu.tscn");
		}
	}
	public override void _Process(float delta)
	{
		if (fps_is_on == true)
		{
			Fps.Text = $"{1 / delta} FPS";
		}
        t = TimeSpan.FromSeconds((int)timer.WaitTime - (int)timer.TimeLeft);
		time.Text = Convert.ToString(t);
    }
}
