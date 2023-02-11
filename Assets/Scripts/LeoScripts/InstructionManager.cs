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

    public InstructionDynamicLib _instructionDynamicLib;

    [SerializeField]
    private InstructionLibraryUI _instructionLibraryUI;

    public int InstructionLibSize
    {
        get { return _instructionDynamicLib._instructionLib.Count; }
    }

    public InstructionDynamicSet _instructionDynamicSet;

    [SerializeField]
    private InstructionSetUI _instructionSetUI;

    public int InstructionSetSize
    {
        get { return _instructionDynamicLib._instructionLib.Count; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _instructionDynamicLib = new InstructionDynamicLib();
        _instructionDynamicSet = new InstructionDynamicSet();
    }

    public void ExecuteInstruction(List<InstructionType> patch, IInstrcutionExecutable target)
    {
        for (int i = 0; i < patch.Count; i++)
        {
            InstructionType instruction = patch[i];

            if (instruction >= InstructionType.UP_INSTRUCT && instruction <= InstructionType.DOWN_INSTRUCT)
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

    }

    public void ClearSetInstructionFromOutside()
    {

    }

    public void AddInstructionToSetFromUI()
    {

    }


    public List<InstructionType> GetInstructionSetFromOutside()
    {
        return new List<InstructionType>();
    }

    public void PackInstructionToLight(LightPath curlightpath)
    {
        // reference copy curlightpath.InstructionSet
        curlightpath._instructionSet = _instructionDynamicSet._instructionSet;

        // reference set to null
        _instructionDynamicSet.Clear();
        
        // clear all UI binding
        
        _instructionSetUI.ClearInstructions(); 
    }
}
interface IInstructionTransf
{
    public List<InstructionType> _instructionSet { get ; set ; }
}
