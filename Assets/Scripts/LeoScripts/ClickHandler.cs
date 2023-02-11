using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private RawImage _rawImagePrefab;

    [SerializeField]
    private GameObject _canvas;
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
        RawImage newImage = Instantiate(_rawImagePrefab, _canvas.transform);
        newImage.transform.position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
