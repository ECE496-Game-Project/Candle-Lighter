using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.LeosScripts.Instruction;

namespace Assets.Scripts.Light {
    
    public class Timer {
        public float _TimePeriod = 10;
        public float _CurTime = 0;
        public bool _TimerIsRunning = false;

        public Timer(float timePeriod) {
            this._TimePeriod = timePeriod;
            _CurTime = timePeriod;
            _TimerIsRunning = true;
        }

        // if zero, reset and return true
        public bool timeTick() {
            if (!_TimerIsRunning) return false;

            if (_CurTime < 0) {
                _CurTime = _TimePeriod;
                return true;
            }

            _CurTime -= Time.deltaTime;
            return false;
        }
    }
    
    public class LightSection {
        public int _dispersionLevel;
        public GameObject _lightSectionGO;
    }

    interface ILightInteract {
        void LightInteract(LightPath curlight);
    }

    public class LightPath : MonoBehaviour, IInstructionTransf {
        #region GLOBAL VARIABLES
        private GameObject _lightSectionType;
        
        private int _lightMaxDispLevel;

        public const float _lightTravelTime = 0.1f; // 1 block appear second
        public const float _destoryTime = 1.0f;
        private Timer destoryTimer;

        public List<LightSection> _lightSectionList;

        public GameObject _lightHitLandScape;
        private int _lightDirDispLevel;


        public List<InstructionType> _instructionSet {
            get; set;
        }
        #endregion

        #region LIGHT CLASS INITALIZATION
        public void InitExternInfo(GameObject lightSectionType) {
            _lightSectionType =lightSectionType;
        }
        private void InitDispersionLevel() {
            if(_instructionSet.Count <= 3) {
                _lightMaxDispLevel = 5;
            }
            else {
                _lightMaxDispLevel = 3;
            }
        }
        private void InitLightSectionList() {

            _lightSectionList = new List<LightSection>();
            _lightHitLandScape = null;

            _lightDirDispLevel = _lightMaxDispLevel;
            RaycastHit hit;

            if (
            Physics.Raycast(this.transform.position + 0.5f * Vector3.up, this.transform.forward, out hit, Mathf.Infinity, ~(1 << 8))
            ) {
                int tmpDistance = (int)Mathf.Round(hit.distance - 0.5f);
                if (_lightDirDispLevel >= tmpDistance) {
                    _lightDirDispLevel = tmpDistance;
                    _lightHitLandScape = hit.transform.gameObject;
                }
            }


            for (int i = 1; i <= _lightDirDispLevel+1; i++) {
                GameObject lightSectionGO = Instantiate(
                    _lightSectionType,
                    this.transform.position + this.transform.forward * i,
                    Quaternion.identity,
                    this.transform
                );
            }
        }
        #endregion

        void Start() {
            _instructionSet = new List<InstructionType>();
            _instructionSet.Add(InstructionType.UP_INSTRUCT);
            _instructionSet.Add(InstructionType.DOWN_INSTRUCT);
            _instructionSet.Add(InstructionType.LEFT_INSTRUCT);
            _instructionSet.Add(InstructionType.ACTIVATE_INSTRUCT);

            InitDispersionLevel();
            InitLightSectionList();

            destoryTimer = new Timer(_destoryTime);
        }
        void Update() {
            if (destoryTimer.timeTick()) {
                foreach (Transform section in this.transform) {
                    GameObject.Destroy(section.gameObject);
                }
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}