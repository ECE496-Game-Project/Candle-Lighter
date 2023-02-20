using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    /// <summary>
    /// The original parent of the GameObject that is currently on the elevator
    /// </summary>
    private Transform _objectParent;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            _objectParent = collision.transform.parent;
            collision.transform.parent = this.transform;
            //Debug.Log(collision.transform.position);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.transform.parent = _objectParent;
            _objectParent = null;
        }
    }
}
