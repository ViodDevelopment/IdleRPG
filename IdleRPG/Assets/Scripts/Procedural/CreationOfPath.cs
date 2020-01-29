using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationOfPath : MonoBehaviour
{
    private Path path;

    public void CreateNextPointPath(GameObject _mesh, float _yPos, Vector3 _impactPoint)
    {
        path = gameObject.GetComponent<Path>();
        GameObject newGO = new GameObject();
        newGO.name = "Point " + path.listPath.Count;
        newGO.transform.position = _impactPoint + new Vector3(0, _yPos, 0);
        newGO.transform.parent = gameObject.transform;
        path.listPath.Add(newGO.transform);
        path.UpdateCamino();
    }
}
