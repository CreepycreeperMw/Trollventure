using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] private float height = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(5f, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Jump")
        {
            rb.velocity = new Vector2(5f, height);
        }
        else if(collision.gameObject.name=="End")
        {
            transform.position = new Vector2(-11f, transform.position.y);
        }
    }
}
