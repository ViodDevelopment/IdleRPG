using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(GeometryProcedural))]
public class GeometryProceduralEditor : Editor
{
    private int sizeX = 0, sizeZ = 0;
    private int density = 0, precision = 0;
    private string nameMesh;

    private bool activo = false;
    private bool borrar = false;
    private float radiusPath = 1;
    private float radiusEnvironment = 1f;

    private int currentTab = 0;
    private int currentTabEditMode = 0;

    public override void OnInspectorGUI()//La función predeterminada del editor
    {
        GeometryProcedural l_geometryP = (GeometryProcedural)target;

        currentTab = GUILayout.Toolbar(currentTab, new string[] {"Create Mesh", "EditMesh" });

        switch(currentTab)
        {
            case 0:
                sizeX = EditorGUILayout.IntSlider("size X", sizeX,1,60);
                sizeZ = EditorGUILayout.IntSlider("size Z", sizeZ, 1, 60);
                density = EditorGUILayout.IntSlider("vertex per unit", density,1,4);
                precision = EditorGUILayout.IntSlider("Precision procedural", precision,1,4);
                nameMesh = EditorGUILayout.TextField("Name Mesh", nameMesh);
                if (GUILayout.Button("Create Mesh"))
                {
                    l_geometryP.PassAllNums(sizeX, sizeZ, density, precision, nameMesh);
                    l_geometryP.CreateAllVertexs();
                }
                break;
            case 1:
                radiusPath = Mathf.Clamp(EditorGUILayout.FloatField("Radius Path",radiusPath),0.1f,50f);
                radiusEnvironment = Mathf.Clamp(EditorGUILayout.FloatField("Radius Enviroment", radiusEnvironment), 0.1f, 50f);

                currentTabEditMode = GUILayout.Toolbar(currentTabEditMode, new string[] { "Print Path", "Rubish" });

                if (currentTabEditMode == 0)
                {
                    activo = true;
                    borrar = false;
                }
                else if (currentTabEditMode == 1)
                {
                    borrar = true;
                    activo = false;
                }


                if (GUILayout.Button("Save Procedural"))
                {
                    l_geometryP.GetComponent<MatrixOfProcedural>().SaveMatrix();
                    activo = false;
                }
                break;
        }



    }



    void OnSceneGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));

        if(activo && EditorApplication.isPlayingOrWillChangePlaymode)
            EditorApplication.ExitPlaymode();

        if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && (activo || borrar))
        {
           
            GameObject _go = HandleUtility.PickGameObject(e.mousePosition, true);
            if (_go != null)
            {
                Ray l_rayWorld = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                float l_numRep = (l_rayWorld.origin.y - _go.transform.position.y) / Mathf.Abs(l_rayWorld.direction.y);
                Vector3 l_puntoImpacto = l_rayWorld.origin + l_numRep * l_rayWorld.direction;
                GeometryProcedural l_geometryP = (GeometryProcedural)target;
                if(activo)
                    l_geometryP.GetComponent<MatrixOfProcedural>().HitPointOfMatrix(l_puntoImpacto, radiusPath, radiusEnvironment);
                else if(borrar)
                    l_geometryP.GetComponent<MatrixOfProcedural>().RubishMode(l_puntoImpacto, radiusPath);

            }

            Event.current.Use();
        }


        if (e.type == EventType.Layout)
        {
            int id = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(id);
        }
    }

}
