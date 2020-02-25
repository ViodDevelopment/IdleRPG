using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class BaseDeDatosAllies : MonoBehaviour
{
    private static BaseDeDatosAllies instance;
    private string nameRute = "";
    private string nameRuteOriginal = "";
    private string nameRutePlayerSave = "/progressPlayerAlly.dat";
    private string nameRuteFaseProgress = "/ProgressEtapasAndFases.dat";
    private string folderDestination = "/PlayerProgress";
    private string nameCSV = "/alliesStats.csv";
    private string fecha = "07022020";//poner la fecha del dia q se crea la bbdd
    private string lastFecha = "07022021";
    private string originalPath = "alliesBBDD.dat";
    private string originalPathEnemies = "enemiesBBDD.dat";
    private string nameRuteEnemies = "";
    private string nameRuteOriginalEnemies = "";
    private void Awake()
    {
        GameObject go = GameObject.Find("BaseDeDatos");
        if (go == null || go == gameObject)
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            if (!Directory.Exists(Application.persistentDataPath + instance.folderDestination))
            {
                Directory.CreateDirectory(Application.persistentDataPath + instance.folderDestination);
            }
            instance.nameRute = instance.fecha + instance.originalPath;
            instance.nameRuteOriginal = instance.fecha + instance.originalPath;

            if (!File.Exists(Application.streamingAssetsPath + "/" + instance.nameRuteOriginal))
            {
                instance.ReadCSV();
            }

            if (!File.Exists(Application.persistentDataPath + folderDestination + "/" + nameRute))
            {
                instance.StartCoroutine(instance.CopyPalabrasBinaryToPersistentPath());
            }
            else
            {

                if (File.Exists(Application.persistentDataPath + folderDestination + "/" + lastFecha + originalPath))
                    File.Delete(Application.persistentDataPath + folderDestination + "/" + lastFecha + originalPath);

                Inicialice();
            }

        }
        else if (go != gameObject)
            Destroy(gameObject);
    }

    private void Inicialice()
    {
        LoadBaseOfDates();

        if (!File.Exists(Application.persistentDataPath + instance.folderDestination + instance.nameRutePlayerSave))
        {
            //poner datos minimos 
            instance.SaveProgressAllies();
        }
        else
            instance.LoadProgressAllies();

        if (!File.Exists(Application.persistentDataPath + instance.folderDestination + instance.nameRuteFaseProgress))
        {
            GameManager.GetInstance().fase = 0;
            GameManager.GetInstance().etapa = 0;
            instance.SaveProgressEtapaAndFase();
        }
        else
            instance.LoadProgressEtapas();
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
                if (l_row == 1)
                {
                    l_allie.id = l_id;
                    l_allie.currentLevel = 1;
                    l_allie.name = valor[0];
                    /*switch(int.Parse(valor[1]))
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
                    //añadir mas cosas*/
                }
                else if (l_row >= 4 && l_row <= 203)
                {
                    if (valor[1] != "")
                    {
                        l_allie.maxLevel = int.Parse(valor[0]);
                        l_allie.lifeList.Add(int.Parse(valor[1]));
                        l_allie.atackList.Add(int.Parse(valor[2]));
                        //l_allie.defensList.Add(int.Parse(valor[3]));
                        l_allie.atackSpeed = float.Parse(valor[3]);
                    }
                }



                if (l_row == 202)
                {
                    l_row = 0;
                    l_id++;
                    GameManager.GetInstance().listInfoAllies.Add(l_allie);
                }

                if (valor.Length == 0)
                    l_ended = true;
            }
            instance.SaveBaseOfDates();
        }
        else Debug.Log("no existe el csv");

    }

    private IEnumerator CopyPalabrasBinaryToPersistentPath() //en un futuro guardar por fecha y mirar si la fecha es la misma y si es diferente cambiar
    {

        //Where to copy the db to
        string dbDestination = Application.persistentDataPath + folderDestination + "/" + nameRute;

        //Check if the File do not exist then copy it

        //Where the db file is at
        string dbStreamingAsset = Application.streamingAssetsPath + "/" + nameRuteOriginal;

        byte[] result;

        //Read the File from streamingAssets. Use WWW for Android
        if (dbStreamingAsset.Contains("://") || dbStreamingAsset.Contains(":///"))
        {
            WWW www = new WWW(dbStreamingAsset);
            yield return www;
            result = www.bytes;
        }
        else
        {
            result = File.ReadAllBytes(dbStreamingAsset);
        }

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(dbDestination)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dbDestination));
        }

        //Copy the data to the persistentDataPath where the database API can freely access the file
        File.WriteAllBytes(dbDestination, result);

        if (File.Exists(Application.persistentDataPath + folderDestination + "/" + lastFecha + originalPath))
            File.Delete(Application.persistentDataPath + folderDestination + "/" + lastFecha + originalPath);

        Inicialice();

    }


    private void SaveBaseOfDates()
    {
        Debug.Log("Guardada la Base de Datos de Allies");

        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Create(Application.streamingAssetsPath + "/" + nameRuteOriginal);

        BaseOfAllies l_datos = new BaseOfAllies();
        l_datos.infoAllies = GameManager.GetInstance().listInfoAllies;

        l_bf.Serialize(l_file, l_datos);

        l_file.Close();

        if (File.Exists(Application.streamingAssetsPath + "/" + lastFecha + originalPath))
            File.Delete(Application.streamingAssetsPath + "/" + lastFecha + originalPath);
    }

    private void LoadBaseOfDates()
    {

        Debug.Log("Cargada la Base de Datos de Allies");
        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Open(Application.persistentDataPath + folderDestination + "/" + nameRute, FileMode.Open);

        BaseOfAllies l_datos = (BaseOfAllies)l_bf.Deserialize(l_file);
        GameManager.GetInstance().listInfoAllies = l_datos.infoAllies;

        l_file.Close();
    }

    public void SaveProgressAllies()
    {
        Debug.Log("Progreso Guardado");

        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Create(Application.persistentDataPath + folderDestination + nameRutePlayerSave);

        ProgressPlayerAllies l_datos = new ProgressPlayerAllies();
        l_datos.alliesOwned = GameManager.GetInstance().alliesOwned;
        l_datos.alliesNotOwned = GameManager.GetInstance().alliesNotOwned;

        l_bf.Serialize(l_file, l_datos);

        l_file.Close();
    }

    private void LoadProgressAllies()
    {

        Debug.Log("Cargado el progreso de los aliados");
        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Open(Application.persistentDataPath + folderDestination + nameRutePlayerSave, FileMode.Open);

        ProgressPlayerAllies l_datos = (ProgressPlayerAllies)l_bf.Deserialize(l_file);
        GameManager.GetInstance().alliesOwned = l_datos.alliesOwned;
        GameManager.GetInstance().alliesNotOwned = l_datos.alliesNotOwned;

        l_file.Close();
    }

    public void SaveProgressEtapaAndFase()
    {
        Debug.Log("Progreso de fases y etapas Guardado");

        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Create(Application.persistentDataPath + folderDestination + nameRuteFaseProgress);

        ProgresPlayerEtapasAndFases l_datos = new ProgresPlayerEtapasAndFases();
        l_datos.fase = GameManager.GetInstance().fase;
        l_datos.etapa = GameManager.GetInstance().etapa;

        l_bf.Serialize(l_file, l_datos);

        l_file.Close();
    }

    private void LoadProgressEtapas()
    {

        Debug.Log("Cargado el progreso de las etapas");
        BinaryFormatter l_bf = new BinaryFormatter();
        FileStream l_file = File.Open(Application.persistentDataPath + folderDestination + nameRuteFaseProgress, FileMode.Open);

        ProgresPlayerEtapasAndFases l_datos = (ProgresPlayerEtapasAndFases)l_bf.Deserialize(l_file);
        GameManager.GetInstance().fase = l_datos.fase;
        GameManager.GetInstance().etapa = l_datos.etapa;

        l_file.Close();
    }

}

[Serializable]
public class BaseOfAllies
{
    public List<InfoAllies> infoAllies = new List<InfoAllies>();
}

[Serializable]
public class ProgressPlayerAllies
{
    public List<InfoAllies> alliesOwned = new List<InfoAllies>();
    public List<InfoAllies> alliesNotOwned = new List<InfoAllies>();
}
[Serializable]
public class ProgresPlayerEtapasAndFases
{
    public int fase;
    public int etapa;
}