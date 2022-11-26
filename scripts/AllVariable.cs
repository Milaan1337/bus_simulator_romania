public class AllVariable
{
    private static int nitrovalue;
    private static int speedvalue;
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
}