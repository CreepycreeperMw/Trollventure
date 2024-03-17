using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    private SpriteRenderer bg;
    private TextMeshPro textMeshPro;
    [SerializeField] private static GameObject template;
    [SerializeField] private GameObject _template;

    private void Awake()
    {
        bg = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        template = _template;
    }

    public static void Create(GameObject parent, string text, Vector3 offset)
    {
        GameObject t = Instantiate(template, (parent.transform.position + offset), parent.transform.rotation);
        t.transform.SetParent(parent.transform);
        t.GetComponent<TextBubble>().Setup(text, offset);
    }

    public void Setup(string text, Vector2 offset) {
        Vector3 _offset = (Vector3) offset;
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(2f, 2f);

        bg.size = textSize + padding;

        if(offset.x > 0)
        {
            bg.flipX = true;
        }

        bg.transform.localPosition = new Vector3(bg.size.x / 2f, 0f) + _offset;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
