using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float impulseForce = 10f;
    private bool ignoreNextCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
        {
        
            return; // Ignore this collision
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
}
