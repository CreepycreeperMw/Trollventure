using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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

public class DialougeEntry
{
    DialougeSentence[] dialougeSentences;
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
public class DialougeSentence
{
    public string text;
    public float writingFrequency = 0.01f;
    public float stayDuration = 2f;

    public DialougeSentence(string _text)
    {
        text = _text;
    }

    public DialougeSentence(string _text, float _stayDuration)
    {
        text = _text;
        stayDuration = _stayDuration;
    }

    public DialougeSentence(string _text, float _writingFrequency, float _stayDuration)
    {
        text = _text;
        writingFrequency = _writingFrequency;
        stayDuration = _stayDuration;
    }

    float writingTime
    {
        get { return writingFrequency * text.Length; }
        set { writingFrequency = value / text.Length; }
    }

    static DialougeSentence create(string text, float writingDuration)
    {
        return new DialougeSentence(text, writingDuration / text.Length);
    }
    static DialougeSentence create(string text, float writingDuration, float stayDuration)
    {
        return new DialougeSentence(text, writingDuration / text.Length, stayDuration);
    }
}