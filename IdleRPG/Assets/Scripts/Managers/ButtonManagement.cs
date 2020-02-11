using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagement : MonoBehaviour
{
    [HideInInspector] public bool musicActive = true;
    public GameObject[] CanvasList;

    [Header("Sprites")]
    public Sprite buttonActive;
    public Sprite buttonDesactive;


    #region Social Media Links
    public void TwitterAccess()
    {
        Application.OpenURL("https://twitter.com/ViODGamesStudio?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor");
    }

    public void InstagramAccess()
    {
        Application.OpenURL("https://twitter.com/ViODGamesStudio?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor");
    }
    public void DiscordAccess()
    {
        Application.OpenURL("https://twitter.com/ViODGamesStudio?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor");
    }

    public void GooglePlayAccess()
    {
        Application.OpenURL("https://twitter.com/ViODGamesStudio?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor");
    }
    #endregion

    public void ChangeButtonActivation(Image _Sprite)
    {
        if (_Sprite.sprite == buttonActive)
            _Sprite.sprite = buttonDesactive;
        else
            _Sprite.sprite = buttonActive;
    }

    public void ActivateOneCanvasDeselectTheRest(GameObject _CanvasToActivate)
    {
        for (int i = 0; i < CanvasList.Length; i++)
        {
            CanvasList[i].SetActive(false);
        }

        _CanvasToActivate.SetActive(true);
    }

    public void AlternatorButton(GameObject _ButtonChanger)
    {
        if (_ButtonChanger.activeSelf)
            _ButtonChanger.SetActive(false);
        else
            _ButtonChanger.SetActive(true);
    }

    public void ChangeCharacter(bool _Left)
    {
        if (_Left) { }

        else { }
        //ChangeCharacter 
    }
    
}
