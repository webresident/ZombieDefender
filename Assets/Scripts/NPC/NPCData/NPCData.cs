using System;
using UnityEngine;

[CreateAssetMenu(fileName ="NPC", menuName ="Questions")]
public class NPCData : ScriptableObject
{
    public Talks[] conversation;
    public Quest[] questions;
}

[Serializable]
public class Quest
{
    public string title;
    public string description;
    public int requiredLevel;
    public bool isCompleted;
}

[Serializable]
public class Talks
{
    public string question;
    public string answer;
    public int requiredLevel;
    public bool isTalked;

    public int timeForWait;
}
