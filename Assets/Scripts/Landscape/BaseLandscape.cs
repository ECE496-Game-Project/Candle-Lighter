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

        public List<InstructionType> _instructionSet {
            get; set;
        }

        // only for BLACKBODY
        public virtual void LightInteract(LightPath curlight) {
            Debug.Log("BaseLandscape: LightInteract Triggered!");
        }

        public virtual void MovementExecute(Direction direction) {
            Debug.Log("BaseLandscape: MovementExecute Triggered!");
        }

        public virtual void ActivateExecute() {
            Debug.Log("BaseLandscape: ActivateExecute Triggered!");
        }
    }
}