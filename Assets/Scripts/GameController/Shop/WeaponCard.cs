using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "WeaponCard", menuName = "GameConfiguration/WeaponCard", order = 1)]
[System.Serializable]
public class WeaponCard : ScriptableObject
{
   
    public string cardName;
    public int Cost;
    public Sprite cardImage;
}



