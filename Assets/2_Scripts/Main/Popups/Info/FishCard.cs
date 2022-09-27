using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public TextMeshProUGUI Text;

    private RectTransform _rectTransform;

    private Transform _tempItemPosTransform;

    private GameObject _tempFishCardObject;

    private Vector2 _currentPos;
    private Vector2 _tempPps;

    private Canvas _mainCanvas;

    private int _cardNum = 0;
    private int _activeNum = 0;
    private string _tagName = "null";
    private string _tempColliderName = "";

    private int _tempTacticPosNum = 10;

    private DragState _dragState = DragState.wait;

    
    private void Awake() 
    {
        _rectTransform = GetComponent<RectTransform>();
        _tempItemPosTransform = GameObject.FindGameObjectWithTag("TempItemPos").GetComponent<Transform>();
        _mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

    }

    private void Start()
    {
        _currentPos = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_dragState == DragState.wait) return;

        _tempColliderName = collision.name;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_dragState == DragState.wait) return;

       // if (_tempColliderName != "") return;

        if (collision.tag == "FishCard")
        {
            _tagName = "FishCard";
            _tempFishCardObject = collision.gameObject;
        }
        else if (collision.tag == "TacticPos")
        {
            _tagName = "TacticPos";
            //_tempTacticPosNum = int.TryParse(collision.name, out 10)//int.Parse(collision.name);
            _tempTacticPosNum = int.Parse(collision.name);
        }
        else
        {
            _tagName = "null";
        }
        //Debug.Log(collision.tag);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_dragState == DragState.wait) return;

        _tagName = "null";
        _tempColliderName = "";
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        FishCardManager.Instance.StartDrag(_cardNum);
        SetDragState(true);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (_dragState == DragState.wait) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _rectTransform.position = Input.mousePosition;
        _rectTransform.anchoredPosition +=  eventData.delta / _mainCanvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //FishCardManager.Instance.EndDrag(_cardNum, _activeNum, _tagName, _tempTacticPosNum);
        if (_tagName == "FishCard") FishCardManager.Instance.EndDragFishCard(_cardNum, _tempFishCardObject.GetComponent<FishCard>().CardNumber);
        else if (_tagName == "TacticPos") FishCardManager.Instance.EndDragTacticPos(_cardNum, _activeNum, _tempTacticPosNum);
        else if (_tagName == "null") FishCardManager.Instance.EndDragNull(_cardNum, _activeNum);

            SetDragState(false);
        _currentPos = this.transform.position;
        _tagName = "null";
        _tempColliderName = "";
    }

    public void ReturnCurrentPos()
    {
        _rectTransform.position = _currentPos;
    }

    public Vector2 CurrentPos
    {
        get { return _currentPos; }
        set { _currentPos = value; } 
    }

    public int ActiveNumber
    {
        get { return _activeNum; }
        set { _activeNum = value; }
    }
    public int CardNumber
    {
        get { return _cardNum; }
        set { _cardNum = value;     }
    }

    private void SetDragState(bool isDrag)
    {
        if (isDrag)
        {
            _dragState = DragState.drag;
            _rectTransform.SetParent(_tempItemPosTransform);
        }
        else
        {
            _dragState = DragState.wait;
        }
    }

    //private void CheckCardPos()
    //{
    //    if (_tempTacticPosTransform != null)
    //    {
    //        this.gameObject.transform.SetParent(_tempTacticPosTransform);
    //        _rectTransform.DOLocalMove(Vector2.zero, 0.1f);
    //    }
    //    else
    //    {       
    //        _rectTransform.DOMove(_parentTransform.GetChild(0).position, 0.1f)
    //            .OnComplete(delegate() {
    //                this.gameObject.transform.SetParent(_parentTransform);
    //                //this.gameObject.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f),0.1f);
    //            });

    //    }
    //}


}

