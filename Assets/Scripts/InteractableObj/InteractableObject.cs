using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    internal string interactText = "��ȭ�ϱ�";
    public DialogueData dialogueData;
    public bool interactable = true;
    public virtual void Interact(InteractableObject obj)
    {
        // ��ӹ޾� ��ȣ�ۿ� ����
    }

    internal virtual void Update()
    {
        
    }
}
