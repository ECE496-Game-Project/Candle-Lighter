using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private PlayerInput _playerInput;

    private InputAction _moveAction;

    private Vector2 _direction;

    [SerializeField]
    private float _speed;

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _moveAction.performed += StartMoving;
        _moveAction.canceled += StopMoving;
    }


    // Start is called before the first frame update
    void Start()
    {
        _direction = new Vector2(0, 0);

    }


    private void StartMoving(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();

    }

    private void StopMoving(InputAction.CallbackContext context)
    {
        _direction = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (_direction == Vector2.zero)
        {
            return;
        }
        Vector3 move = new Vector3(_direction.x, 0, _direction.y);
        move *= _speed * Time.deltaTime;
        transform.Translate(move);
    }
}
