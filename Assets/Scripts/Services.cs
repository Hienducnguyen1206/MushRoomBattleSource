
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text.RegularExpressions;

public class Services : MonoBehaviour
{
   public static bool Chance(float chance)
    {   if(chance > 100)
        {
            chance = 100;
        }
        if(UnityEngine.Random.Range(1,101)<= chance)
        {
            return true;
        }else
        {
            return false;
        }
   }

    public static T RandomObject<T>(T[] gameObjects)
    {
        if (gameObjects == null || gameObjects.Length == 0)
        {
            throw new System.ArgumentException("The array cannot be null or empty");
        }

        int n = gameObjects.Length;
        int x = UnityEngine.Random.Range(0, n);

        return gameObjects[x];
    }

    public static T GetRandomElement<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException("Danh sách rỗng hoặc null.");
        }

        
        System.Random random = new System.Random();

       
        int randomIndex = random.Next(list.Count);

        
        return list[randomIndex];
    }

    public  static string ConvertToReadableFormat(string input)
    {
        
        string formatted = Regex.Replace(input, "(?<!^)([A-Z])", " $1");
       
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formatted.ToLower());
    }





}
