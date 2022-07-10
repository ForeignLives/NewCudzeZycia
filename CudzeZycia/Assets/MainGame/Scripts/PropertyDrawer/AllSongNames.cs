using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSongNames : MonoBehaviour
{
    public static string[] GetAllSongNames()
    {
        // nazwy skopiuj z AudioManager
        var x = new List<string> {
            "MenuTheme",
            "CityNight",
            "Restaurant",
            "MenuClick",
            "FightMusic",
            "Tajemniczy",
            "Dramatyczna",
            "Mieszkanie",
            "MinigamePixel",
            "Cisza",
            "BurdelMusic",
            "ArcadeMusic",
        };
        return x.ToArray();
    }
}
