using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TacticCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int CardNum = 0;

    private RectTransform _rectTransform;

    private Canvas _mainCanvas;

    private Transform _parentTransform;
    private Transform _tempItemPosTransform;

    private DragState _dragState = DragState.wait;

    private string _tempTacticPosName = "";
    private string _currentTacticPosName = "";

    public string CurrentTacticPosName
    {
        get { return _currentTacticPosName; }
        set { _currentTacticPosName = value; }
    }

    private void Awake() 
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        _parentTransform = transform.parent;
        _tempItemPosTransform = GameObject.FindGameObjectWithTag("TempItemPos").GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_dragState == DragState.wait) return;

        if (collision.tag == "TacticPos") _tempTacticPosName = collision.name;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _tempTacticPosName = _currentTacticPosName;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        SetDragState(true);
    }

    public void OnDrag(PointerEventData eventData)
    { 
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDragState(false);
        TacticCardManager.Instance.EndDrag(_tempTacticPosName, CardNum);
        _currentTacticPosName = _tempTacticPosName;
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
            _rectTransform.SetParent(_parentTransform);
            _dragState = DragState.wait;
        }
    }

}