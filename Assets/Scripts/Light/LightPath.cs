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
        int _dispersionLevel;
        /// <summary>
        /// <br>LightSection GameObject</br>
        /// <br>created using prefab Instantiate after all section are computed</br>
        /// </summary>
        Transform _sectionTransform;
    }

    interface ILightInteract {
        void LightInteract(LightPath curlight);
    }

    public class LightPath : MonoBehaviour, IInstructionTransf {
        private Vector3 _position;
        private Vector3 _direction;
        private GameObject _lightSectionType;
        private int _totalDispersionLevel;

        public const float _velocity = 0.1f; // 1 block appear second
        
        public readonly List<LightSection> _lightSectionList;

        public List<InstructionType> _instructionSet {
            get; set;
        }
        
        public void InitWorldSpaceInfo(Vector3 position, Vector3 direction, GameObject lightSectionType) {
            _position = position;
            _direction = direction;
            _lightSectionType =lightSectionType;
        }
        
        void InitDispersionLevel() {
            if(_instructionSet.Count <= 3) {
                _totalDispersionLevel = 5;
            }
            else {
                _totalDispersionLevel = 3;
            }
        }

        void Start() {
            _instructionSet = new List<InstructionType>();
            _instructionSet.Add(InstructionType.UP_INSTRUCT);
            _instructionSet.Add(InstructionType.DOWN_INSTRUCT);
            _instructionSet.Add(InstructionType.LEFT_INSTRUCT);
            _instructionSet.Add(InstructionType.ACTIVATE_INSTRUCT);

            InitDispersionLevel();

            RaycastHit hit;
            if (Physics.Raycast(_position, _direction, out hit, Mathf.Infinity, ~(1 << 8))) {
                Debug.Log(_position + "Did Hit" + _direction);
                for (int i = 0; i < (int)Mathf.Round(hit.distance); i++) {
                    LightPath lightPath = Instantiate(
                        _lightSectionType,
                        _position + _direction * i,
                        Quaternion.identity,
                        this.transform
                    ).GetComponent<LightPath>();
                }
            }
        }
        void Update() {
            
        }
    }
}