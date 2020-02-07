
using UnityEngine;
using UnityEngine.UI;

public class PruebaParaLlamarBBDD : MonoBehaviour
{
    public Text texto;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = GameManager.GetInstance().listInfoAllies.Count.ToString()  +"  " + (int)Time.time;

    }
}
