using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GandalfSurprise : MonoBehaviour, EventExecutor
{
    private GameObject player;
    [SerializeField] private float speed = 2f;
    bool isFollowing = false;
    Vector3 orPos = Vector3.zero;

    private void Start()
    {
        orPos = transform.position;
    }

    void Update()
    {
        if(player == null) { return; }

        if(isFollowing)
        {
            Vector3 targetPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, orPos, speed * Time.deltaTime);
        }
    }

    public void OnTrigger(GameObject gameObject, EventCollider eventTrigger)
    {
        if(gameObject.CompareTag("player"))
        {
            player = gameObject;
            isFollowing = true;
            PlayerMovement.blockMovement = true;
        }
    }

    public void OnTriggerOff(GameObject gameObject, EventCollider eventTrigger)
    {
        if(gameObject.CompareTag("player"))
        {
            isFollowing = false;
            PlayerMovement.blockMovement = false;
        }
    }
}
