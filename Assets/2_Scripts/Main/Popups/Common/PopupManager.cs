using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    //if you add main button and popup, you have to add popup and button. and you have to modify PopupManager script. 


    public GameObject[] PopupObjects;

    private void Awake()
    {
        OnAllPopup();
        
    }

    private void Start()
    {
        OffPopup();
    }

    public void OffPopup()
    {
        for (int i = 0; i < PopupObjects.Length; i++)
        {
            PopupObjects[i].SetActive(false);
        }
    }

    public void OnPopup(string popupName)
    {

        for (int i = 0; i < PopupObjects.Length; i++)
        {
            if (PopupObjects[i].name == popupName) PopupObjects[i].SetActive(true);
            else PopupObjects[i].SetActive(false);
        }
    }

    public void OnAllPopup()
    {
        for (int i = 0; i < PopupObjects.Length; i++)
        {
            PopupObjects[i].SetActive(true);
        }

    }

}


 
