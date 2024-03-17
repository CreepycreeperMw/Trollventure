using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool active = true;
    private int sceneIndex = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active && collision.gameObject.CompareTag("player")) {
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
