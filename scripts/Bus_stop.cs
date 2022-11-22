using Godot;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using File = System.IO.File;

public class Bus_stop : Node2D
{
	public float x;
	public float y;
	public Vector2 position;
	public Area2D bus_stop;
	public Node2D bus;
	public KinematicBody2D bus_body;
	public Sprite circle;
	public Random rng;
	public int money;
    public Label moneylabel;
    public override void _Ready()
	{
		setPos();
        moneylabel = GetNode("../Money") as Label;
    }
	public void _on_Timer_timeout()
    {
		game_end();
    }

	public void setPos(){
		rng = new Random();
		x = rng.Next(0,400);
		y = rng.Next(-400,0);
		position = new Vector2(x,y);
		bus_stop = GetNode("Area2D") as Area2D;
		circle = GetNode("Sprite") as Sprite;
		bus = GetNode("/root/Game/Bus") as Node2D;
		bus_body = bus.GetNode("KinematicBody2D") as KinematicBody2D;
		bus_stop.Position = position;
		circle.Position = position;
	}

	public void _on_Area2D_body_entered(object body)
	{
		if (body == bus_body){
			setPos();
			GD.Print("cigo");
			money += 325;
		}else{
			GD.Print("nem cigo");
		}
	}
	public void game_end()
	{
		string text = File.ReadAllText(@"save/options.json");
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
		money = 0;
		GetTree().ChangeScene("res://scenes/Timeout.tscn");
	}

	public override void _Process(float delta)
	{
        string textinsec = File.ReadAllText(@"save/options.json");
        var options = JsonConvert.DeserializeObject<ConfigBody>(textinsec);
        moneylabel.Text = Convert.ToString(money);
    }
}



