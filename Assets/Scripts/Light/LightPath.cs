using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.LeosScripts.Instruction;

namespace Assets.Scripts.Light {
    
    public class Timer {
        public float _TimePeriod = 10;
        public float _CurTime = 0;

        public bool _TimerIsRunning { get; set; } = true;

        public Timer(float timePeriod) {
            this._TimePeriod = timePeriod;
            _CurTime = timePeriod;
        }

        // if zero, reset and return true
        public bool timeReach() {
            if (!_TimerIsRunning) return false;

            if (_CurTime < 0) {
                Debug.Log("Time has run out!");
                _CurTime = _TimePeriod;
                return true;
            }

            _CurTime -= Time.deltaTime;
            return false;
        }
    }
    
    public struct LightSection {
        /// <summary>
        /// <br>Animation Play Sequence</br>
        /// </summary>
        int _DispersionLevel;
        /// <summary>
        /// <br>LightSection GameObject</br>
        /// <br>created using prefab Instantiate after all section are computed</br>
        /// </summary>
        GameObject _SectionObj;
    }

    interface ILightInteract {
        void LightInteract(LightPath curlight);
    }

    public class LightPath : MonoBehaviour, IInstructionTransf {
        public Vector3 _position;
        public Vector3 _direction;
        private int _totalDispersionLevel;

        public const float _velocity = 0.1f; // 1 block appear second
        
        public readonly List<LightSection> _LightSectionList;

        public List<InstructionType> _InstructionSet {
            get; set;
        }

        void Start() {
            

            RaycastHit hit;
            if (Physics.Raycast(_position, _direction, out hit, Mathf.Infinity, ~(1 << 8))) {

                int a = (int)Mathf.Round(hit.distance);
                Debug.Log(a + "Did Hit" + (_position + _direction * a));
            }
        }
        void Update() {
            
        }
    }
}