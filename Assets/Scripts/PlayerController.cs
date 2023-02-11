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

    public GameObject _lightPath;

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

        if (_isMoving) { return; }
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
        Transform origin = this.transform.Find("origin");
        Transform foward = this.transform.Find("foward");

        LightPath lightPath = Instantiate(_lightPath, foward.position, Quaternion.identity, this.transform).GetComponent<LightPath>();
        lightPath._position = origin.position;
        lightPath._direction = foward.position - origin.position;
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
                Debug.Log(1);
            }
            else
            {
                SetUpMovement();
                Debug.Log(2);
            }
            
        }
    }
}
