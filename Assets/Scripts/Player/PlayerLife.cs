using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private static bool _isDead = false;
    private static bool fallDamage = false;
    public static bool safe = false;
    private static DateTime fallTime = DateTime.Now;
    public static PlayerLife pl;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fallDamage = false;
        _isDead = false;
        safe = false;
        pl = this;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("trap"))
        {
            Die();
        }
    }
    public void Die()
    {
        _isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if(!_isDead && transform.position.y < -10 && !safe)
        {
            Die();
        }
    }

    public static void canDieToFallDamage(bool state)
    {
        if(state==true)
        {
            if(fallDamage!=true) fallTime = DateTime.Now;
            fallDamage = true;
        }
        else
        {
            fallDamage=false;
        }
    }
    public static bool isDead() { return _isDead; }
}
