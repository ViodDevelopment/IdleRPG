using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class BaseDeDatosAllies
{
    private static BaseDeDatosAllies instance;
    public List<InfoAllies> listInfoAllies = new List<InfoAllies>();
    private string nameRute = "/alliesBBDD.dat";
    private string nameCSV = "/alliesStats.csv";

    public static BaseDeDatosAllies GetInstance()
    {
        if (instance == null)
        {
            instance = new BaseDeDatosAllies();
            if(!File.Exists(Application.streamingAssetsPath + instance.nameRute))
                instance.ReadCSV();
            else
            instance.LoadBaseOfDates();
        }
        return instance;
    }

    private void ReadCSV()
    {
        if (File.Exists(Application.streamingAssetsPath + nameCSV))
        {
            StreamReader streamReader = new StreamReader(Application.streamingAssetsPath + nameCSV);
            bool l_ended = false;
            int l_row = 0;
            int l_id = 0;
            while (!l_ended)
            {
                string data = streamReader.ReadLine();
                l_row++;
                if (data == null || streamReader == null)
                {
                    l_ended = true;
                    break;
                }
                var valor = data.Split(',');

                InfoAllies l_allie = new InfoAllies();
                if(l_row == 1)
                {
                    Debug.Log(l_id);
                    l_allie.id = l_id;
                    l_allie.name = valor[0];
                    switch(int.Parse(valor[1]))
                    {
                        case 0:
                            l_allie.faction = InfoAllies.TypeOfFactions.IMPERIO;//si peta es pq el excel todavia no lo tiene en numeros estos datos
                            break;
                        case 1:
                            l_allie.faction = InfoAllies.TypeOfFactions.MISTICO;
                            break;
                        case 2:
                            l_allie.faction = InfoAllies.TypeOfFactions.YOKAI;
                            break;
                        case 3:
                            l_allie.faction = InfoAllies.TypeOfFactions.DIOS;
                            break;
                    }

                    switch (int.Parse(valor[2]))
                    {
                        case 0:
                            l_allie.classe = InfoAllies.TypeOfClass.TANQUE;
                            break;
                        case 1:
                            l_allie.classe = InfoAllies.TypeOfClass.MELEE;
                            break;
                        case 2:
                            l_allie.classe = InfoAllies.TypeOfClass.DISTANCIA;
                            break;
                        case 3:
                            l_allie.classe = InfoAllies.TypeOfClass.APOYO;
                            break;
                    }


                    if (valor[3] == "M")
                        l_allie.melee = true;
                    else l_allie.melee = false;
                    //añadir mas cosas
                }else if(l_row >= 4 && l_row <= 203)
                {
                    if (valor[1] != "")
                    {
                        l_allie.maxLevel = int.Parse(valor[0]);
                        l_allie.lifeList.Add(int.Parse(valor[1]));
                        l_allie.atackList.Add(int.Parse(valor[2]));
                        l_allie.defensList.Add(int.Parse(valor[3]));
                        l_allie.atackSpeed = float.Parse(valor[4]);
                    }
                }



                if(l_row == 202)
                {
                    l_row = 0;
                    l_id++;
                    listInfoAllies.Add(l_allie);
                }

                if (valor.Length == 0)
                    l_ended = true;
            }
            instance.SaveBaseOfDates();
        }
        else Debug.Log("no existe el csv");

    }


    private void SaveBaseOfDates()
    {
        Debug.Log("Guardada la Base de Datos de Allies");

        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Create(Application.streamingAssetsPath + nameRute);

        BaseOfAllies l_datos = new BaseOfAllies();
        l_datos.infoAllies = listInfoAllies;

        l_bf.Serialize(l_file, l_datos);

        l_file.Close();
    }

    private void LoadBaseOfDates()
    {

        Debug.Log("Cargada la Base de Datos de Allies");
        List<BaseOfAllies> l_allies = new List<BaseOfAllies>();
        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Open(Application.streamingAssetsPath + nameRute, FileMode.Open);

        BaseOfAllies l_datos = (BaseOfAllies)l_bf.Deserialize(l_file);
        listInfoAllies = l_datos.infoAllies;

        l_file.Close();
    }

}

[Serializable]
public class BaseOfAllies
{
    public List<InfoAllies> infoAllies = new List<InfoAllies>();
}