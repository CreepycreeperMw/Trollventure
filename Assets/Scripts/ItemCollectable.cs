using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectable : MonoBehaviour
{
    private int fruitsCollected = 0;
    [SerializeField] private Text counterTxt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("fruit"))
        {
            Destroy(collision.gameObject);
            fruitsCollected++;
            counterTxt.text = "Pro Counter: "+fruitsCollected;
        }
    }
}
