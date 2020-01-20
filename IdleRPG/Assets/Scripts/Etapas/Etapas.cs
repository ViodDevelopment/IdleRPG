using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Etapas : MonoBehaviour
{
    #region Variables
    public int Etapa;
    public int Fase;
    public int VidaTotal;
 
    public int Enemigos;
    public Button m_Avance;
    public Button m_AvanzarFase;
    public Text VerFase;
    #endregion

    //fase y etapa
    // Start is called before the first frame update
    void Start()
    {
        VidaTotal = 5;
        Enemigos = 10;
        Fase = 0;
        m_Avance.onClick.AddListener(AvanceFase);
        m_AvanzarFase.onClick.AddListener(PasarFase);
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarFasesEtapa();
        VerFase.text = "Fase: " + Fase + " etapa:" + Etapa;

    }
    private void ComprobarFasesEtapa()
    {
        if (VidaTotal <= 0) // enviar a la fase 0 de la misma etapa si se mueren todos los personajes
        {
            Fase = 0;
            VidaTotal = 5;
        }
        else if (Enemigos == 0 && Fase != 10)
        {
            Fase++;
            Enemigos = 10;
         
        }
        else if (Enemigos == 0 && Fase == 10)
        {
            Fase = 0;
            Etapa++;
            Enemigos = 1;
        }

    }
    private void AvanceFase() // avanza directamente a la fase 10, se debera detener la fase en la que esta e iniciar la 10
    {
        Fase = 10;
    }
    private void PasarFase()
    {
        Enemigos = 0;
    }

}
