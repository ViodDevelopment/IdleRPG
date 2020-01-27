using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ally, enemy, instance;

    public AllyController[] allies;
    public List<GameObject> enemies;


    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < 4; i++)
        {
            instance = Instantiate(ally);
            instance.transform.position = Vector3.right * i * 1.2f;
            

            instance = Instantiate(enemy);
            instance.transform.position = Vector3.one * i * 1.2f;
            enemies.Add(instance);
        }

        allies = FindObjectsOfType<AllyController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AllyController al in allies)
        {
            al.UpdateAlly();
        }
    }
}
