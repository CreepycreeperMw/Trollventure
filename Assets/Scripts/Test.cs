using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string text = "Hi, how are you doing?";
    public float time = 1.0f;
    public Vector3 offset = Vector3.zero;

    [ContextMenu("Spawn Chat Bubble")]
    public void SpawnChatBubble()
    {
        TextBubble.Create(gameObject, text, offset);
    }

    [ContextMenu("Update Textbubble")]
    public void Change()
    {
        Transform bubble = transform.Find("ChatBubble(Clone)");
        TextBubble comp = bubble.GetComponent<TextBubble>();
        comp.Setup(text, offset);
    }
    [ContextMenu("Update Textbubble slowly")]
    public void ChangeWriting()
    {
        Transform bubble = transform.Find("ChatBubble(Clone)");
        TextBubble comp = bubble.GetComponent<TextBubble>();
        comp.WriteText(text, time, offset);
    }
}
