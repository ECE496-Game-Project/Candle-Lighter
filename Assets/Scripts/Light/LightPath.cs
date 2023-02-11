using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.LeosScripts.Instruction;
using Assets.Scripts.Landscape;
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

    public class LightPath : MonoBehaviour, IInstructionTransf {
        #region GLOBAL VARIABLES
        private GameObject _lightSectionType;
        
        private int _lightMaxDispLevel;

        public const float _lightTravelTime = 0.1f; // 1 block appear second
        public const float _destoryTime = 1.0f;
        public const int _lowFreqInstruct = 20;
        public const int _lowFreqLightDistance = 5;
        public const int _highFreqLightDistance = 3;
        private Timer destoryTimer;

        public List<LightSection> _lightSectionList;

        public BaseLandscape _lightHitLandScape;
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
            if(_instructionSet.Count <= _lowFreqInstruct) {
                _lightMaxDispLevel = _lowFreqLightDistance;
            }
            else {
                _lightMaxDispLevel = _highFreqLightDistance;
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
                    _lightHitLandScape = hit.transform.gameObject.GetComponent<BaseLandscape>();
                }
            }


            for (int i = 1; i <= _lightDirDispLevel; i++) {
                GameObject lightSectionGO = Instantiate(
                    _lightSectionType,
                    this.transform.position + this.transform.forward * i,
                    Quaternion.identity,
                    this.transform
                );
            }
        }
        #endregion

        void LandscapeHandler() {
            if (_lightHitLandScape == null) return;
            _lightHitLandScape.LightInteract(this);
        }

        void Start() {

            InitDispersionLevel();
            InitLightSectionList();
            LandscapeHandler();

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