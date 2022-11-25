using Godot;
using System;

public class Map : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public TileMap tileMap;
    public TileSet tileSet;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tileMap = GetNode("TileMap") as TileMap;
        tileSet = tileMap.TileSet;
        int grass = (int)tileSet.GetTilesIds()[1];
        int dirt = (int)tileSet.GetTilesIds()[0];
        Random rnd = new Random();
        for (int i = 0; i < 100; i++)
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
            }else{
                for (int a = 0; a < 3; a++)
                {
                    tileMap.SetCell(i,a,rnd.Next(0,2));
                    getTilesBefore(i,a);
                }
            }
        }
    }


    public Vector2 getTilesBefore(int sor, int oszlop){
        int block = tileMap.GetCell(sor,oszlop);
        for (int i = 0; i < 3; i++)
        {
            if (tileMap.GetCell(i,oszlop-1) == 0){
                return new Vector2(i,oszlop); // Valahogy elkell mentenünk melyik az a sor ahova nem random érték kerül, így a többit lehet majd randomizálni(#TODO)
            }
        }
        return new Vector2();
        //TODO ennek folytatása, az a lényeg, hogy az előző oszlopban lévő kosz block mellé egy kosz block fixen kell ezen kivul pedig lehet még randomizálni, így lesz random de mégis folyamatos a pálya.

    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
