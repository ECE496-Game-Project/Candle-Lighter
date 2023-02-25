using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlParticle : MonoBehaviour
{
    private GameObject _particle;

    private void Awake()
    {
        _particle = gameObject.transform.Find("circular_light").gameObject;
    }

    void ActivateParticle()
    {
        _particle?.SetActive(true);
    }

    void DeactivateParticle()
    {
        _particle?.SetActive(false);
    }
}
