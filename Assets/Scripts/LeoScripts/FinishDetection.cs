using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDetection : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    String _nextScence;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.transform.parent.name);
        Transform player = collision.gameObject.transform.parent;
        
        if (collision.gameObject.transform.parent.name == "Player")
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.IsMoving = false;
            SceneManager.LoadScene(_nextScence);
        }
    }
}
