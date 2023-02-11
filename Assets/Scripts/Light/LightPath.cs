using System.Collections;
using UnityEngine;
using System.Collections.Generic;
namespace Assets.Scripts.Light {
    
    public class Timer {
        public float timePeriod = 10;
        public float curTime = 0;

        public bool timerIsRunning { get; set; } = true;

        public Timer(float timePeriod) {
            this.timePeriod = timePeriod;
            curTime = timePeriod;
        }

        // if zero, reset and return true
        public bool timeReach() {
            if (!timerIsRunning) return false;

            if (curTime < 0) {
                Debug.Log("Time has run out!");
                curTime = timePeriod;
                return true;
            }

            curTime -= Time.deltaTime;
            return false;
        }
    }
    
    public struct LightSection {
        /// <summary>
        /// <br>Animation Play Sequence</br>
        /// </summary>
        int DispersionLevel;
        /// <summary>
        /// <br>LightSection GameObject</br>
        /// <br>created using prefab Instantiate after all section are computed</br>
        /// </summary>
        GameObject sectionObj;
    }

    public class LightPath : MonoBehaviour {
        public Vector3 position;
        public Vector3 direction;
        private int totalDispersionLevel;

        public const float velocity = 0.1f; // 1 block appear second
        
        public readonly List<int> InstructSet;
        public readonly List<LightSection> LightSectionList;


 
        void Start() {
            new List<string>()
            {
                "carrot",
                "fox",
                "explorer"
            };

            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity, ~(1 << 8))) {

                int a = (int)Mathf.Round(hit.distance);
                Debug.Log(a + "Did Hit" + (position + direction * a));
            }
        }
        void Update() {
            
        }
    }
}