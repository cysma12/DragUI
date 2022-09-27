using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticCardManager : MonoBehaviour
{
    public static TacticCardManager Instance;

    public GameObject[] TacticPoses;
    public TacticCard[] TacticCards;


    private void Awake()
    {
        Instance = this;
    }


    public void InitCardPos()
    {
        //SetInitCardPos();
    }

    public void EndDrag(string tacticPosName, int cardNum)
    {
        //SetCardPos(tacticPosName, cardNum);
    }

    //private void SetInitCardPos()
    //{
    //    int tempCount = 0;

    //    TacticData tacticDatas = DataManager.Instance.MyPlayerData.TacticData;

    //    foreach (int value in tacticDatas.ActivedTacticPos.Values)
    //    {
    //        //Debug.Log(value);
    //        TacticCards[tempCount].gameObject.transform.position = TacticPoses[value].transform.position;
    //        TacticCards[tempCount].CurrentTacticPosName = TacticPoses[value].name;
    //        tempCount++;
    //    }
    //}
    //private void SetCardPos(string tacticPosName, int cardNum)
    //{
    //    int tempLocatedCardNum = DataManager.Instance.MyPlayerData.TacticData.ActivedTacticPos[cardNum];

    //    for (int i = 0; i < TacticPoses.Length; i++)
    //    {
    //        if (TacticPoses[i].name == tacticPosName)
    //        {
    //            TacticCards[cardNum].gameObject.transform.DOMove(TacticPoses[i].transform.position, 0.1f);



    //            for (int j = 0; j < TacticCards.Length; j++)
    //            {
    //                if (TacticCards[j].CurrentTacticPosName == tacticPosName)
    //                {
    //                    TacticCards[j].gameObject.transform.DOMove(TacticPoses[tempLocatedCardNum].transform.position, 0.1f);
    //                    TacticCards[j].CurrentTacticPosName = TacticCards[cardNum].CurrentTacticPosName;
    //                    DataManager.Instance.MyPlayerData.TacticData.ActivedTacticPos[j] = tempLocatedCardNum;
    //                }
    //            }

    //            DataManager.Instance.MyPlayerData.TacticData.ActivedTacticPos[cardNum] = i;

    //            break;
    //        }
    //    }
    //}
}
