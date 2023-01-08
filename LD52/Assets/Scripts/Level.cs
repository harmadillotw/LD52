using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public int levelNumber;
    public string levelName;
    public string startingText;
    public string endingText;

    public int redApples;
    public int greenApples;
    public int pears;
    public int others;


    public Level()
    {

    }
    public Level(int levelNumber, string levelName, string startingText, string endingText, int redApples, int greenApples, int pears, int others)
    {
        this.levelNumber = levelNumber;
        this.levelName = levelName;
        this.startingText = startingText;
        this.endingText = endingText;
        this.redApples = redApples;
        this.greenApples = greenApples;
        this.pears = pears;
        this.others = others;
    }

    

}
