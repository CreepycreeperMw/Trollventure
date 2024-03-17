using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveAway : MonoBehaviour, EventExecutor
{
    [SerializeField] private float defaulSpeed = 1f;
    [SerializeField] private float eventSpeed = 20f;
    [SerializeField] private float speed = 0f;
    [SerializeField] private GameObject[] goals;
    [SerializeField] private int target;
    [SerializeField] private bool keepOnGoing = true;

    // Update is called once per frame
    void Update()
    {
        if(target >= 0 && target < goals.Length)
        {
            Vector2 pos = goals[target].transform.position;
            pos = new Vector2(pos.x, pos.y);
            if (Vector2.Distance(pos, transform.position) < 0.1f)
            {
                ReachGoal();
            }
            transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        }
    }

    private void ReachGoal()
    {
        speed = defaulSpeed;
        if(keepOnGoing) target++;
    }

    public void OnTrigger(GameObject gameObject, EventCollider trigger) {
        if(gameObject.CompareTag("player")) { 
            target = 0;
            speed = eventSpeed;
            Destroy(trigger.gameObject);
        }
    }
}
