using Godot;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using File = System.IO.File;

public class Bus_stop : Node2D
{
	public Vector2 position;
	public Area2D finish;
	public Node2D car;
	public Node2D car2;
	public KinematicBody2D car_body;
	public KinematicBody2D car2_body;
	public Sprite circle;
	public Random rng;
	public Timer timer;
	public int t;
	public int max_sec;
	private AllVariable allVariable;
    public override void _Ready()
	{
		setPos();
	}

	public void setPos()
	{
		allVariable = new AllVariable();
		position = new Vector2(-((allVariable.maplength * 8.28125f) *64), 0);
		//GD.Print(allVariable.maplength);
		finish = GetNode("Area2D") as Area2D;
		circle = GetNode("Sprite") as Sprite;
		car = GetNode("/root/Game/Car") as Node2D;
		timer = GetNode("/root/Game/Timer") as Timer;
		car_body = car.GetNode("KinematicBody2D") as KinematicBody2D;

		finish.Position = position;
		circle.Position = position;
	}

	public void _on_Area2D_body_entered(object body)
	{	
			string text = File.ReadAllText(@"save/times.json");
			var get_options = JsonConvert.DeserializeObject<ConfigBody>(text);
			allVariable = new AllVariable();
			setPos();
			//GD.Print(timer.WaitTime - timer.TimeLeft);
			t = (int)timer.WaitTime - (int)timer.TimeLeft;
			GD.Print(t);
			allVariable.time = t;
			if (get_options.max_sec > t) { max_sec = t; }else { max_sec = get_options.max_sec; }
			game_end();		
	}
	public void game_end()
	{
		string text = File.ReadAllText(@"save/times.json");
		var get_options = JsonConvert.DeserializeObject<ConfigBody>(text);
		JObject options = new JObject(
				new JProperty("max_sec", (int)max_sec),
				new JProperty("last_sec", (int)t));
			File.WriteAllText(@"save/times.json", options.ToString());

			using (StreamWriter file = File.CreateText(@"save/times.json"))
			using (JsonTextWriter writer = new JsonTextWriter(file))
			{
				options.WriteTo(writer);
			}

		GetTree().ChangeScene("res://scenes/Timeout.tscn");
	}

	public override void _Process(float delta)
	{
	}
}



