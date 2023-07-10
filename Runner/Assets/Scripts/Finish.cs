using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class Finish : MonoBehaviour
{
    public static bool IsGameFinished;
    public static Action GameFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameFinished();
            IsGameFinished = true;
        }
    }
}
