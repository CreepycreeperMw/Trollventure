using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumKiller : MonoBehaviour, EventExecutor
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject overlay;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            float angle = Vector2.Angle(rb.velocity, transform.position - collision.gameObject.transform.position) - 180;
            Debug.Log(angle);
            if(angle < 90 && angle > -90) {
                PlayerLife.pl.Die();
            }
        }
    }

    public void OnTrigger(GameObject gameObject, EventCollider eventTrigger)
    {
        overlay.SetActive(false);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
