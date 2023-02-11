using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.LeosScripts.Instruction;
using Assets.Scripts.Light;

public class InstructionManager : MonoBehaviour {
    public RawImage[] _instructionImages;

    public InstructionDynamicLib _instructionDynamicLib;

    public int InstructionLibSize
    {
        get { return _instructionDynamicLib._instructionLib.Count; }
    }

    public InstructionDynamicSet _instructionDynamicSet;

    public int InstructionSetSize
    {
        get{ return _instructionDynamicLib._instructionLib.Count; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _instructionDynamicLib= new InstructionDynamicLib();
        _instructionDynamicSet= new InstructionDynamicSet();
    }

    public void PackInstructionToLight(LightPath curlightpath) {

        //SetClear
        // 1. clear all UI binding
        // 2. reference copy curlightpath.InstructionSet
        // 3. reference set to null

    }
}
interface IInstructionTransf {
    public List<InstructionType> _InstructionSet { get; set; }
}