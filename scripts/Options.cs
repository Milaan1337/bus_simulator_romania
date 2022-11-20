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
    public OptionButton FPSOptions;
    public int fps = 60;
    public override void _Ready()
    {
        MainVolume = GetNode("MainVolume") as HSlider;
        MusicVolume = GetNode("MusicVolume") as HSlider;
        UIVolume = GetNode("UIVolume") as HSlider;
        SoundEffectVolume = GetNode("SoundEffectVolume") as HSlider;
        NameInput = GetNode("NameInput") as TextEdit;
        FPSOptions = GetNode("FPSOptions") as OptionButton;


        string text = File.ReadAllText(@"save/options.json");
        var options = JsonConvert.DeserializeObject<ConfigBody>(text);

        MainVolume.Value = options.MainVolume;
        MusicVolume.Value = options.MusicVolume;
        UIVolume.Value = options.UIVolume;
        SoundEffectVolume.Value = options.SoundEffectVolume;
        NameInput.Text = options.Name;

        FPSOptions.AddItem("60 FPS");
        FPSOptions.AddItem("120 FPS");
        FPSOptions.AddItem("240 FPS");
        FPSOptions.AddItem("360 FPS");
        FPSOptions.AddItem("Unlimited");
    }

    public void _on_FPSOptions_item_selected(int index)
    {
        var selected = index;

        switch (selected)
        {
            case 0:
                GD.Print("60");
                fps = 60;
                break;

            case 1:
                GD.Print("120");
                fps = 120;
                break;

            case 2:
                GD.Print("240");
                fps = 240;
                break;

            case 3:
                GD.Print("360");
                fps = 360;
                break;
            case 4:
                GD.Print("Unlimided");
                fps = 100000;
                break;
        }
    }

    public void _on_SaveButton_pressed()
    {

        JObject options = new JObject(
            new JProperty("MainVolume", (int)MainVolume.Value),
            new JProperty("MusicVolume", (int)MusicVolume.Value),
            new JProperty("UIVolume", (int)UIVolume.Value),
            new JProperty("SoundEffectVolume", (int)SoundEffectVolume.Value),
            new JProperty("Name", (string)NameInput.Text));
            new JProperty("Fps", (int)fps);

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
        GetTree().ChangeScene("res://scenes/Menu.tscn");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
