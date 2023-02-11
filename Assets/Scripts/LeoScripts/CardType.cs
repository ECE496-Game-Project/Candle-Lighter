using Assets.Scripts.LeosScripts.Instruction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardType : MonoBehaviour
{
    [SerializeField]
    private InstructionType _type;

    public InstructionType Type
    {
        get { return _type; }
    }
}
