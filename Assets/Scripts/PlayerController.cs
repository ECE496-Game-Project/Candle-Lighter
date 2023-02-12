using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.Light;
using System;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private DirectionReference _directionReference;

    // Instruction
    [SerializeField]
    private InstructionManager _instructionManager;

    private InputAction _moveAction;
    private InputAction _shootAction;

    private Vector2 _direction;

    /// <summary>
    /// Whether the character is in the animation
    /// </summary>
    private bool _isMoving;

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
    /// amount of length for each move
    /// </summary>
    [SerializeField]
    private int _step = 1;

    /// <summary>
    /// Used for animation
    /// </summary>
    
    private float _timer = 0;

    // total amount of time it takes to do the animation
    private float _duration;

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

        //_character = this.transform.GetChild(0);
        //Character c = _character.GetComponent<Character>();
        //if (c == null) Debug.LogError("Character does not have character script");

        //c.OnCharacterCollisionEnter.AddListener(OnCharacterCollisionEnter);
    }


    // Start is called before the first frame update
    void Start()
    {
        _direction = new Vector2(0, 0);
        _isMoving = false;
        _hasInput = false;   
    }


    private void StartMoving(InputAction.CallbackContext context)
    {

        Debug.Log("startMoving");

        _direction = context.ReadValue<Vector2>();
        _direction = FilterDirection(_direction);
        _hasInput = true;
        if (_isMoving) { return; }



        SetUpMovement();

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

    private void ChangeDirection()
    {
        Vector3 targetDirection = _directionReference.ScreenDirectionToWorldDirecton(_direction);
        Vector3 currentDirection = transform.forward;

        //Debug.Log(targetDirection);
        //Debug.Log(currentDirection);
        float angle = 0;

        float value = Vector3.Dot(targetDirection, currentDirection);
        //Debug.Log(value);
        if (value < -0.1)
        {
            if (value < -0.9) { angle = 180; }
        }
        else
        {
            Vector3 rotation = Vector3.Cross(targetDirection, currentDirection);
            //Debug.Log(rotation);
            if (rotation.y > 0.9)
            {
                angle = -90;
            }
            else if (rotation.y < -0.9)
            {
                angle = 90;
            }
        }


        //Debug.Log(angle); 
        transform.Rotate(Vector3.up, angle);
    }

    private void SetUpMovement()
    {
        ChangeDirection();

        if (!canMove()) return;
        _startingPosition = transform.position;
        _destination = _startingPosition + _step * new Vector3(_direction.x, 0, _direction.y);
        _timer = 0;
        _duration = _timeToMove;
        _isMoving = true;
    }

    private bool canMove()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position + 0.5f * Vector3.up, this.transform.forward, out hit, _step, ~(1 << 8))){
            return false;
        }
        return true;
    }

    private void SetUpMovement(Vector3 start, Vector3 end)
    {
        Vector3 direction3D = (end - start).normalized;
        Vector2 direction2D = new Vector2(Mathf.Round(direction3D.x), Mathf.Round(direction3D.y));
        _direction = FilterDirection(direction2D);

        ChangeDirection();
        _startingPosition = start;
        _destination = end;

        float distance = Vector3.Magnitude(end - start);

        float unit = distance / _step;

        _duration = _timeToMove * unit;
        _timer = 0;
        _isMoving = true;
    }

    private void StopMoving(InputAction.CallbackContext context)
    {
        _direction = new Vector2(0, 0);
        _hasInput = false;
    }

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

    //public void OnCharacterCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Enter Collision");

    //    SetUpMovement(transform.position, _startingPosition);

    //    //transform.position = _character.position;
    //    //_character.localPosition = Vector3.zero;
    //    //Debug.Log($"starting position: {_startingPosition}");
    //    //Debug.Log($"Destination: {_destination}");
    //    //Debug.Log($"timer: {_timer}");
    //    //Debug.Log($"Duration: {_duration}");
    //}

    void Update()
    {

        if (!_isMoving) { return; }

        _timer += Time.deltaTime;

        //Debug.Log(_timer);
        transform.position = Vector3.Lerp(_startingPosition, _destination, _timer / _duration);
        //Debug.Log(transform.position);
        
        if (_timer >= _duration)
        {

            if (!_hasInput)
            {
                _isMoving = false;
            }
            else
            {
                SetUpMovement();
            }  
        }
    }
}
