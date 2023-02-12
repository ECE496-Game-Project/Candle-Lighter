using System.Collections;
using UnityEngine;
using Assets.Scripts.Light;
namespace Assets.Scripts.Landscape {
    public class StaticBox : BaseLandscape {

        public override void LightInteract(LightPath curlight) {
            base.LightInteract(curlight);
            Debug.Log("StaticBox: LightInteract Triggered");
            
        }

        // Use this for initialization
        public override void Start() {
            base.Start();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}