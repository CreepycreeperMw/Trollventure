using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private static float maxXdistance = 5.0f;
    private static float maxYdistance = 2.0f;
    Camera cam;

    bool followPlayer = true;
    Vector2 targetPoint = Vector2.zero;
    float speed = 0.2f;

    static private CameraController mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        mainCamera = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            float xOffset = player.position.x - transform.position.x;
            float x = 0f;

            if(xOffset > maxXdistance)
            {
                x = xOffset - maxXdistance;
            }
            else if(-xOffset > maxXdistance)
            {
                x = xOffset + maxXdistance;
            }


            float yOffset = player.position.y - transform.position.y;
            float y = 0f;

            if(yOffset > maxYdistance)
            {
                y = yOffset - maxYdistance;
            }
            else if(-yOffset > maxYdistance)
            {
                y = yOffset + maxYdistance;
            }
            transform.position = new Vector3(transform.position.x + x, transform.position.y+y, transform.position.z);
        }
        else
        {
            if (targetPoint == null || speed == 0f) { return; }
            if(Vector2.Distance(transform.position, targetPoint) < 0.1f)
            {
                transform.position = targetPoint;
                speed = 0f;
                return;
            }
            Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        }
    }

    public float zoom
    {
        get => cam.orthographicSize;
        set { cam.orthographicSize = value; }
    }

    public void setFocus(Vector2 pos, float transitionSpeed)
    {
        targetPoint = pos;
        followPlayer = false;
        speed = transitionSpeed;
    }

    public static CameraController activeCam {
        get
        {
            return mainCamera;
        }
    }
}
