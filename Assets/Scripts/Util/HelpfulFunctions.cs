using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpfulFunctions
{
    public static Sprite GetSprite(string _fileName){
        Sprite mySprite;
        mySprite = Resources.Load<Sprite>("Sprites/" + _fileName);
        return mySprite; 
    }
}
