using System.Collections;
using UnityEngine;
using Assets.Scripts.Light;
using Assets.Scripts.LeosScripts.Instruction;
using System.Collections.Generic;

namespace Assets.Scripts.Landscape {
    public class BaseLandscape : MonoBehaviour, IInstrcutionExecutable, IInstructionTransf {

        public enum LandscapeType {
            BLACKBODY,
            HALFTRANSP,
            NONE
        }

        public LandscapeType _landscapeType;

        public InstructionManager _instructionManager;

        public List<InstructionType> _instructionSet {
            get; set;
        }

        // only for BLACKBODY
        public virtual void LightInteract(LightPath curlight) {
            //Debug.Log("BaseLandscape: LightInteract Triggered!");
            this._instructionSet = curlight._instructionSet;
            if (this._instructionSet == null) return;
            //for (int i = 0; i < this._instructionSet.Count; i++) {
            //    Debug.Log($"Instruction {this._instructionSet[i]}");
            //}
        }

        public virtual void MovementExecute(Direction direction) {
            //Debug.Log("BaseLandscape: MovementExecute Triggered!");
        }

        public virtual void ActivateExecute() {
            //Debug.Log("BaseLandscape: ActivateExecute Triggered!");
        }

        public virtual void Start() {
            _instructionManager = GameObject.Find("InstructionManager");
        }
    }
}