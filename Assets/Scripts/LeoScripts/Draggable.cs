using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform _rectTransform;

    private Transform _parentToReturnTo;

    public Transform ParentToReturnTo
    {
        get {return _parentToReturnTo; }
        set {_parentToReturnTo = value;} 
    }

    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private GameObject _placeHolderPrefab;


    public UnityEvent<int, GameObject> OnInstrctionCardStartDragging;

    public UnityEvent<GameObject> OnInstructionCardDragging;

    public UnityEvent<GameObject> OnInstructionCardEndDragging;

    public UnityEvent<GameObject> OnInstructionCardClicked;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        
        if (OnInstrctionCardStartDragging != null)
        {
            OnInstrctionCardStartDragging.Invoke(transform.GetSiblingIndex(), gameObject);
        }

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

        if (OnInstructionCardDragging != null)
        {
            OnInstructionCardDragging.Invoke(gameObject);
        }

        
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        

        if (OnInstructionCardEndDragging != null)
        {
            OnInstructionCardEndDragging.Invoke(gameObject);
        }
        Debug.Log("End Drag");

        if (ParentToReturnTo == null) Destroy(gameObject);
        _canvasGroup.blocksRaycasts = true;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnInstructionCardClicked != null)
        {
            OnInstructionCardClicked.Invoke(gameObject);
        }
    }
}
