using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Object/DialogueData", order = 0)]
public class DialogueData : ScriptableObject
{
    public List<Dialogue> dialogues = new List<Dialogue>();
}

[System.Serializable]
public class Dialogue
{
    public string name;
    public string content;
}