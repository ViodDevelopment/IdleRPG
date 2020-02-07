using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor;


[CustomEditor(typeof(CreationOfPath))]
public class CreationOfPathEditor : Editor
{
    private bool activo = false;
    private float yPos = 0;
    private bool battle;
    private bool respawn;
    private int currentTab = 0;

    public override void OnInspectorGUI()//La función predeterminada del editor
    {
        yPos = EditorGUILayout.FloatField("Altura desde Mesh", yPos);
        activo = EditorGUILayout.Toggle("Crear Camino", activo);
        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Passing", "Battle", "Respawn" });
        
        switch(currentTab)
        {
            case 0:
                battle = false;
                respawn = false;
                break;
            case 1:
                battle = true;
                respawn = false;
                break;
            case 2:
                battle = false;
                respawn = true;
                break;
        }
    }

    void OnSceneGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));

        if (activo)
            EditorApplication.ExitPlaymode();

        if ( e.type == EventType.MouseDown && activo)
        {
            GameObject _go = HandleUtility.PickGameObject(e.mousePosition, true);
            if (_go != null)
            {
                Ray l_rayWorld = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                float l_numRep = (l_rayWorld.origin.y - _go.transform.position.y) / Mathf.Abs(l_rayWorld.direction.y);
                Vector3 l_puntoImpacto = l_rayWorld.origin + l_numRep * l_rayWorld.direction;

                int l_numState = 0;
                if (battle)
                    l_numState = 1;
                else if (respawn)
                    l_numState = 2;

                CreationOfPath _path = (CreationOfPath)target;
                _path.CreateNextPointPath(_go, yPos, l_puntoImpacto, l_numState);
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
