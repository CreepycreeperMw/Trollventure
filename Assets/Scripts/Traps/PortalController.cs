using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PortalController : MonoBehaviour, EventExecutor
{
    private bool isMoving = false;
    private float xCord;
    private GameObject player;
    [SerializeField] private GameObject[] bgs;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void OnTrigger(GameObject gameObject, EventCollider eventTrigger)
    {
        if (gameObject.CompareTag("player"))
        {
            Destroy(eventTrigger);
            isMoving = !isMoving;
            if(isMoving)
            {
                xCord = transform.position.x - gameObject.transform.position.x;
            }
        }
    }
    private void Update()
    {
        if(isMoving && (xCord > transform.position.x - player.transform.position.x))
        {
            float x = xCord + player.transform.position.x;
            float removed = transform.position.x - x;

            transform.position = new Vector2(xCord + player.transform.position.x, transform.position.y);

            foreach (var bg in bgs)
            {
                SpriteRenderer comp = bg.GetComponent<SpriteRenderer>();
                comp.size = new Vector2(comp.size.x + removed, comp.size.y);

                bg.transform.position = new Vector2(bg.transform.position.x - removed/2, bg.transform.position.y);
            }
        }
    }
}
