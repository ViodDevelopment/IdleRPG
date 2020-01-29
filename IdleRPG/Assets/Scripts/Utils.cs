using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    //Returns a list of game objects that are in range with the game object (thisGo) that have the same tag
    public static List<GameObject> CheckGOInRangeByTag(GameObject thisGo, string tag, float range)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        List<GameObject> goInRange = new List<GameObject>();

        foreach (GameObject go in gameObjects)
        {
            if (Vector3.Distance(go.transform.position, thisGo.gameObject.transform.position) <= range && go.activeSelf)
            {
                goInRange.Add(go);
            }
        }
        return goInRange;
    }

    //Returns the closest go to your game object in a list
    public static GameObject GetClosestGOInList(GameObject thisGo, List<GameObject> list)
    {
        float closestDistance = Mathf.Infinity, distance;

        GameObject target = null;

        foreach (GameObject go in list)
        {
            if (go == null) break;
            distance = Vector3.Distance(go.transform.position, thisGo.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = go;
            }
        }

        return target;
    }

   
}