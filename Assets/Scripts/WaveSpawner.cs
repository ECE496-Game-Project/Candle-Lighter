using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private int _numOfPhotons = 50;

    [SerializeField]
    private MovingPhoton _photonPrefab;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _maxDistance;

    [SerializeField]
    private float _radius;
    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        Vector3 position = transform.position;

        float step = 2 * Mathf.PI / _numOfPhotons;

        float initialLineHalfLenth = Mathf.Sqrt(2 * _radius * (1 - Mathf.Cos(step))) / 2f;
        for(int i = 0; i < _numOfPhotons; i++)
        {
            float angle = step * i;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            MovingPhoton photon = Instantiate(_photonPrefab);

            photon.SetParameter(_speed, direction, initialLineHalfLenth, _maxDistance);
            photon.transform.position = position + 1f * direction;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
