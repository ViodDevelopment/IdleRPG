using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class InfoAllies
{
    public int id = 0;
    public int currentLevel = 0;
    public int maxLevel = 0;
    public string name = "";
    public List<int> atackList = new List<int>();
    public List<int> defensList = new List<int>();
    public List<int> lifeList = new List<int>();
    public float atackSpeed = 1;
}
