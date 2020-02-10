using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;

    [SerializeField] public List<InfoAllies> alliesOwned = new List<InfoAllies>();
    [SerializeField] public List<InfoAllies> alliesNotOwned = new List<InfoAllies>();
    [SerializeField] public List<InfoAllies> listInfoAllies = new List<InfoAllies>();
    [SerializeField] public int fase;
    [SerializeField] public int etapa;

    public static GameManager GetInstance()
    {
        if(instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public void AddAllieOwned(InfoAllies _ally)//mirar si funciona o tengo que llamarlo por id
    {
        alliesOwned.Add(_ally);
        if (alliesNotOwned.Contains(_ally))
            alliesNotOwned.Remove(_ally);
        //guardar en binario
    }

    public void RemoveAllieOwned(InfoAllies _ally)
    {
        int l_id = _ally.id;
        alliesOwned.Remove(_ally);

        bool exist = false;
        foreach (var item in alliesOwned)
        {
            if(item.id == l_id)
            {
                exist = true;
                break;
            }
        }
        if (exist)
        {
            _ally.currentLevel = 1;
            alliesNotOwned.Add(_ally);
        }
    }
}
