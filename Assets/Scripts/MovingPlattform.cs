using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlattform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints = new GameObject[0];
    private int activeWaypoint = 0;
    public float speed = 3.5f;
    [SerializeField] private int xOffset = 0;
    [SerializeField] private int yOffset = 0;

    // Start is called before the first frame update
    private void Start()
    {
        if (waypoints.Length == 0) return;
        Vector2 pos = waypoints[0].transform.position;
        transform.position = new Vector2 (pos.x + xOffset, pos.y + yOffset);
    }

    // Update is called once per frame
    private void Update()
    {
        if (waypoints.Length == 0) return;
        Vector2 pos = waypoints[activeWaypoint].transform.position;
        pos = new Vector2 (pos.x + xOffset, pos.y + yOffset);
        if (Vector2.Distance(pos, transform.position) < 0.1f)
        {
            activeWaypoint++;
            if (activeWaypoint == waypoints.Length)
            {
                activeWaypoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
