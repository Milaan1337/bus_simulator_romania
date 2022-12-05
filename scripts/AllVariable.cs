using System;
using Godot;
public class AllVariable
{
    private static int nitrovalue;
    private static int nitrovalue2;
    private static int speedvalue = 400;
    private static int timevalue;
    private static int maplengthvalue;
    private static int hpvalue = 100;
    private static bool pauseonvalue = false;
    private static bool singleplayvalue = false;
    private static string nyertautovalue;

    public int nitrous 
    {
        get { return nitrovalue; }
        set { nitrovalue = value; }
    }

    public int nitrous2
    {
        get { return nitrovalue2; }
        set { nitrovalue2 = value; }
    }
    public string nyertauto 
    {
        get { return nyertautovalue; }
        set { nyertautovalue = value; }
    }
    public int speed 
    {
        get { return speedvalue; }
        set { 
            speedvalue = value; 
            GD.Print("CIGO ALLITVA");    
        }
    }
    public int hp
    {
        get { return hpvalue; }
        set { hpvalue = value; }
    }
    public int time
    {
        get { return timevalue; }
        set { timevalue = value; }
    }
    public int maplength
    {
        get { return maplengthvalue; }
        set { maplengthvalue = value; }
    }
    public bool pauseon
    {
        get { return pauseonvalue; }
        set { pauseonvalue = value; }
    }

    public bool singleplay
    {
        get { return singleplayvalue; }
        set { singleplayvalue = value; }
    }
}