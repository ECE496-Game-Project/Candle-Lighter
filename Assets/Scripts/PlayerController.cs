using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.Light;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private DirectionReference _directionReference;

    private InputAction _moveAction;
    private InputAction _shootAction;

    private Vector2 _direction;

    private bool _isMoving;

    public bool _hasInput;

    public Vector3 _startingPosition, _destination;

    [SerializeField]
    private float _timeToMove;

    [SerializeField]
    private int _step = 1;

    private float _timer = 0;

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _moveAction.performed += StartMoving;
        _moveAction.canceled += StopMoving;

        _shootAction = _playerInput.actions["Shoot"];
        _shootAction.performed += pressLight;
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
        _direction = context.ReadValue<Vector2>();

        _hasInput = true;


        if (_isMoving) { return; }

        if (_direction.x > 0)
        {
            _direction = new Vector2(1, 0);
        }
        else if (_direction.x < 0)
        {
            _direction = new Vector2(-1, 0);
        }
        else if (_direction.y > 0)
        {
            _direction = new Vector2(0, 1);
        }
        else
        {
            _direction = new Vector2(0, -1);
        }

        

        Vector3 targetDirection = _directionReference.ScreenDirectionToWorldDirecton(_direction);
        Vector3 currentDirection = transform.forward;

        Debug.Log(targetDirection);
        Debug.Log(currentDirection);
        float angle = 0;

        float value = Vector3.Dot(targetDirection, currentDirection);
        Debug.Log(value);
        if (value < -0.1)
        {
            if (value < -0.9) { angle = 180; }
        }
        else
        {
            Vector3 rotation = Vector3.Cross(targetDirection, currentDirection);
            Debug.Log(rotation);
            if (rotation.y > 0.9)
            {
                angle = -90;
            }else if (rotation.y < -0.9)
            {
                angle = 90;
            }
        }


        Debug.Log(angle); 
        transform.Rotate(Vector3.up, angle);


        SetUpMovement();

    }

    private void SetUpMovement()
    {
        _startingPosition = transform.position;
        _destination = _startingPosition + _step * new Vector3(_direction.x, 0, _direction.y);
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
    }

    void Update()
    {

        if (!_isMoving) { return; }

        _timer += Time.deltaTime;
        //Debug.Log(_timer);
        transform.position = Vector3.Lerp(_startingPosition, _destination, _timer / _timeToMove);
        //Debug.Log(transform.position);
        
        if (_timer >= _timeToMove)
        {
            if (!_hasInput)
            {
                _isMoving = false;
                //Debug.Log(1);
            }
            else
            {
                SetUpMovement();
                //Debug.Log(2);
            }
            
        }
    }
}
