using System.Collections;
using UnityEngine;
using Assets.Scripts.Light;

namespace Assets.Scripts.Landscape {
    public class ActivateBox : BaseLandscape {
        
        public Animator _Animator;
        public override void ActivateExecute() {
            if (!_Animator.GetBool("isActivated"))
                _Animator.SetBool("isActivated", true);

            else
                _Animator.SetBool("isActivated", false);
            
        }
    }
}