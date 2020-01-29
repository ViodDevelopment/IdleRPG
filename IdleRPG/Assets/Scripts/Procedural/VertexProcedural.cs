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


    public void ResetPoint()
    {
        currentTypeVertex = typeOfVertex.NONE;
    }
}
