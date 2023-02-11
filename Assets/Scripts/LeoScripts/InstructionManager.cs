using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.LeosScripts.Instruction;
using Assets.Scripts.Light;
public class InstructionManager : MonoBehaviour
{
    public GameObject[] _instructionImagePrefabs;

    public InstructionDynamicLib _instructionDynamicLib;

    public int InstructionLibSize
    {
        get { return _instructionDynamicLib._instructionLib.Count; }
    }

    public InstructionDynamicSet _instructionDynamicSet;

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

        //SetClear
        // 1. clear all UI binding
        // 2. reference copy curlightpath.InstructionSet
        // 3. reference set to null
    }
}
interface IInstructionTransf
{
    public List<InstructionType> _InstructionSet { get; set; }
}
