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

    #region BoolsTypesOfTool
    private bool activo = false;
    private bool borrar = false;
    private bool environment = false;
    private bool lockedCamera = false;
    private bool cleanAll = false;
    private bool cleanPath = false;
    private bool cleanEnvironment = false;
    #endregion

    private float radiusPath = 1;
    private float radiusEnvironment = 1f;

    private int currentTab = 0;
    private int currentTabEditMode = 0;
    private int currentTabClean = 0;

    public override void OnInspectorGUI()//La función predeterminada del editor
    {
        GeometryProcedural l_geometryP = (GeometryProcedural)target;

        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Create Mesh", "EditMesh" });

        switch (currentTab)
        {
            case 0:
                sizeX = EditorGUILayout.IntSlider("size X", sizeX, 1, 60);
                sizeZ = EditorGUILayout.IntSlider("size Z", sizeZ, 1, 60);
                density = EditorGUILayout.IntSlider("vertex per unit", density, 1, 4);
                precision = EditorGUILayout.IntSlider("Precision procedural", precision, 1, 4);
                nameMesh = EditorGUILayout.TextField("Name Mesh", nameMesh);
                if (GUILayout.Button("Create Mesh"))
                {
                    l_geometryP.PassAllNums(sizeX, sizeZ, density, precision, nameMesh);
                    l_geometryP.CreateAllVertexs();
                }
                break;
            case 1:
                radiusPath = Mathf.Clamp(EditorGUILayout.FloatField("Radius Path", radiusPath), 0.1f, 50f);
                radiusEnvironment = Mathf.Clamp(EditorGUILayout.FloatField("Radius Enviroment", radiusEnvironment), 0.1f, 50f);

                EditorGUILayout.LabelField("Print");
                currentTabEditMode = GUILayout.Toolbar(currentTabEditMode, new string[] { "None", "Print Path", "Environment" });

                if (currentTabEditMode != 0)
                    currentTabClean = 0;

                EditorGUILayout.LabelField("Clean");
                currentTabClean = GUILayout.Toolbar(currentTabClean, new string[] { "None", "Rubish", "R.Path", "R.Env.", "Clean all" });

                if (currentTabClean != 0)
                    currentTabEditMode = 0;


                switch (currentTabEditMode)
                {
                    case 1:
                        ResetBools();
                        activo = true;
                        break;
                    case 2:
                        ResetBools();
                        environment = true;
                        break;

                }

                switch (currentTabClean)
                {
                    case 1:
                        ResetBools();
                        borrar = true;
                        break;
                    case 2:
                        ResetBools();
                        cleanPath = true;
                        break;
                    case 3:
                        ResetBools();
                        cleanEnvironment = true;
                        break;
                    case 4:
                        ResetBools();
                        cleanAll = true;
                        break;
                }

                if (currentTabEditMode > 0 || currentTabClean > 0)
                    lockedCamera = true;
                else
                {
                    lockedCamera = false;
                    borrar = false;
                    activo = false;
                    environment = false;
                }


                if (GUILayout.Button("Save Procedural"))
                {
                     MatrixOfProcedural[] l_allMatrix = GameObject.FindObjectsOfType<MatrixOfProcedural>();//Puede ser poco optimo, revisar para mejorar
                    foreach (var item in l_allMatrix)
                    {
                        item.SaveMatrix();
                    }
                    ResetBools();
                    currentTabClean = 0;
                    currentTabEditMode = 0;
                }
                break;
        }



    }



    void OnSceneGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));

        if (lockedCamera && EditorApplication.isPlayingOrWillChangePlaymode)
            EditorApplication.ExitPlaymode();

        if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && lockedCamera)
        {

            GameObject _go = HandleUtility.PickGameObject(e.mousePosition, true);
            if (_go != null)
            {
                if (_go.GetComponent<GeometryProcedural>() != null)
                {
                    GeometryProcedural l_geometryP = _go.GetComponent<GeometryProcedural>();
                    if (!cleanAll)
                    {
                        Ray l_rayWorld = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                        float l_numRep = (l_rayWorld.origin.y - _go.transform.position.y) / Mathf.Abs(l_rayWorld.direction.y);
                        Vector3 l_puntoImpacto = l_rayWorld.origin + l_numRep * l_rayWorld.direction - _go.transform.position;
                        if (activo)
                            l_geometryP.GetComponent<MatrixOfProcedural>().PrintPath(l_puntoImpacto, radiusPath, radiusEnvironment);
                        else if (borrar)
                            l_geometryP.GetComponent<MatrixOfProcedural>().RubishMode(l_puntoImpacto, radiusPath);
                        else if (environment)
                            l_geometryP.GetComponent<MatrixOfProcedural>().EnvironmentMode(l_puntoImpacto, radiusPath + radiusEnvironment);
                        else if (cleanPath)
                            l_geometryP.GetComponent<MatrixOfProcedural>().CleanPath(l_puntoImpacto, radiusPath);
                        else if (cleanEnvironment)
                            l_geometryP.GetComponent<MatrixOfProcedural>().CleanEnvironment(l_puntoImpacto, radiusPath + radiusEnvironment);//



                    }
                    else
                        l_geometryP.GetComponent<MatrixOfProcedural>().CleanAll();
                }


            }


            Event.current.Use();
        }


        if (e.type == EventType.Layout)
        {
            int id = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(id);
        }
    }

    private void ResetBools()
    {
        activo = false;
        borrar = false;
        environment = false;
        cleanAll = false;
        cleanPath = false;
        cleanEnvironment = false;
    }

}


