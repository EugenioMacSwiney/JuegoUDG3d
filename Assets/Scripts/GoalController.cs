using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.singleton.NextLevel();  
    } 
}