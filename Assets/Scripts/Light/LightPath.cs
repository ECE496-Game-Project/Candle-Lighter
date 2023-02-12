using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.LeosScripts.Instruction;
using Assets.Scripts.Landscape;
namespace Assets.Scripts.Light {
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

            StartCoroutine(DestoryLightPath());
        }

        IEnumerator DestoryLightPath() {
            yield return new WaitForSeconds(_destoryTime);
            foreach (Transform section in this.transform) {
                GameObject.Destroy(section.gameObject);
            }
            GameObject.Destroy(this.gameObject);
        }
    }
}