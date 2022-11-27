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
    public CheckButton fps_is_onbutton;
    public AudioStreamPlayer2D click;
    public OptionButton fpstarget;
    public OptionButton display;
    public int money;
    public bool fps_is_on;
    public int fpstar;
    public int displayindex;
    public override void _Ready()
    {
        MainVolume = GetNode("MainVolume") as HSlider;
        MusicVolume = GetNode("MusicVolume") as HSlider;
        UIVolume = GetNode("UIVolume") as HSlider;
        SoundEffectVolume = GetNode("SoundEffectVolume") as HSlider;
        fps_is_onbutton = GetNode("fps_is_on") as CheckButton;
        click = GetNode("Click") as AudioStreamPlayer2D;
        fpstarget = GetNode("FpsTarget") as OptionButton;
        display = GetNode("Display") as OptionButton;


        string text = File.ReadAllText(@"save/options.json");
        var options = JsonConvert.DeserializeObject<ConfigBody>(text);

        MainVolume.Value = options.MainVolume;
        MusicVolume.Value = options.MusicVolume;
        UIVolume.Value = options.UIVolume;
        SoundEffectVolume.Value = options.SoundEffectVolume;
        money = options.Money;
        fps_is_onbutton.Pressed = options.fps_is_on;
        click.VolumeDb = options.UIVolume;

        /////////////////////////////////////////////////////////

        fpstarget.AddItem("FPS");
        fpstarget.AddItem("30");
        fpstarget.AddItem("60");
        fpstarget.AddItem("120");
        fpstarget.AddItem("240");
        fpstarget.AddItem("360");
        fpstarget.AddItem("Unlimited");

        fpstarget.Selected = options.fps_target;
        fpstarget.SetItemDisabled(0, true);

        /////////////////////////////////////////////////////////

        display.AddItem("Bordless");
        display.AddItem("Fullscreen");
        display.AddItem("Bordless Fullscreen");

        display.Selected = options.display_index;

    }
    public void _on_Display_item_selected(int index)
    {
        switch (index)
        {
            case 0:
                displayindex = 0;
                break;
                
            case 1:
                displayindex = 1;
                break;
                
            case 2:
                displayindex = 2;
                break;
        }
    }
    public void _on_FpsTarget_item_selected(int index)
    {
        switch (index)
        {
            case 1:
                fpstar = 1;
                break;
                
            case 2:
                fpstar = 2;
                break;
                
            case 3:
                fpstar = 3;
                break;
                
            case 4:
                fpstar = 4;
                break;
                
            case 5:
                fpstar = 5;
                break;
                
            case 6:
                fpstar = 6;
                break;
        }
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
            new JProperty("fps_target", (int)fpstar),
            new JProperty("display_index", (int)displayindex));

        File.WriteAllText(@"save/options.json", options.ToString());

        using (StreamWriter file = File.CreateText(@"save/options.json"))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            options.WriteTo(writer);
        }
        GetTree().ChangeScene("res://scenes/Menu.tscn");
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
