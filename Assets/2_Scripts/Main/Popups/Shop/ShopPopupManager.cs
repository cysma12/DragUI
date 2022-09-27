using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopupManager : PopupModule
{
    public static ShopPopupManager Instance;

    public GameObject[] ItemScrollViews;

    private void Awake()
    {
        Instance = this;
        ShowItemScrollView(0);
    }

    public void ShowItemScrollView(int items)
    {
        ActiveItemScrollView(items);
    }

    public void ResetScrollVIew()
    {
        ShowItemScrollView(0);
    }
    private void ActiveItemScrollView(int items)
    {



        for (int i = 0; i < ItemScrollViews.Length; i++)
        {
            if (i == items)
                ItemScrollViews[i].SetActive(true);
            else
                ItemScrollViews[i].SetActive(false);
        }
    }
}
