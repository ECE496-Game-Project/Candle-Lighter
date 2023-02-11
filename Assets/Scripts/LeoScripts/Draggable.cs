using Assets.Scripts.LeosScripts.Instruction;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// responsible for each individual instruction card movement
/// and inovoke the event when input actions is performed
/// </summary>
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


    public UnityEvent<int, GameObject> OnInstructionCardStartDragging;

    public UnityEvent<GameObject> OnInstructionCardDragging;

    public UnityEvent<GameObject> OnInstructionCardEndDragging;

    public UnityEvent<GameObject> OnInstructionCardClicked;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (OnInstructionCardStartDragging != null)
        {
            OnInstructionCardStartDragging.Invoke(transform.GetSiblingIndex(), gameObject);
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


    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnInstructionCardClicked != null)
        {
            OnInstructionCardClicked.Invoke(gameObject);
        }
    }
}
