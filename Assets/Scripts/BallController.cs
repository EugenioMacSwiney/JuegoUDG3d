using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float impulseForce = 3f;
    private bool ignoreNextCollision;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; 
    } 

    private void OnCollisionEnter(Collision collision)
    {
   
        // Increment score on collision
        if (ignoreNextCollision)
        {
        
            return; // Ignore this collision
        }
           DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
           if (deathPart)
           {
            GameManager.singleton.RestartLevel(); // Restart the level if colliding with a death part
           }

        rb.linearVelocity = Vector3.zero; // Reset velocity on collision
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // Apply impulse force
    
        ignoreNextCollision = true; // Set the flag to ignore the next collision
        Invoke("AllowNextCollision", 0.1f); // Allow the next collision after a short delay
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false; // Allow the next collision
    }
    public void ResetBall()
    {
        transform.position = startPosition; // Reset the ball's position to the initial position
      // Reset velocity
    }
    }
