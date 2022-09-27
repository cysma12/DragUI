using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCardManager : MonoBehaviour
{
    //Active Pos 말고 TacticPos로

    public static FishCardManager Instance;

    public GameObject FishCardObject;
    public GameObject EmptyObject;

    public Transform[] ScrollViewTransforms;
    public GameObject[] TacticPoses;

    private Transform _tempItemPos;

    private FishCardData _tempData;

  [SerializeField]
    private List<FishCard> _fishCardObjects; 

    //tempData
    public Color[] Colors;

    private FishCardState _fishCardState = FishCardState.up;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    public void InitFishCard()
    {
        _tempData = DataManager.Instance.MyPlayerData.FishCardData;

        foreach (var data in _tempData.Datas)
        { 
            ConvertState(data.Key);
            SetInitCardPos(data.Key,data.Value);   
        }
    }

    public void StartDrag(int cardNum)
    {
        ActiveEmptyObject(cardNum);
    }

    public void EndDragFishCard(int myCardNum, int yourCardNum)
    {

        SetCardPos(myCardNum, yourCardNum);
    }

    public void EndDragTacticPos(int cardNum, int activeNum, int tacticPosNum = 10)
    {
        SetTacticPos(cardNum, activeNum, tacticPosNum);
    }

    public void EndDragNull(int cardNum, int activeNum)
    {
        SetNullTag(cardNum, activeNum);
    }

    public void EndDrag(int cardNum, int activeNum, string tag = "", int tacticPosNum = 10)
    {
        if (tag == "FishCard")
        {

        }
        else if (tag == "TacticPos")
        {

        }
        else if (tag == "null")
        {
       
        }

 

        //if (activeNum > 3)
        //{
        //    //SetTacticPos(cardNum, activeNum);
        //}
        //else
        //{
        //    SetCardPos(cardNum, activeNum);
        //}
        //    if (_fishCardObjects[i].GetComponent<FishCard>().CardNumber == cardNum)
        //    { 
        //        EmptyObject.transform.SetParent(_fishCardObjects[i].transform.parent);
        //        EmptyObject.transform.SetSiblingIndex(_fishCardObjects[i].transform.GetSiblingIndex());
        //        break;
        //    }
        //}         

    }

    private void SetCardPos(int myCardNum, int yourCardNum)
    {

        for (int i = 0; i < _fishCardObjects.Count; i++)
        {
            if (_fishCardObjects[i].CardNumber == myCardNum)
            {
                for (int j = 0; j < _fishCardObjects.Count; j++)
                {
                    if (_fishCardObjects[j].CardNumber == yourCardNum)
                    {
                        //Debug.Log(myCardNum + ", " + yourCardNum);
                        ChangeCard(i,j);
                        break;
                    }
                }
                break;
            }
        }
    }

    private void SetTacticPos(int cardNum, int activeNum, int tacticPosNum)
    {
        if (activeNum < 10)
        {
            for (int i = 0; i < _fishCardObjects.Count; i++)
            {
                if (_fishCardObjects[i].ActiveNumber == activeNum)
                {
                    _fishCardObjects[i].transform.position = TacticPoses[tacticPosNum].transform.position;
                    _fishCardObjects[i].ActiveNumber = tacticPosNum;
                    DataManager.Instance.SaveFishCardData(cardNum, tacticPosNum);
                    //DataManager.Instance.MyPlayerData.FishCardData.Datas[cardNum] = tacticPosNum;
                    ActiveTacticCollider(true, activeNum);
                    ActiveTacticCollider(false, tacticPosNum);
                    break; 
                }
            }
        }
        else
        {
            DeactiveEmptyOjbect(cardNum);
        }
    }
    
    private void SetNullTag(int cardNum, int activeNum)
    {
        DeactiveEmptyOjbect(cardNum);
    }

    private void ChangeCard(int myCardCount, int yourCardCount)
    {
        int tempActiveMyNumber;
        int tempActiveYourNumber;
        int tempScrollViewNum;

        Vector2 tempmyPos;
        Vector2 tempYourPos;
          
        tempmyPos = _fishCardObjects[myCardCount].CurrentPos;
        tempYourPos = _fishCardObjects[yourCardCount].CurrentPos;
        tempActiveMyNumber = _fishCardObjects[myCardCount].ActiveNumber;
        tempActiveYourNumber = _fishCardObjects[yourCardCount].ActiveNumber;

        if (tempActiveMyNumber < 10) //클릭한 내카드가 TacticPos에 있을 때 
        {
            if (tempActiveYourNumber < 10) // 상황 1 : 상대방 카드가 TacticPos에 있을 경우 - ok
            {
                _fishCardObjects[myCardCount].transform.SetParent(_tempItemPos);
                _fishCardObjects[myCardCount].transform.position = tempYourPos;
                _fishCardObjects[myCardCount].CurrentPos = tempYourPos;
                _fishCardObjects[myCardCount].ActiveNumber = tempActiveYourNumber;
        
                _fishCardObjects[yourCardCount].transform.SetParent(EmptyObject.transform.parent);
                _fishCardObjects[yourCardCount].transform.position = tempmyPos;
                _fishCardObjects[yourCardCount].ActiveNumber = tempActiveMyNumber;
                _fishCardObjects[yourCardCount].transform.SetSiblingIndex(EmptyObject.transform.GetSiblingIndex());
                _fishCardObjects[yourCardCount].CurrentPos = tempmyPos;

                EmptyObject.transform.SetParent(_tempItemPos);
            }
            else // 상황 2 : 상대방 카드가 ScrollPos에 있을 경우
            {
                tempScrollViewNum = ConvertStateCardNum(_fishCardObjects[myCardCount].CardNumber);
                _fishCardObjects[myCardCount].transform.SetParent(ScrollViewTransforms[tempScrollViewNum]);
                _fishCardObjects[myCardCount].ActiveNumber = tempActiveYourNumber;
                _fishCardObjects[myCardCount].transform.SetSiblingIndex(0);
                _fishCardObjects[myCardCount].CurrentPos = _fishCardObjects[myCardCount].transform.position;

                _fishCardObjects[yourCardCount].transform.SetParent(_tempItemPos);
                _fishCardObjects[yourCardCount].transform.position = tempmyPos;
                _fishCardObjects[yourCardCount].ActiveNumber = tempActiveMyNumber;
                _fishCardObjects[yourCardCount].CurrentPos = tempmyPos;

                EmptyObject.transform.SetParent(_tempItemPos);
            }
        }
        else //클릭한 내카드가 Scroll View Pos에 있을 때
        {
            if (tempActiveYourNumber < 10) // 상황 3 : 상대방 카드가 TacticPos에 있을 경우
            {
                _fishCardObjects[myCardCount].transform.SetParent(_tempItemPos);
                _fishCardObjects[myCardCount].transform.position = tempYourPos;
                _fishCardObjects[myCardCount].CurrentPos = tempYourPos;
                _fishCardObjects[myCardCount].ActiveNumber = tempActiveYourNumber;      

                tempScrollViewNum = ConvertStateCardNum(_fishCardObjects[yourCardCount].CardNumber);
                _fishCardObjects[yourCardCount].transform.SetParent(ScrollViewTransforms[tempScrollViewNum]);
                _fishCardObjects[yourCardCount].ActiveNumber = tempActiveMyNumber;
                _fishCardObjects[yourCardCount].transform.SetSiblingIndex(0);
                _fishCardObjects[yourCardCount].CurrentPos = _fishCardObjects[yourCardCount].transform.position;

                EmptyObject.transform.SetParent(_tempItemPos);
            }
            else // 상황 2 : 상대방 카드가 ScrollPos에 있을 경우
            {
                tempScrollViewNum = ConvertStateCardNum(_fishCardObjects[myCardCount].CardNumber);

                if (tempScrollViewNum == ConvertStateCardNum(_fishCardObjects[yourCardCount].CardNumber))
                {
                    _fishCardObjects[myCardCount].transform.SetParent(ScrollViewTransforms[tempScrollViewNum]);
                    _fishCardObjects[myCardCount].ActiveNumber = tempActiveYourNumber;
                    _fishCardObjects[myCardCount].transform.SetSiblingIndex(_fishCardObjects[yourCardCount].transform.GetSiblingIndex());
                    _fishCardObjects[myCardCount].CurrentPos = _fishCardObjects[yourCardCount].transform.position;

                    _fishCardObjects[yourCardCount].ActiveNumber = tempActiveMyNumber;
                    _fishCardObjects[yourCardCount].transform.SetSiblingIndex(EmptyObject.transform.GetSiblingIndex());
                    _fishCardObjects[yourCardCount].CurrentPos = _fishCardObjects[myCardCount].transform.position;

                    EmptyObject.transform.SetParent(_tempItemPos);
                }
                else
                {
                    DeactiveEmptyOjbect(_fishCardObjects[myCardCount].CardNumber);
                    return;
                }
            }
        }
        DataManager.Instance.SaveFishCardData(_fishCardObjects[myCardCount].CardNumber, tempActiveYourNumber);
        DataManager.Instance.SaveFishCardData(_fishCardObjects[yourCardCount].CardNumber, tempActiveMyNumber);
        //DataManager.Instance.MyPlayerData.FishCardData.Datas[_fishCardObjects[yourCardCount].CardNumber] = tempActiveMyNumber;
        //Debug.Log("durlRKwl dhkstjd");
    }

    private void SetInitCardPos(int key, int value)
    {
        GameObject tempCard;     

        int transformNum = 0;

        if (_fishCardState == FishCardState.up) transformNum = 0;
        else if (_fishCardState == FishCardState.middle) transformNum = 1;
        else if (_fishCardState == FishCardState.down) transformNum = 2;



        //Locate Tactic Card 
        if (value < TacticPoses.Length)
        {
            tempCard = Instantiate(FishCardObject, _tempItemPos);
            //tempCard.transform.SetParent(_tempItemPos);
            tempCard.gameObject.transform.position = TacticPoses[value].transform.position;
            ActiveTacticCollider(false, value);
        }
        else
        {
            tempCard = Instantiate(FishCardObject, ScrollViewTransforms[transformNum]);
        }

        tempCard.GetComponent<FishCard>().CardNumber = key;
        tempCard.GetComponent<FishCard>().ActiveNumber = value;
        tempCard.GetComponent<FishCard>().Text.text = key.ToString();
        tempCard.GetComponent<Image>().color = Colors[transformNum];
        _fishCardObjects.Add(tempCard.GetComponent<FishCard>());
    }
      
    private void ActiveEmptyObject(int cardNum)
    {
        for (int i = 0; i < _fishCardObjects.Count; i++)
        {
            if (_fishCardObjects[i].GetComponent<FishCard>().CardNumber == cardNum)
            {
                EmptyObject.transform.SetParent(_fishCardObjects[i].transform.parent);
                EmptyObject.transform.SetSiblingIndex(_fishCardObjects[i].transform.GetSiblingIndex());
                break;
            }
        }
    }

    private void DeactiveEmptyOjbect(int cardNum)
    {
        for (int i = 0; i < _fishCardObjects.Count; i++)
        {
            if (_fishCardObjects[i].GetComponent<FishCard>().CardNumber == cardNum)
            {
                if (_fishCardObjects[i].GetComponent<FishCard>().ActiveNumber < 10)
                {
                    _fishCardObjects[i].ReturnCurrentPos();
                }
                else
                {
                    _fishCardObjects[i].transform.SetParent(EmptyObject.transform.parent);
                    _fishCardObjects[i].transform.SetSiblingIndex(EmptyObject.transform.GetSiblingIndex());
                    _fishCardObjects[i].ReturnCurrentPos();
                    EmptyObject.transform.SetParent(_tempItemPos);
      

                }
                break;
            }
        }
    }

    private void ActiveTacticCollider(bool isActive, int num)
    {
        TacticPoses[num].SetActive(isActive);
    
    }

    private void ConvertState(int cardNum)
    {
        if (cardNum < 1000) _fishCardState = FishCardState.up;
        else if (1000 <= cardNum && cardNum < 2000) _fishCardState = FishCardState.middle;
        else if(2000 <= cardNum && cardNum < 3000) _fishCardState = FishCardState.down;        
    }

    private int ConvertStateCardNum(int cardNum)
    {
        int tempNum = 0;

        if (cardNum < 1000) tempNum = 0;
        else if (1000 <= cardNum && cardNum < 2000) tempNum =1;
        else if (2000 <= cardNum && cardNum < 3000) tempNum = 2;
        return tempNum;
    }

    private void Init()
    {

        _tempItemPos = GameObject.FindGameObjectWithTag("TempItemPos").GetComponent<Transform>();
        for (int i = 0; i < ScrollViewTransforms.Length; i++)
            ScrollViewTransforms[i].DOMoveY(0,0);

    }


    private enum FishCardState
    {
        up, middle, down
    }

}

