using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VertexProcedural 
{
    public float[] positionsInFloats = new float[3];// Si se hace binario y la posicion el local
    //public Vector3 position; //Si se hace en GO en escena
    public int posXMatrix;
    public int posZMatrix;
    public enum typeOfVertex {NONE ,PATH, ENVIRONMENT };
    public typeOfVertex currentTypeVertex = typeOfVertex.NONE;
    public bool ocupated = false;
    public VertexProcedural parentOcupated;
    public List<int> myVertexsX = new List<int>();
    public List<int> myVertexsZ = new List<int>();
    public bool myGameObject = false;//por probar
    public string nameGO = "";
    public int density = 1;

    public void ResetPoint()
    {
        currentTypeVertex = typeOfVertex.NONE;
        parentOcupated = null;
        ocupated = false;
        if(myGameObject)
        {
            myVertexsX.Clear();//recalular los vertices
            myVertexsZ.Clear();

            GameObject.DestroyImmediate(GameObject.Find(nameGO));
            myGameObject = false;
            nameGO = "";
        }
    }
}
