using Godot;
using System;

public class Map : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public TileMap tileMap;
    public TileSet tileSet;
    public Vector2 grass1 = new Vector2(0,1);
    public Vector2 grass2 = new Vector2(0,0);

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
                Vector2 forcedTile = getTilesBefore(i);
                for (int a = 0; a < 3; a++)
                {
                    if (a == forcedTile.y){
                        tileMap.SetCell(i,a,dirt);    
                    }else{
                        tileMap.SetCell(i,a,rnd.Next(0,2));
                    }
                    
                }
                int grassAmount = rnd.Next(1,3);
            }
        }
    }


    public Vector2 getTilesBefore(int oszlop){
        // GD.Print($"Sor: {sor} és az oszlop: {oszlop} ahol a block : {block}");
        int grassCount = 0;
        Vector2 res = new Vector2();
        Random rndg = new Random();
        
        for (int i = 0; i < 3; i++)
        {
            if (tileMap.GetCell(oszlop-1,i) == 0){
                grassCount+=1;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (tileMap.GetCell(oszlop-1,i) == 0){
                    //return new Vector2(i,oszlop); // Valahogy elkell mentenünk melyik az a sor ahova nem random érték kerül, így a többit lehet majd randomizálni(#TODO)
                    res.x = oszlop;
                    res.y = i;
                    //GD.Print($"Az {oszlop-1} szamu oszlopban {grassCount} db szar volt.");
                    if (grassCount <= 1){break;}
                    else{int randomNum = rndg.Next(0,2);if (randomNum == 0){break;}
                }
            }
            //GD.Print($"A block ebben a sorban {i} és ebben az oszlopban {oszlop-1} ==> {tileMap.GetCell(oszlop-1,i)}");
          
        }
        return res;
        //TODO ennek folytatása, az a lényeg, hogy az előző oszlopban lévő kosz block mellé egy kosz block fixen kell ezen kivul pedig lehet még randomizálni, így lesz random de mégis folyamatos a pálya.

    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
