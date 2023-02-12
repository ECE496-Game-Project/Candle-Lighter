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

        [SerializeField] private TextBoxViewer _textBoxViewer;
        [SerializeField] private ReadText _objTextDescription;


        public List<InstructionType> _instructionSet {
            get; set;
        }

        // only for BLACKBODY
        public virtual void LightInteract(LightPath curlight) {
            Debug.Log("BaseLandscape: LightInteract Triggered!");
            this._instructionSet = curlight._instructionSet;

            if (this._instructionSet == null) return;
            if (this._instructionSet.Count == 0)
            {
                if (!_textBoxViewer.isLocked) {
                    _textBoxViewer.isLocked = true;
                    StartCoroutine(EmptyInstructLightShowText());
                    _textBoxViewer.isLocked = false;
                }
            }
            else {
                _instructionManager.ExecuteInstruction(_instructionSet, this);
            }
        }

        public virtual void MovementExecute(Direction direction) {
            Debug.Log("BaseLandscape: MovementExecute Triggered!");
        }

        public virtual void ActivateExecute() {
            Debug.Log("BaseLandscape: ActivateExecute Triggered!");
        }

        public virtual void Start() {
            _instructionManager = GameObject.FindObjectsOfType<InstructionManager>()[0];

            _objTextDescription = gameObject.GetComponent<ReadText>();
            _textBoxViewer = _objTextDescription.textBox.GetComponent<TextBoxViewer>();
        }

        IEnumerator EmptyInstructLightShowText() {
            _textBoxViewer.OpenTextBox(_objTextDescription.textLines);
            yield return new WaitForSeconds(2.0f);
            _textBoxViewer.CloseTextBox();
        }

    }
}