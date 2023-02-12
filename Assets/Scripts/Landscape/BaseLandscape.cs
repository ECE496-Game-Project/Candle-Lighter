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
            Debug.Log("BaseLandscape: LightInteract Triggered!");
            this._instructionSet = curlight._instructionSet;

            if (this._instructionSet == null)
            {
                GameObject textBox = gameObject.GetComponent<ReadText>().textBox;
                ShowInfo(textBox);
                return;
            }

            _instructionManager.ExecuteInstruction(_instructionSet, this);

            for (int i = 0; i < this._instructionSet.Count; i++)
            {
                Debug.Log($"Instruction {this._instructionSet[i]}");
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
        }

        public void ShowInfo(GameObject textBox)
        {
            if (!textBox.GetComponent<TextBoxViewer>().isLocked)
            {
                Debug.Log("Text Box is Unlocked");
                List<string> textlines = gameObject.GetComponent<ReadText>().textLines;
                float textSpeed = gameObject.GetComponent<ReadText>().textSpeed;
                textBox.GetComponent<TextBoxViewer>().isLocked = true;
                textBox.GetComponent<TextBoxViewer>().OpenTextBox(textlines);
                StartCoroutine(WaitForRead(textlines, textSpeed));
                CloseInfo(textBox);
            }
        }

        public void CloseInfo(GameObject textBox)
        {
            if (textBox.GetComponent<TextBoxViewer>().isLocked)
            {
                textBox.GetComponent<TextBoxViewer>().isLocked = false;
                textBox.GetComponent<TextBoxViewer>().CloseTextBox();
            }
        }

        IEnumerator WaitForRead(List<string> textlines, float textSpeed)
        {
            foreach(string line in textlines)
            {
                foreach(char c in line)
                {
                    Debug.Log("Wait a sec");
                    yield return new WaitForSeconds(textSpeed);
                }
            }
        }
    }
}