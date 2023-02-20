using System.Collections;
using UnityEngine;
using Assets.Scripts.Light;
using Assets.Scripts.LeosScripts.Instruction;
using System.Collections.Generic;
using TMPro;
using System.Threading;

namespace Assets.Scripts.Landscape {
    public class MotionBox : BaseLandscape, IInstrcutionExecutable
    {

        private Queue<Vector3> _movementBuffer;

        // private bool isMoving = false;

        private IInstrcutionExecutable _IInstructionExecutable;

        private DirectionReference _directionReference;

        private Vector3 _startingPosition, _destination;

        [SerializeField]
        private float _step = 1f;

        [SerializeField]
        private float _timeToMove = 0.5f;
        private float _timer;
        private float _duration;

        private bool _isMoving = false;

        private void SetUpMovement(Vector3 direction)
        {

            _startingPosition = transform.position;
            _destination = _startingPosition + _step * direction;
            _timer = 0;
            _duration = _timeToMove;
            _isMoving = true;
        }

        private bool canMove(Vector3 direction)
        {
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position + 0.5f * Vector3.up, direction, out hit, _step, ~(1 << 8)))
            {
                return false;
            }
            return true;
        }

        public override void LightInteract(LightPath curlight) {
            base.LightInteract(curlight);
            
            
        }

        override
        public void MovementExecute(Direction direction)
        {
            _movementBuffer.Enqueue(_directionReference.DirectionToWorldDirection(direction));
        }

        override
        public void ActivateExecute()
        {
            return;
        }

        public void Awake()
        {
            _IInstructionExecutable = GetComponent<IInstrcutionExecutable>();
            GameObject player = GameObject.Find("Player");
            if (player == null) { Debug.LogError("Can not find Player"); }

            _directionReference = player.GetComponent<DirectionReference>();
            if (_directionReference == null) { Debug.LogError("Can not find direction reference"); }
            _movementBuffer= new Queue<Vector3>();
        }

        // Use this for initialization
        public override void Start() {
            base.Start();

        }

        // Update is called once per frame
        void Update() {
            if (_movementBuffer.Count == 0 && !_isMoving) { return; }


            if (!_isMoving)
            {
                Vector3 direction = _movementBuffer.Dequeue();
                if (!canMove(direction))
                {
                    return;
                }

                SetUpMovement(direction);
            }

            _timer += Time.deltaTime;

            transform.position = Vector3.Lerp(_startingPosition, _destination, _timer / _duration);



            if (_timer >= _duration)
            {
                _isMoving = false;
            }
        }
    }
}