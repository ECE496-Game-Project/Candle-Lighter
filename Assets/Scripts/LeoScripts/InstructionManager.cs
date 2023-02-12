using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.LeosScripts.Instruction;
using Assets.Scripts.Light;
using System.Runtime.Serialization;

public class InstructionManager : MonoBehaviour
{
    public GameObject[] _instructionImagePrefabs;

    //public InstructionDynamicLib _instructionDynamicLib;

    private InstructionLibraryUI _instructionLibraryUI;

    public int InstructionLibSize
    {
        get { return _instructionLibraryUI.InstructionLibSize; }
    }

    //public InstructionDynamicSet _instructionDynamicSet;

    private InstructionSetUI _instructionSetUI ;

    public int InstructionSetSize
    {
        get { return _instructionSetUI.InstructionSetSize;}
    }


    private void Awake()
    {
        GameObject instructionLibraryUIObject = GameObject.Find("InstructionLibrary");
        if (instructionLibraryUIObject == null) { Debug.LogError("can not find object InstructionLibrary"); }
        _instructionLibraryUI = instructionLibraryUIObject.GetComponent<InstructionLibraryUI>();
        if (_instructionLibraryUI == null) { Debug.LogError("Object Instruction Library does not have InstructionLibraryUI Component"); }

        GameObject instructionSetUIObject = GameObject.Find("InstructionSet");
        if (instructionSetUIObject == null) { Debug.LogError("can not find object InstructionSet"); }
        _instructionSetUI = instructionSetUIObject.GetComponent<InstructionSetUI>();
        if (_instructionSetUI == null) { Debug.LogError("Object InstructionSet does not have InstructionSetUI Component"); }

    }

    public void ExecuteInstruction(List<InstructionType> patch, IInstrcutionExecutable target)
    {
        for (int i = 0; i < patch.Count; i++)
        {
            InstructionType instruction = patch[i];

            if (instruction >= InstructionType.UP_INSTRUCT && instruction <= InstructionType.RIGHT_INSTRUCT)
            {
                target.MovementExecute((Direction)instruction);
            }
            else if (instruction == InstructionType.ACTIVATE_INSTRUCT)
            {
                target.ActivateExecute();
            }
        }
    }

    public void AddInstructionToLibFromOutside(InstructionType instruction)
    {
        GameObject card = _instructionImagePrefabs[(int)instruction];

        _instructionLibraryUI.AddInstruction(card);
    }

    public void PackInstructionToLight(LightPath curlightpath)
    {
        // reference copy curlightpath.InstructionSets
        curlightpath._instructionSet = _instructionSetUI.GetInstructionList();


        // clear all UI binding

        _instructionSetUI.ClearInstructions();
    }
}
interface IInstructionTransf
{
    public List<InstructionType> _instructionSet { get ; set ; }
}
