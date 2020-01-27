using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeometryProcedural))]
public class GeometryProceduralEditor : Editor
{
    private static int sizeX = 0, sizeZ = 0;
    private static int density = 0, precision = 0;
    private static string nameMesh;
    private bool activo = true;

    public override void OnInspectorGUI()//La función predeterminada del editor
    {
        base.OnInspectorGUI();
        sizeX = Mathf.Clamp(EditorGUILayout.IntField("size X", sizeX), 1, 60);
        sizeZ = Mathf.Clamp(EditorGUILayout.IntField("size Z", sizeZ), 1, 60);
        density = Mathf.Clamp(EditorGUILayout.IntField("vertex per unit", density), 1, 4);
        precision = Mathf.Clamp(EditorGUILayout.IntField("Precision procedural", precision), 1, 4);
        nameMesh = EditorGUILayout.TextField("Name Mesh", nameMesh);
        activo = EditorGUILayout.Toggle("Move Camera",activo);

        GeometryProcedural l_geometryP = (GeometryProcedural)target;

        if (GUILayout.Button("Create Mesh"))
        {
            l_geometryP.PassAllNums(sizeX, sizeZ, density, precision, nameMesh);
            l_geometryP.CreateAllVertexs();
        }


    }



    void OnSceneGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));

        if (e.type == EventType.MouseDown && activo)
        {
            Event.current.Use();
        }


        if (e.type == EventType.Layout)
        {
            int id = GUIUtility.GetControlID(FocusType.Native);
            HandleUtility.AddDefaultControl(id);
        }
    }

}
