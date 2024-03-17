using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GandalfEncounter1 : MonoBehaviour
{
    [SerializeField] private GameObject gandalf;
    private Animator anim;
    private bool triggered = false;

    private void Start()
    {
        anim = gandalf.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!triggered && collision.gameObject.CompareTag("player"))
        {
            triggered = true;
            anim.SetBool("isHitting", true);
        }
    }
}
