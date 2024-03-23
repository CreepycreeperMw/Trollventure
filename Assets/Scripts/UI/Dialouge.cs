using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class Dialouge
{
    private DialougeEntry[] dialougeEntries;

    public Dialouge(params DialougeEntry[] entries)
    {
        this.dialougeEntries = entries;
    }

    public DialougeEntry[] entries {
        get { return dialougeEntries; }
    }
}

[System.Serializable]
public class DialougeEntry
{
    [SerializeField] private DialougeSentence[] dialougeSentences;
    public Vector2 offset = new Vector2(-3f, 1f);
    public bool keepGoingImmediatly = false;

    public DialougeEntry(params DialougeSentence[] sentences)
    {
        this.dialougeSentences = sentences;
    }
    public DialougeEntry(Vector2 localPos, params DialougeSentence[] sentences)
    {
        offset = localPos;
        this.dialougeSentences = sentences;
    }

    public DialougeEntry(bool keepOnGoing, params DialougeSentence[] sentences)
    {
        this.dialougeSentences = sentences;
        this.keepGoingImmediatly = keepOnGoing;
    }
    public DialougeEntry(Vector2 localPos, bool keepOnGoing, params DialougeSentence[] sentences)
    {
        this.offset = localPos;
        this.dialougeSentences = sentences;
        this.keepGoingImmediatly = keepOnGoing;
    }

    public DialougeSentence[] sentences
    {
        get { return dialougeSentences; }
    }

    public string getTotalText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        int l = dialougeSentences.Length;
        for (int i = 0; i < l; i++)
        {
            stringBuilder.Append(dialougeSentences[i].text);
        }

        return stringBuilder.ToString();
    }
}

[System.Serializable]
public class DialougeSentence
{
    public string text;
    public float writingDuration = 0.6f;
    public float stayDuration = 2f;

    public DialougeSentence(string _text)
    {
        text = _text;
        writingDuration = 0.01f * _text.Length;
    }

    public DialougeSentence(string _text, float _stayDuration)
    {
        text = _text;
        writingDuration = 0.01f * _text.Length;
        stayDuration = _stayDuration;
    }

    public DialougeSentence(string _text, float writingTime, float _stayDuration)
    {
        text = _text;
        writingDuration = writingTime;
        stayDuration = _stayDuration;
    }

    public float writingFrequency
    {
        get { return writingDuration / text.Length; }
        set { writingDuration = value * text.Length; }
    }

    static DialougeSentence CreateWithFrequency(string text, float writingFrequency)
    {
        return new DialougeSentence(text, writingFrequency * text.Length);
    }
    static DialougeSentence CreateWithFrequency(string text, float writingDuration, float stayDuration)
    {
        return new DialougeSentence(text, writingDuration * text.Length, stayDuration);
    }
}