using Godot;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using File = System.IO.File;

public class Options : Node2D
{
    public HSlider MainVolume;
    public HSlider MusicVolume;
    public HSlider UIVolume;
    public HSlider SoundEffectVolume;
    public TextEdit NameInput;
    public int money;
    public bool fps_is_on;
    public CheckButton fps_is_onbutton;
    public AudioStreamPlayer2D click;
    public override void _Ready()
    {
        MainVolume = GetNode("MainVolume") as HSlider;
        MusicVolume = GetNode("MusicVolume") as HSlider;
        UIVolume = GetNode("UIVolume") as HSlider;
        SoundEffectVolume = GetNode("SoundEffectVolume") as HSlider;
        NameInput = GetNode("NameInput") as TextEdit;
        fps_is_onbutton = GetNode("fps_is_on") as CheckButton;
        click = GetNode("Click") as AudioStreamPlayer2D;


        string text = File.ReadAllText(@"save/options.json");
        var options = JsonConvert.DeserializeObject<ConfigBody>(text);

        MainVolume.Value = options.MainVolume;
        MusicVolume.Value = options.MusicVolume;
        UIVolume.Value = options.UIVolume;
        SoundEffectVolume.Value = options.SoundEffectVolume;
        NameInput.Text = options.Name;
        money = options.Money;
        fps_is_onbutton.Pressed = options.fps_is_on;

    }
    public void _on_SaveButton_pressed()
    {
        string text = File.ReadAllText(@"save/options.json");
        var optionsget = JsonConvert.DeserializeObject<ConfigBody>(text);
        click.Play();
        JObject options = new JObject(
            new JProperty("MainVolume", (int)MainVolume.Value),
            new JProperty("MusicVolume", (int)MusicVolume.Value),
            new JProperty("UIVolume", (int)UIVolume.Value),
            new JProperty("SoundEffectVolume", (int)SoundEffectVolume.Value),
            new JProperty("Money", (int)money),
            new JProperty("Fps_is_on", (bool)fps_is_onbutton.Pressed),
            new JProperty("Money_in_game", (int)optionsget.Money_in_game),
            new JProperty("Name", (string)NameInput.Text));

        File.WriteAllText(@"save/options.json", options.ToString());

        using (StreamWriter file = File.CreateText(@"save/options.json"))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            options.WriteTo(writer);
        }
    }

    public override void _Input(InputEvent esemeny)
    {
        if (Input.IsActionJustPressed("back"))
        {
            GetTree().ChangeScene("res://scenes/Menu.tscn");
        }
    }
    public void _on_TextureButton_pressed()
    {
        click.Play();
        GetTree().ChangeScene("res://scenes/Menu.tscn");
    }

    public override void _Process(float delta)
    {
    }
}
