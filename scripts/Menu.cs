using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

public class Menu : Node2D
{
    public AudioStreamPlayer2D MainMusic;
    public override void _Ready()
    {
        File file = new File();
        file.Open("res://save/options.json", File.ModeFlags.Read);
        string text = file.GetAsText();
        ConfigBody jsonfile = JsonConvert.DeserializeObject<ConfigBody>(text);
        file.Close();
        GD.Print(jsonfile.MainVolume);
        GD.Print(jsonfile.MusicVolume);
        GD.Print(jsonfile.UIVolume);
        GD.Print(jsonfile.SoundEffectVolume);
        GD.Print(jsonfile.Name);

        MainMusic = GetNode("MainMusic") as AudioStreamPlayer2D;
        MainMusic.VolumeDb = jsonfile.MainVolume;
    }

    public void _on_PlayButton_pressed(){
        GetTree().ChangeScene("res://scenes/Game.tscn");
    }
    public void _on_OptionsButton_pressed(){
        GetTree().ChangeScene("res://scenes/Options.tscn");
    }
    public void _on_ExitButton_pressed(){
        GetTree().Quit();
    }


    /*
        public override void _Process(float delta)
        {
            MainMusic.VolumeDb -= 0.1f;
        }*/
}
