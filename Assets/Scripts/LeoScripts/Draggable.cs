using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;

    private Transform _parentToReturnTo;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        _parentToReturnTo = transform.parent;
        transform.SetParent(transform.parent.parent);

        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, 
            eventData.position, eventData.pressEventCamera,out var globalMousePosition))
        {
            _rectTransform.position = globalMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_parentToReturnTo);
        _canvasGroup.blocksRaycasts = true;
        Debug.Log("End Drag");
    }

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
