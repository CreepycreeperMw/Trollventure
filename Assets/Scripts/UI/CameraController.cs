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
    [SerializeField] private Vector2 maxDistance = new Vector2(maxXdistance, maxYdistance);
    Camera cam;

    [SerializeField] private bool followPlayer = true;
    [SerializeField] private Vector3 targetPoint = Vector3.zero;
    [SerializeField] private float speed = 0.2f;

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

            if (xOffset > maxXdistance)
            {
                x = xOffset - maxXdistance;
            }
            else if (-xOffset > maxXdistance)
            {
                x = xOffset + maxXdistance;
            }


            float yOffset = player.position.y - transform.position.y;
            float y = 0f;

            if (yOffset > maxYdistance)
            {
                y = yOffset - maxYdistance;
            }
            else if (-yOffset > maxYdistance)
            {
                y = yOffset + maxYdistance;
            }
            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        }
        else
        {
            if (speed == 0f) { return; }
            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                transform.position = targetPoint;
                speed = 0f;
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
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

    [ContextMenu("Set Max Distance")]
    private void setMaxDistance()
    {
        maxXdistance = maxDistance.x; maxYdistance = maxDistance.y;
    }

    public static CameraController activeCam {
        get
        {
            return mainCamera;
        }
    }
}
