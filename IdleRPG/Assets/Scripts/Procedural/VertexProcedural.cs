using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VertexProcedural 
{
    public float[] positionsInFloats = new float[3];// Si se hace binario
    //public Vector3 position; //Si se hace en GO en escena
    public int posXMatrix;
    public int posZMatrix;
    public int typeOfVertex; //En un futuro hacerlo con diccionario
    public bool transitable;
}
