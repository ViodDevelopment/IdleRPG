using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
  

    private int DiaAct; // hay que Guardar los dias aunque te salgas del juego
    private int DiaRec;
    private string wdatos;
    private float timer;
    private string _currentDay;

    private bool Recolectada = false; // si ya ha reclamado la recompensa sera true



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getTime());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(getTime());
            OnClickReward();
        }
        if (Recolectada && timer %2 >= 0 && timer % 2<=0.5 )
        {
            StartCoroutine(getTime());
            DiaRec = int.Parse(_currentDay);
            if (DiaAct != DiaRec)
            {
                Recolectada = false; 
            }
            //comprobar si estan en el mismo dia.
            timer += Time.deltaTime;
        }
        else
        {
            timer += Time.deltaTime; 
        }

    }

    void OnClickReward (){


        if (Advertisement.IsReady("video") && !Recolectada)
        {
            Advertisement.Show("video");

            

           
            DiaAct = int.Parse(_currentDay); 
            Recolectada = true;
        }
        else
        {
            Debug.Log("Ya esta reclamada");
        }

    }

    

   


    public IEnumerator getTime()
    {
        WWW www = new WWW("http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php");

        yield return www;
        wdatos = www.text;
        string[] words = wdatos.Split('/');
        string[] PartesFecha = words[0].Split('-');

        _currentDay =PartesFecha[1];
        
    }

}
