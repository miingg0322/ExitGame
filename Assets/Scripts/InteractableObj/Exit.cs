using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Door
{
    public bool isPass;
    void Start()
    {
        if (isPass)
            interactText = "���� ���� �̵�";
        else
            interactText = "���Ǽ� �̵�";
    }

    public override void Interact(InteractableObject obj)
    {
        if (obj != this)
            return;

        GameManager.Instance.StageClear(isPass);
    }
}
