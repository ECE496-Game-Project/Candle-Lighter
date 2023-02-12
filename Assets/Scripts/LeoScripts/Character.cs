using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityEvent<Collision> OnCharacterCollisionEnter;


    private void OnCollisionEnter(Collision collision)
    {
        if (OnCharacterCollisionEnter != null)
        {
            OnCharacterCollisionEnter.Invoke(collision);
        }
    }

}
