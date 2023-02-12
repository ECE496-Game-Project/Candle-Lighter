using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InstructionLibraryUI : MonoBehaviour
{

    [SerializeField]
    private InstructionSetUI _instructionSetUI;

    public int InstructionLibSize
    {
        get { return transform.childCount; }
    }

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

    private void SubscribeToInstruction(Draggable child)
    {
        child.OnInstructionCardStartDragging.AddListener(OnChildStartDragging);
    }

    void OnChildStartDragging(int index, GameObject child)
    {
        //Debug.Log("Hello");
        Draggable childDraggable = child.GetComponent<Draggable>();
        childDraggable.OnInstructionCardStartDragging.RemoveListener(OnChildStartDragging);
        _instructionSetUI.AddInstruction(childDraggable);

        GameObject newChild = Instantiate(child, transform);
        newChild.transform.SetSiblingIndex(index);
        Draggable newChildDraggable = newChild.GetComponent<Draggable>();
        newChildDraggable.OnInstructionCardStartDragging.AddListener(OnChildStartDragging);
    }


    public void AddInstruction(GameObject cardPrefab)
    {
        GameObject card = Instantiate(cardPrefab, transform);
        Draggable draggable = card.GetComponent<Draggable>();
        SubscribeToInstruction(draggable); 
    }

}
