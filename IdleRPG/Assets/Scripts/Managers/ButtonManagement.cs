using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagement : MonoBehaviour
{
    [HideInInspector] public bool musicActive = true;
    public GameObject[] canvasList;

    [Header("Sprites")]
    public Sprite buttonActive;
    public Sprite buttonDesactive;

    public GameObject prefabCharacterListImage;
    public GameObject contentScroll;
    public GameObject contentScrollEvolution;
    public GameObject contentSize;
    public GameObject contentSizeEvolution;




    private void Update()
    {
       
      
    }

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
        for (int i = 0; i < canvasList.Length; i++)
        {
            canvasList[i].SetActive(false);
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

    #region CharacterMenus
    public void ChangeCharacter(bool _Left)
    {
        if (_Left) { }

        else { }
        //ChangeCharacter 
    }
    
    public void ChangeFormation()
    {

    }

    public void FilterOwned()
    {

    }

    public void FilterRace()
    {

    }

    public void FilterRarity()
    {

    }
    #endregion

    public void GenerateCharacterList()
    {
        int l_Columns = 4;
        int l_Rows = 4/*numchar /4 */;
        int l_Size = 250;
        int l_Dist = 25;
        
        for (int i = 0; i < l_Rows; i++)
        {
            for (int j = 0; j < l_Columns; j++)
            {
                GameObject l_Prefab = Instantiate(prefabCharacterListImage);
                l_Prefab.transform.SetParent(contentScroll.transform);
                l_Prefab.GetComponent<RectTransform>().localPosition = new Vector3( l_Size * j + 10 * j,-( l_Dist* i + l_Size*i),0.1f);
                l_Prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                contentSize.GetComponent<RectTransform>().sizeDelta =new Vector2(0, l_Rows * l_Size * 1.25f); 
            }
        }
    }

    public void GenerateCharacterListEvolution()
    {
        int l_Columns = 4;
        int l_Rows = 4/*numchar /4 */;
        int l_Size = 250;
        int l_Dist = 25;
        
        for (int i = 0; i < l_Rows; i++)
        {
            for (int j = 0; j < l_Columns; j++)
            {
                GameObject l_Prefab = Instantiate(prefabCharacterListImage);
                l_Prefab.transform.SetParent(contentScrollEvolution.transform);
                l_Prefab.GetComponent<RectTransform>().localPosition = new Vector3( l_Size * j + 10 * j,-( l_Dist* i + l_Size*i),0.1f);
                l_Prefab.GetComponent<RectTransform>().localScale = new Vector3( 1,1,1);
                contentSizeEvolution.GetComponent<RectTransform>().sizeDelta =new Vector2(0, l_Rows * l_Size * 1.25f); 
            }
        }
    }

    public void SelectedChar( GameObject _SelectedChar)
    {

    }

    public void MoveIcon(GameObject _panelToMove)
    {
        if (_panelToMove.activeSelf == false)
        {
           // iconToMove.transform.position = newPosition.position;
            _panelToMove.SetActive(true);
        }
        else
        {
           // iconToMove.transform.position = firstPosition.position;
            _panelToMove.SetActive(false);
        }
    }

}
