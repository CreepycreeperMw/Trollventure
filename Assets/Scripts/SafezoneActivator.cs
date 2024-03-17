using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafezoneActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("player")) PlayerLife.safe = true;
    }
}
