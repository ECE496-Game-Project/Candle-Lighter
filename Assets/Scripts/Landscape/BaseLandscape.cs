using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Landscape {
    public class BaseLandscape : MonoBehaviour {

        public enum LandscapeType {
            BLACKBODY,
            HALFTRANSP,
            NONE
        }

        public LandscapeType type;
    }
}