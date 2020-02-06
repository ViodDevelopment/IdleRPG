using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class InfoAllies
{
    public enum TypeOfFactions {NONE,IMPERIO, MISTICO, YOKAI, DIOS};
    public enum TypeOfClass {NONE, TANQUE, MELEE, DISTANCIA, APOYO};

    public TypeOfFactions faction = TypeOfFactions.NONE;
    public TypeOfClass classe = TypeOfClass.NONE;

    public int id = 0;
    public int currentLevel = 0;
    public int maxLevel = 0;

    public string name = "";
    public bool melee = false;
    public bool obtained = false;

    public List<int> atackList = new List<int>();
    public List<int> defensList = new List<int>();
    public List<int> lifeList = new List<int>();
    public float atackSpeed = 0;
}
