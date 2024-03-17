using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EventExecutor
{
    public void OnTrigger(GameObject gameObject) { }
    public void OnTrigger(GameObject gameObject, EventCollider eventTrigger) { }
    public void OnTriggerOff(GameObject gameObject) { }
    public void OnTriggerOff(GameObject gameObject, EventCollider eventTrigger) { }
}
