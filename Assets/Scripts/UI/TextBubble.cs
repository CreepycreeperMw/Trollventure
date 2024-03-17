using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    private SpriteRenderer bg;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        bg = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public static void Create(GameObject parent, string text, Vector3 offset)
    {
        GameObject t = Instantiate(GameHandler.assets.textBubble, (parent.transform.position), parent.transform.rotation);
        t.transform.SetParent(parent.transform);
        TextBubble textBubble = t.GetComponent<TextBubble>();
        
        textBubble.Setup(text, offset);

    }

    public void Setup(string text, Vector2 offset) {
        Vector3 hardOffset = new Vector3(-1.2f, 0.1f);
        Vector3 _offset = (Vector3) offset;
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(1f, 1.3f);

        if (offset.x > 0)
        {
            bg.flipX = true;
        } else bg.flipX = false;
        transform.localPosition = _offset;

        bg.size = textSize + padding;

        bg.transform.localPosition = new Vector3(bg.size.x / 2f, 0f) + hardOffset;
    }

    public void SetText(string text)
    {
        textMeshPro.SetText(text);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
