//lets add some target
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Score scoreManager;

    //this method is called whenever a collision is detected
    private void OnCollisionEnter(Collision collision)
    {

        if (scoreManager != null) {
            //on collision adding point to the score
            scoreManager.AddPoint();
            
            // printing if collision is detected on the console
            Debug.Log("Collision Detected");
            
            //after collision is detected destroy the gameobject
            // if (collision.gameObject.CompareTag("Player")) {
            //     Debug.Log("Player collided with target!");
            // } else {
                Destroy(gameObject);
            // }
        }
        else {
            Debug.LogWarning("ScoreManager is not assigned to the Target script!");
        }

        Debug.Log("Collision Detected");
        
    }
}