using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    private SpriteRenderer bg;
    private TextMeshPro textMeshPro;
    private string targetText = string.Empty;
    private int charI = 0;
    public bool isWriting = false;
    // Im done with this shit of dealing with delta time and shit. Dividing and
    // multiplying by 1000 doesnt get you between seconds to milliseconds which
    // it should so really fuck this. You' re now suppose to smell wether this
    // is seconds or milliseconds cuz I dont even know anymore, bruh. Gl hf lmao
    public float frequency = 1.0f;
    private float tempTime = 0.0f;
    private DateTime nextUpdate = DateTime.MinValue;
    private string writtenText;
    private int minLines = 0;
    private bool skipped = false;

    private void Awake()
    {
        bg = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if(!isWriting)
        {
            /*if(Input.GetKeyDown(KeyCode.Return) || (nextUpdate != DateTime.MinValue && (nextUpdate.Millisecond - DateTime.Now.Millisecond <= 0)))
            {
                nextUpdate = DateTime.MinValue;
                if(targetText != textMeshPro.text)
                {
                    isWriting = true;
                }
            }*/
            return;
        }
        if (targetText == string.Empty || charI >= targetText.Length)
        {
            targetText = string.Empty;
            tempTime = 0f;
            isWriting = false;
            Debug.Log("DONE WRITING OUT TEXT");
            return;
        }
        else Debug.Log(isWriting); Debug.Log(skipped);

        if(Input.GetKeyDown(KeyCode.Return))
        {
            skipped = true;
            isWriting = false;
            tempTime = 0f;
            charI = targetText.Length;
            SetTextWithMinLines(targetText, minLines);
            isWriting = false;
            return;
        }

        tempTime += Time.deltaTime;
        if(tempTime >= frequency)
        {
            tempTime -= frequency;
            writtenText += targetText[charI];

            SetTextWithMinLines(writtenText, minLines);
            charI++;
        }
    }

    public static TextBubble Create(GameObject parent, string text, Vector3 offset)
    {
        GameObject t = Instantiate(GameHandler.assets.textBubble, (parent.transform.position), parent.transform.rotation);
        t.transform.SetParent(parent.transform);
        TextBubble textBubble = t.GetComponent<TextBubble>();
        
        textBubble.Setup(text, offset);
        return textBubble;
    }

    public void Setup(string text, Vector2 offset) {
        Vector3 hardOffset = new Vector3(-1.2f, 0.1f);
        Vector3 _offset = (Vector3) offset;
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        writtenText = text;

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
        writtenText = text;
        textMeshPro.SetText(text);
    }
    public void SetTextWithMinLines(string text, int minLines)
    {
        writtenText = text;
        
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        if (textMeshPro.textInfo.lineCount < minLines) textMeshPro.SetText(text + new String('\n', minLines - textMeshPro.textInfo.lineCount));
        
        Debug.Log(textMeshPro.textInfo.lineCount);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void WriteText(string text, float time, Vector2 offset)
    {
        Setup(text, offset);
        minLines = textMeshPro.textInfo.lineCount;
        writtenText = "";
        textMeshPro.SetText("");
        targetText = text;
        frequency = time / text.Length;
        charI = 0;
        isWriting = true;
    }
    public void AddText(string text, float timeMs, Vector2 offset)
    {
        string _writtenText = writtenText;
        charI = textMeshPro.text.Length;
        targetText = writtenText + text;
        // Debug.Log(text); Debug.Log(targetText); Debug.Log(writtenText); Debug.Log(_writtenText);

        Setup(targetText, offset);
        // Debug.Log(text); Debug.Log(targetText); Debug.Log(writtenText); Debug.Log(_writtenText);
        textMeshPro.ForceMeshUpdate();
        minLines = textMeshPro.textInfo.lineCount;

        SetText(_writtenText);
        // Debug.Log(text); Debug.Log(targetText); Debug.Log(writtenText); Debug.Log(_writtenText);

        frequency = timeMs/text.Length;
        isWriting = true;
    }

    public IEnumerator WriteDialouge(Dialouge dialouge)
    {
        foreach (var page in dialouge.entries)
        {
            charI = 0;
            Setup(page.getTotalText(), page.offset);
            minLines = textMeshPro.textInfo.lineCount;

            SetText("");

            DialougeSentence[] sentences = page.sentences;
            int l = sentences.Length;
            for (int i = 0; i < l; i++)
            {
                var part = sentences[i];
                targetText = writtenText + part.text;
                frequency = part.writingFrequency;
                isWriting = true;
                skipped = false;
                yield return new WaitUntil(() => isWriting == false);

                if (skipped) { SetText(page.getTotalText()); break; }
                else if(l-1 != i) yield return new WaitForSeconds(part.stayDuration);
            }

            if(!page.keepGoingImmediatly) {
                /// TODO : Notify user that he needs to press enter to keep going
                if(skipped) yield return new WaitUntil(()=>Input.GetKeyUp(KeyCode.Return));
                yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.Return));
            }
        }
    }
    public IEnumerator WriteDialougeWithOffset(Dialouge dialouge, Vector2 offset)
    {
        foreach (var page in dialouge.entries)
        {
            charI = 0;
            Setup(page.getTotalText(), offset);
            minLines = textMeshPro.textInfo.lineCount;

            SetText("");

            DialougeSentence[] sentences = page.sentences;
            int l = sentences.Length;
            for (int i = 0; i < l; i++)
            {
                var part = sentences[i];
                targetText = writtenText + part.text.Replace("\\n","\n");
                frequency = part.writingFrequency;
                isWriting = true;
                skipped = false;
                yield return new WaitUntil(() => isWriting == false);

                if (skipped) { SetText(page.getTotalText()); break; }
                else if(page.keepGoingImmediatly || l-1 != i) yield return new WaitForSeconds(part.stayDuration);
            }

            if(!page.keepGoingImmediatly) {
                /// TODO : Notify user that he needs to press enter to keep going
                if(skipped) yield return new WaitUntil(()=>Input.GetKeyUp(KeyCode.Return));
                yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.Return));
            }
        }
    }

    public string chatText
    {
        get
        {
            return textMeshPro.text;
        }

        set { textMeshPro.SetText(value); }
    }
}
