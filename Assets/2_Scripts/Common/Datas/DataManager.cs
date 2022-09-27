using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private PlayerData _playerData = new PlayerData();

    private void Awake()
    {
        Instance = this;
    }

    public void SaveFishCardData(int cardNum, int activeNum)
    {
        _playerData.FishCardData.Datas[cardNum] = activeNum;
    }

    public PlayerData MyPlayerData
    {
        get { return _playerData; }
        set { _playerData = value; }
    }    

}
