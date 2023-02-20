using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.Light;
using System;
using Cinemachine.Utility;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private DirectionReference _directionReference;

    // Instruction
    private InstructionManager _instructionManager;

    private InputAction _moveAction;
    private InputAction _shootAction;

    private Vector2 _direction;

    /// <summary>
    /// Whether the character is in the animation
    /// </summary>
    private bool _isMoving;


    public bool IsMoving
    {
        set { _isMoving = value; }
    }
    /// <summary>
    /// whether the player is keep pressing the button
    /// </summary>
    private bool _hasInput;

    /// <summary>
    /// the starting position and destination of the movement animation
    /// </summary>
    private Vector3 _startingPosition, _destination;

    /// <summary>
    /// The amount of time(s) it takes to move one units
    /// </summary>
    [SerializeField]
    private float _timeToMove;

    /// <summary>
    /// The angle in degree the character will rotate every second 
    /// </summary>
    [SerializeField]
    private float _turnSpeed = 90;

    [SerializeField]
    private float _tapThreshold = 0.2f;

    /// <summary>
    /// amount of length for each move
    /// </summary>
    [SerializeField]
    private int _step = 1;

    /// <summary>
    /// The length of each cell
    /// </summary>
    [SerializeField]
    private float _cellSize = 1f;

    //private Transform _character;
    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _moveAction.performed += StartMoving;
        _moveAction.canceled += StopMoving;

        _shootAction = _playerInput.actions["Shoot"];
        _shootAction.performed += pressLight;

        if (this.transform.childCount > 1)
        {
            Debug.LogWarning("The player object has multiple children, it might not be able to find the character model");
        }

        GameObject instructionManagerObject = GameObject.Find("InstructionManager");
        if (instructionManagerObject == null) Debug.LogError("Can not find Instruction Manager");

        _instructionManager = instructionManagerObject.GetComponent<InstructionManager>();
        if (_instructionManager == null) Debug.LogError("Object InstructionManager does not have InstructionManger Script");

        gameObject.tag = "Player";

    }


    // Start is called before the first frame update
    void Start()
    {
        _direction = new Vector2(0, 0);
        _isMoving = false;
        _hasInput = false;   
    }
    
    /// <summary>
    /// round the transform position to the grid position
    /// </summary>
    void CleanPosition()
    {
        Vector3 newLocation = this.transform.position;
        newLocation.x = Mathf.Round(newLocation.x / _cellSize) * _cellSize;
        newLocation.z = Mathf.Round(newLocation.z / _cellSize) * _cellSize;

        this.transform.position = newLocation;
    }

    IEnumerator Move()
    {
        _isMoving = true;
        float timer, duration;
        while (_hasInput)
        {
            CleanPosition();
            // set up position
            Vector3 startingPosition = transform.position;
            Vector3 direction = _directionReference.ScreenDirectionToWorldDirecton(_direction);
            Vector3 destination = startingPosition + _step * direction;


            //******************************* Turning Animation************************************************//
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            float rotateAngle = Math.Abs(targetRotation.eulerAngles.y - startRotation.eulerAngles.y);

            
            if (rotateAngle > 180) rotateAngle -= 180;


            timer = 0;
            duration = rotateAngle/ _turnSpeed;

            if (rotateAngle != 0)
            {
                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timer / duration);
                    yield return null;
                }

                if (timer < _tapThreshold) yield return new WaitForSeconds(_tapThreshold - timer);

                continue;
            }
            

            
            //******************************* Moving Animation************************************************//
            if (!canMove() || IsEdge()) break;

            // set up timer
            timer = 0;
            duration = _timeToMove;
            // move to destination
            while (timer < duration)
            {

                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(startingPosition, destination, timer / duration);
                yield return null;
            }
            if (timer < _tapThreshold) yield return new WaitForSeconds(_tapThreshold - timer);

        }

        _isMoving = false;

    }


    private void StartMoving(InputAction.CallbackContext context)
    {

        _direction = context.ReadValue<Vector2>();
        _direction = FilterDirection(_direction);
        _hasInput = true;
        if (_isMoving) { return; }
            

        StartCoroutine(Move());

    }

    private void StopMoving(InputAction.CallbackContext context)
    {
        _direction = new Vector2(0, 0);
        _hasInput = false;
    }

    private Vector2 FilterDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            direction = new Vector2(1, 0);
        }
        else if (direction.x < 0)
        {
            direction = new Vector2(-1, 0);
        }
        else if (direction.y > 0)
        {
            direction = new Vector2(0, 1);
        }
        else
        {
            direction = new Vector2(0, -1);
        }

        return direction;
    }


    /// <summary>
    /// change the character orientation if difference
    /// </summary>
    /// <returns>whether the character actually turn</returns>
    //private float ChangeDirection()
    //{
    //    Vector3 targetDirection = _directionReference.ScreenDirectionToWorldDirecton(_direction);
    //    Vector3 currentDirection = transform.forward;

    //    //Debug.Log(targetDirection);
    //    //Debug.Log(currentDirection);
    //    float angle = 0;

    //    float value = Vector3.Dot(targetDirection, currentDirection);
    //    //Debug.Log(value);
    //    if (value < -0.1)
    //    {
    //        if (value < -0.9) { angle = 180; }
    //    }
    //    else
    //    {
    //        Vector3 rotation = Vector3.Cross(targetDirection, currentDirection);
    //        //Debug.Log(rotation);
    //        if (rotation.y > 0.9)
    //        {
    //            angle = -90;
    //        }
    //        else if (rotation.y < -0.9)
    //        {
    //            angle = 90;
    //        }
    //    }


    //    //Debug.Log(angle); 
    //    transform.Rotate(transform.up, angle);
    //    return angle;
    //}

    //private void SetUpMovement()
    //{
    //    ChangeDirection();

    //    if (!canMove()) return;
    //    _startingPosition = transform.position;
    //    _destination = _startingPosition + _step * new Vector3(_direction.x, 0, _direction.y);
    //    _timer = 0;
    //    _duration = _timeToMove;
    //    _isMoving = true;
    //}

    private bool canMove()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position + 0.5f * transform.up, this.transform.forward, out hit, _step, ~(1 << 8))){
            return false;
        }
        return true;
    }

    private bool IsEdge()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + 0.5f * transform.up + 1f * transform.forward, - this.transform.up, out hit, 0.6f, ~(1 << 8)))
        {
            
            return false;
        }

        return true;
    }
    //private void SetUpMovement(Vector3 start, Vector3 end)
    //{
    //    Vector3 direction3D = (end - start).normalized;
    //    Vector2 direction2D = new Vector2(Mathf.Round(direction3D.x), Mathf.Round(direction3D.y));
    //    _direction = FilterDirection(direction2D);

    //    ChangeDirection();
    //    _startingPosition = start;
    //    _destination = end;

    //    float distance = Vector3.Magnitude(end - start);

    //    float unit = distance / _step;

    //    _duration = _timeToMove * unit;
    //    _timer = 0;
    //    _isMoving = true;
    //}

    

    private void pressLight(InputAction.CallbackContext context) {
        LightPath lightPath = Instantiate(
            (GameObject)Resources.Load("Light/LightPath"),
            this.transform.position,
            Quaternion.LookRotation(this.transform.forward, Vector3.up),
            this.transform
        ).GetComponent<LightPath>();

        lightPath.InitExternInfo((GameObject)Resources.Load("Light/LightSection_Robot"));
        _instructionManager.PackInstructionToLight(lightPath);
        //for(int i = 0; i < lightPath._instructionSet.Count; i++)
        //{
        //    Debug.Log($"Instruction {lightPath._instructionSet[i]}");
        //}
    }

    public void OnCharacterCollisionEnter(Collision collision)
    {
        Debug.Log("Enter Collision");

        //SetUpMovement(transform.position, _startingPosition);

        //transform.position = _character.position;
        //_character.localPosition = Vector3.zero;
        //Debug.Log($"starting position: {_startingPosition}");
        //Debug.Log($"Destination: {_destination}");
        //Debug.Log($"timer: {_timer}");
        //Debug.Log($"Duration: {_duration}");
    }

    //void Update()
    //{

    //    if (!_isMoving) { return; }

    //    _timer += Time.deltaTime;

    //    //Debug.Log(_timer);
    //    transform.position = Vector3.Lerp(_startingPosition, _destination, _timer / _duration);
    //    //Debug.Log(transform.position);

    //    if (_timer >= _duration)
    //    {

    //        if (!_hasInput)
    //        {
    //            _isMoving = false;
    //        }
    //        else
    //        {
    //            SetUpMovement();
    //        }  
    //    }
    //}
}
