using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationOfPath : MonoBehaviour
{
    private MyPath path;

    public void CreateNextPointPath(GameObject _mesh, float _yPos, Vector3 _impactPoint, int _state)
    {
        path = gameObject.GetComponent<MyPath>();
        GameObject newGO = new GameObject();
        newGO.name = "Point " + path.listPath.Count;
        newGO.transform.position = _impactPoint + new Vector3(0, _yPos, 0);
        newGO.transform.parent = gameObject.transform;

        switch(_state)
        {
            case 0:
                newGO.tag = "Passing";
                break;
            case 1:
                newGO.tag = "Battle";
                break;
            case 2:
                newGO.tag = "Respawn";
                break;
        }
        path.listPath.Add(newGO.transform);
        path.UpdateCamino();
    }
}
