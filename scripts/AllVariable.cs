public class AllVariable
{
    private static int nitrovalue;
    private static int speedvalue;
    private static int timevalue;
    private static int maplengthvalue;
    private static int hpvalue = 100;

    public int nitrous 
    {
        get { return nitrovalue; }
        set { nitrovalue = value; }
    }
    public int speed 
    {
        get { return speedvalue; }
        set { speedvalue = value; }
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
}