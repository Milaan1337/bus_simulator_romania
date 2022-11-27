using Godot;
using System;

public class Map : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] private int mapLength;
    public TileMap tileMap;
    public TileSet tileSet;
    public Node2D bus_StopN;
    public Sprite red_circle;
    public Area2D bus_stop;
    public enum dirtForm
    {
        oneBlock,
        oneUp,
        oneDown,
    }
    public int lastDirtIndex;
    public dirtForm lastDirtForm;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bus_StopN = GetNode("/root/Game/Bus_stop") as Node2D;
        bus_stop = bus_StopN.GetNode("Area2D") as Area2D;
        GD.Print($"BUS_STOP1{bus_stop.Position.x}");
        //TODO Mi a gyászért ír totális faszságot a getpos egyik helyen 186 a másikon már 286 és nem lehet állítani a poziciojat
        red_circle = bus_StopN.GetNode("Sprite") as Sprite;
        tileMap = GetNode("TileMap") as TileMap;
        tileSet = tileMap.TileSet;
        int grass = (int)tileSet.GetTilesIds()[1];
        int dirt = (int)tileSet.GetTilesIds()[0];
        Random rnd = new Random();
        for (int i = 0; i < mapLength; i++)
        {
            if (i == 0){
                for (int a = 0; a < 3; a++)
                {
                    if (a != 1){
                        tileMap.SetCell(i,a,grass);
                    }else{
                        tileMap.SetCell(i,a,dirt);
                    }
                }

                lastDirtForm = dirtForm.oneBlock;
            }else
            {
                int column = i - 1;
                lastDirtIndex = getTileBefore(column);
                /*
                for (int a = 0; a < 3; a++)
                {
                    if (a == lastDirtIndex){
                        tileMap.SetCell(i,a,dirt);    
                    }else{
                        tileMap.SetCell(i,a,grass);
                    }
                    
                }
                */
                drawDirt(i);
            }
        }
    }


    public int getTileBefore(int oszlop){
        int res = 0;
        switch (lastDirtForm)
        {
            default:
                break;
            case dirtForm.oneBlock:
                for (int i = 0; i < 3; i++)
                {
                    if (tileMap.GetCell(oszlop, i) == 0)
                    {
                        res = i;
                        break;
                    }
                }
                break;
            case dirtForm.oneUp:
                for (int i = 0; i < 3; i++)
                {
                    if (tileMap.GetCell(oszlop, i) == 0)
                    {
                        res = i;
                        break;
                    }
                }
                break;
            case dirtForm.oneDown:
                for (int i = 0; i < 3; i++)
                {
                    if (tileMap.GetCell(oszlop, i) == 0)
                    {
                        res = i;
                    }
                }
                break;
        }

        return res;
    }

    public void drawDirt(int oszlop)
    {
        Random randomObject = new Random();
        dirtForm randomForm = dirtForm.oneBlock;
        Vector2[] positions = new Vector2[3];
        switch (lastDirtIndex)
        {
            default:
                break;
            case 0:
                int randomNum = randomObject.Next(0, 2);
                switch (randomNum)
                {
                    default:
                        break;
                    case 0:
                        randomForm = dirtForm.oneBlock;
                        positions[0] = new Vector2(oszlop,0);
                        positions[1] = new Vector2(-1,-1);
                        positions[2] = new Vector2(-1,-1);
                        break;
                    case 1:
                        randomForm = dirtForm.oneDown;
                        positions[0] = new Vector2(oszlop,0);
                        positions[1] = new Vector2(oszlop,1);
                        positions[2] = new Vector2(-1,-1);
                        break;
                }
                break;
            case 1:
                int randomNum1 = randomObject.Next(0, 3);
                switch (randomNum1)
                {
                    default:
                        break;
                    case 0:
                        randomForm = dirtForm.oneBlock;
                        positions[0] = new Vector2(-1,-1);
                        positions[1] = new Vector2(oszlop,1);
                        positions[2] = new Vector2(-1,-1);
                        break;
                    case 1:
                        randomForm = dirtForm.oneDown;
                        positions[0] = new Vector2(-1,-1);
                        positions[1] = new Vector2(oszlop,1);
                        positions[2] = new Vector2(oszlop,2);
                        break;
                    case 2:
                        randomForm = dirtForm.oneUp;
                        positions[0] = new Vector2(oszlop,0);
                        positions[1] = new Vector2(oszlop,1);
                        positions[2] = new Vector2(-1,-1);

                        break;
                }
                break;
            case 2:
                int randomNum2 = randomObject.Next(0, 2);
                switch (randomNum2)
                {
                    default:
                        break;
                    case 0:
                        randomForm = dirtForm.oneBlock;
                        positions[0] = new Vector2(-1,-1);
                        positions[1] = new Vector2(-1,-1);
                        positions[2] = new Vector2(oszlop,2);
                        break;
                    case 1:
                        randomForm = dirtForm.oneUp;
                        positions[0] = new Vector2(-1,-1);
                        positions[1] = new Vector2(oszlop,1);
                        positions[2] = new Vector2(oszlop,2);
                        break;
                }
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            //GD.Print($"{oszlop}.oszlop {i}. eleme egy : {positions[i]}");
            if (positions[i].x != -1)
            {
                tileMap.SetCell(oszlop,i,0);
                if (oszlop == mapLength-1)
                {
                    GD.Print($"BUS_STOP2{bus_stop.Position.x}");
                    //bus_stop.Position = new Vector2(oszlop*64,i*64);
                    //red_circle.Position = new Vector2(oszlop*64,i*64);
                }
            }
            else
            {
                tileMap.SetCell(oszlop,i,1);
            }
        }
        lastDirtForm = randomForm;

    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
