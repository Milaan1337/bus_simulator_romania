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
    public override void _Ready()
    {
    }

    public void _on_SaveButton_pressed()
    {
        MainVolume = GetNode("MainVolume") as HSlider;
        MusicVolume = GetNode("MusicVolume") as HSlider;
        UIVolume = GetNode("UIVolume") as HSlider;
        SoundEffectVolume = GetNode("SoundEffectVolume") as HSlider;
        NameInput = GetNode("NameInput") as TextEdit;

        JObject volume = new JObject(
            new JProperty("MainVolume", (int)MainVolume.Value),
            new JProperty("MusicVolume", (int)MusicVolume.Value),
            new JProperty("UIVolume", (int)UIVolume.Value),
            new JProperty("SoundEffectVolume", (int)SoundEffectVolume.Value),
            new JProperty("Name", (string)NameInput.Text));

        File.WriteAllText(@"save/options.json", volume.ToString());

        using (StreamWriter file = File.CreateText(@"save/options.json"))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            volume.WriteTo(writer);
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
