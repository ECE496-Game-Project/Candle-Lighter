using System.Collections;
using UnityEngine;
using Assets.Scripts.Light;

namespace Assets.Scripts.Landscape {
    public class ActivateBox : BaseLandscape {
        

        public Animator _Elevator_Animator;
        public override void ActivateExecute() {
            if (!_Elevator_Animator.GetBool("isActivated"))
                _Elevator_Animator.SetBool("isActivated", true);

            else
                _Elevator_Animator.SetBool("isActivated", false);
            
        }
    }
}