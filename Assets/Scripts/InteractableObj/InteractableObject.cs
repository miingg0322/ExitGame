using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    internal string interactText = "대화하기";
    public DialogueData dialogueData;
    public bool interactable = true;
    public virtual void Interact(InteractableObject obj)
    {
        // 상속받아 상호작용 구현
    }

    internal virtual void Update()
    {
        
    }
}
