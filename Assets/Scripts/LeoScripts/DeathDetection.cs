using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathDetection : MonoBehaviour
{

    Transform character;
    void ReLoadScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    private void Awake()
    {
        if (transform.childCount > 1) Debug.LogWarning("More than 1 child for Player, I may not find the" +
            "model object");
        character = transform.GetChild(0);
    }

    private void Update()
    {
        if (character.transform.position.y <= -20 ) ReLoadScence();
    }
}
