using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        DOVirtual.DelayedCall(1, delegate () {
            //TacticCardManager.Instance.InitCardPos();
            FishCardManager.Instance.InitFishCard();
        });
    }
}
