using Assets.Scripts.LeosScripts.Instruction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionSetUI : MonoBehaviour
{
    [SerializeField]
    private InstructionManager _instructionManager;
    [SerializeField]
    private GameObject _placeHolderPrefab;

    private GameObject _placeHolder;

    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            Draggable child = transform.GetChild(i).GetComponent<Draggable>();
            if (child == null) Debug.LogError($"Instruction library {i}th child does not have Draggable Script");

            SubscribeToInstruction(child);
        }
    }

    void OnChildStartDragging(int index, GameObject child)
    {

        Debug.Log("Hello");
        Draggable childDraggable = child.GetComponent<Draggable>();
        childDraggable.ParentToReturnTo = transform;

        _placeHolder = Instantiate(_placeHolderPrefab, transform);
        _placeHolder.transform.SetSiblingIndex(index);



    }

    void OnChildDragging(GameObject child)
    {
        int newSiblingIndex = _placeHolder.transform.GetSiblingIndex();
        float childLocation = child.transform.position.x;

        for (int i = 0; i < transform.childCount; i++)
        {

            float sibling = transform.GetChild(i).transform.position.x;

            if (childLocation < sibling && i < newSiblingIndex ||
                childLocation > sibling && i > newSiblingIndex)
            {
                newSiblingIndex = i;
                break;
            }
        }

        _placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }

    void OnChildEndDraggin(GameObject child)
    {
        Draggable draggedObject = child.GetComponent<Draggable>();
        if (_placeHolder == null)
        {
            draggedObject.OnInstructionCardStartDragging.RemoveListener(OnChildStartDragging);
            draggedObject.OnInstructionCardDragging.RemoveListener(OnChildDragging);
            draggedObject.OnInstructionCardEndDragging.RemoveListener(OnChildEndDraggin);
            draggedObject.ParentToReturnTo = null;
            return;
        }
        else
        {
            draggedObject.ParentToReturnTo = this.transform;
            draggedObject.transform.SetParent(transform);
            draggedObject.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
            Destroy(_placeHolder);
            _placeHolder = null;
        }
    }

    void OnChildClicked(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    void SubscribeToInstruction(Draggable draggedObject)
    {
        draggedObject.OnInstructionCardStartDragging.AddListener(OnChildStartDragging);
        draggedObject.OnInstructionCardDragging.AddListener(OnChildDragging);
        draggedObject.OnInstructionCardEndDragging.AddListener(OnChildEndDraggin);
        draggedObject.OnInstructionCardClicked.AddListener(OnChildClicked);
    }

    public void AddInstruction(Draggable draggedObject)
    {

        SubscribeToInstruction(draggedObject);
        _placeHolder = Instantiate(_placeHolderPrefab, transform);
        draggedObject.ParentToReturnTo = transform;
    }



    
}