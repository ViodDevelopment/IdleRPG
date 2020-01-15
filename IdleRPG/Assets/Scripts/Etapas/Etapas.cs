using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etapas : MonoBehaviour
{
    #region Variables
    public int Etapa;
    public int Fase;
    public int VidaTotal;
    //public int Damage;
    public int Enemigos;
    public bool Etapa10;
    #endregion

    //fase y etapa
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //sustituir la variable del if por el boton 
        {
            Fase = 10;
            //parar todo lo que haya en la escena y pasar a la fase 10
        }
        ComprobarFasesEtapa();
      

    }
    private void ComprobarFasesEtapa()
    {
        if (VidaTotal <= 0) // enviar a la fase 0 de la misma etapa si se mueren todos los personajes
        {
            Fase = 0;
            VidaTotal = 5;
        }
        else if (Enemigos == 0 && Fase!=10)
        {
            Fase++;
        }else if (Enemigos == 0 && Fase==10)
        {
            Fase = 0;
            Etapa++;
        }
        
    }
    

    /*
    private void ScriptDeCosas(int _variable)//lo que hace
    {
        l_variableDentro
    }*/
}
