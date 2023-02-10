using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MovingPhoton : MonoBehaviour
{
    [SerializeField]
    private float _speed;


    [SerializeField]
    private Vector3 _direction;


    [SerializeField]
    LineRenderer _lineRenderer;

    private float _lineInitialHalfLength=0.1f;

    private float _distanceTraveled=0f;

    private float _maxDistance=5f;


    
    
    void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        _direction = Vector3.Reflect(_direction, normal);
    }

    /// <summary>
    /// set the prameter of the photon
    /// </summary>
    /// <param name="speed"> the speed of the photon</param>
    /// <param name="direction">the travelling direction of the photon</param>
    /// <param name="lineInitialHalfLength">the half length of the line that is drawn perpendicular to 
    /// the direction of the photon when the photon is spawned</param>
    /// <param name="max_distance">maximum distance the photon can travel before it disppear</param>
    public void SetParameter(float speed, Vector3 direction, float lineInitialHalfLength, float max_distance)
    {
        _speed = speed;
        _direction = direction;
        _lineInitialHalfLength = lineInitialHalfLength;
        _maxDistance = max_distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_maxDistance <= _distanceTraveled)
        {
            Destroy(gameObject);
        }

        transform.Translate(Time.deltaTime * _speed * _direction);
        Vector3 position = transform.position;

        Vector3 lineDirection = new Vector3(-_direction.z, 0, _direction.x);
        _distanceTraveled +=  Time.deltaTime * _speed;

        float lineMagnitude = _lineInitialHalfLength * (1 + _distanceTraveled);
        _lineRenderer.SetPosition(0, position + lineMagnitude * lineDirection);
        _lineRenderer.SetPosition(1, position - lineMagnitude * lineDirection);
    }
}
