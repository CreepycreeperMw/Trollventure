using UnityEngine;

public class EventCollider : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var gObj in gameObjects)
        {
            EventExecutor test = gObj.GetComponent<EventExecutor>();
            test.OnTrigger(collision.gameObject);
            test.OnTrigger(collision.gameObject, this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var gObj in gameObjects)
        {
            EventExecutor test = gObj.GetComponent<EventExecutor>();
            test.OnTrigger(collision.gameObject);
            test.OnTrigger(collision.gameObject, this);
        }
    }
}
