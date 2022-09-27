using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPopupManager : PopupModule
{
    public GameObject PlayPopup;
    public GameObject SelectPopup;
    public GameObject SinglePopup;
    public GameObject TeamPopup;



    //public override  void ExitPopup()
    //{
    //    PlayPopup.SetActive(false);
    //    SelectPopup.SetActive(true);
    //    SinglePopup.SetActive(false);
    //    TeamPopup.SetActive(false);
    //}

    public void SelectMode(string modeName)
    {
        SelectPopup.SetActive(false);

        if (modeName == "Single") PlaySingle();
        else PlayTeam();
    }

    private void PlaySingle()
    {
        SinglePopup.SetActive(true);
    }

    private void PlayTeam()
    {
        TeamPopup.SetActive(true);
    }

   

}
