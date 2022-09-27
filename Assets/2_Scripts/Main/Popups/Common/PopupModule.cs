using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupModule : MonoBehaviour
{
    public GameObject BasePopup;
    public GameObject[] HidePopups;


    public virtual void ExitPopup()
    {
        BasePopup.SetActive(true);
        for (int i = 0; i < HidePopups.Length; i++)
            HidePopups[i].SetActive(false);
    }



}
