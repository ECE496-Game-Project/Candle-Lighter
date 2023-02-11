using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.LeosScripts.Instruction
{
    public enum InstructionType
    {
        UP_INSTRUCT,
        DOWN_INSTRUCT,
        LEFT_INSTRUCT,
        RIGHT_INSTRUCT,
        ACTIVATE_INSTRUCT
    };

    public enum ErrorCode
    {
        SUCCESS,
        OUT_OF_BOUND,
        INST_ALREADY_EXIST
    }

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    public interface IInstrcutionExecutable
    {
        public void MovementExecute(Direction direction);

        public void ActivateExecute();
    }
    public class InstructionDynamicSet
    {
        public List<InstructionType> _instructionSet;

        public InstructionDynamicSet()
        {
            _instructionSet = new List<InstructionType>();  

        }

        public ErrorCode AddInstruction(InstructionType inst, int idx)
        {
            if (_instructionSet == null)
            {
                _instructionSet = new List<InstructionType>();
            }

            if (idx < 0) { 
                Debug.LogError("instruction set out of bound");
                return ErrorCode.OUT_OF_BOUND; 
            }

            _instructionSet.Insert(idx, inst);
            return ErrorCode.SUCCESS;
        }

        public ErrorCode DeleteInstruction(int idx)
        {

            if(idx < 0 || idx >= _instructionSet.Count)
            {
                Debug.LogError("instruction set out of bound");
                return ErrorCode.OUT_OF_BOUND;
            }


            return ErrorCode.SUCCESS;
        }

       

        public ErrorCode Clear()
        {
            _instructionSet = null;
            return ErrorCode.SUCCESS;
        }
    }   

    public class InstructionDynamicLib
    {
        public List<InstructionType> _instructionLib;

        public InstructionDynamicLib() {
            _instructionLib = new List<InstructionType>();
        }

        public ErrorCode AddInstruction(InstructionType instr)
        {
            for (int i = 0; i < _instructionLib.Count; i++)
            {
                if (_instructionLib[i] == instr)
                {
                    return ErrorCode.INST_ALREADY_EXIST;
                }
            }

            _instructionLib.Add(instr);
            return ErrorCode.SUCCESS;
        }
    }
}
