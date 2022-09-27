using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempOfflineApiManager : MonoBehaviour
{

    private void Start()
    {
        SetTempFishCardData();
    }

    private void SetTempFishCardData()
    {
        DataManager.Instance.MyPlayerData.FishCardData.Datas.Add(0,0);
        DataManager.Instance.MyPlayerData.FishCardData.Datas.Add(1000, 1);
        DataManager.Instance.MyPlayerData.FishCardData.Datas.Add(2000, 7);

        for (int i = 1; i <= 3; i++)
        {
            DataManager.Instance.MyPlayerData.FishCardData.Datas.Add(i, 10);
            DataManager.Instance.MyPlayerData.FishCardData.Datas.Add(i+1000, 10);
            DataManager.Instance.MyPlayerData.FishCardData.Datas.Add(i+2000, 10);
        }
    }
}
