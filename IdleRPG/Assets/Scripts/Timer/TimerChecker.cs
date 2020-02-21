using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerChecker : MonoBehaviour
{

    public string fecha; 
    public TimeSpan hora;
    public int dia;
    public int mes;

    private string fechaSalida;
    private TimeSpan horaSalida;
    private int diaSalida;
    private int mesSalida;

    public TimeSpan CurrentTime;
    public string[] partesFecha;
    void Start ()
    {
        //CurrentTime = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
       

        StartCoroutine(checkTime());
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) //entra
        {
            StartCoroutine(checkTime());
            Check();
           // print("la longitud del array aumenta ? " + partesFecha.Length);

        }
        if (Input.GetMouseButtonDown(1))  //sale
        {
            StartCoroutine(checkTime());
           
        }
           
     
    }

    private IEnumerator checkTime()
    {
        yield return StartCoroutine(TimeManager.sharedInstance.getTime());
        updateTime();
        updateDate();
    }

    void updateTime()
    {
        hora = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
    }
     void updateDate()
    {
        fecha = TimeManager.sharedInstance.getCurrentDateNow();
        partesFecha = fecha.Split('-');
        mes = int.Parse(partesFecha[0]);
        dia = int.Parse(partesFecha[1]);
    }


    void Check()
    {
        if(fechaSalida== null)
        {
            Debug.Log("Es su primerito dia");
        }
        else
        {

        }

    }


    /*  private IEnumerator CheckTime()
 {
     Debug.Log("==> Checking the time");
     timeLabel.text = "Checking the time";
     yield return StartCoroutine(
         TimeManager.sharedInstance.getTime()
     );
     updateTime();
     Debug.Log("==> Time check complete!");

 }
     * 
     * 
     * TimeSpan interval1, interval2;
     interval1 = new TimeSpan(1, 45, 16);
     interval2 = new TimeSpan(1,18, 12, 38);

    print("{0:G} - {1:G} = {2:G} " + (interval1 - interval2));
    print(String.Format(new CultureInfo("fr-FR"), "{0:G} + {1:G} = {2:G}", interval1, interval2, interval1 + interval2));

     interval1 = new TimeSpan(0, 0, 1, 14, 36);
     interval2 = TimeSpan.FromTicks(2143756);
     print("{0:G} + {1:G} = {2:G}"+ interval1 + " " + interval2 + " " + (interval1 + interval2));
     */

}