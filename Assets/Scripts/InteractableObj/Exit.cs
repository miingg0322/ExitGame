using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Door
{
    public bool isPass;
    void Start()
    {
        if (isPass)
            interactText = "다음 구역 이동";
        else
            interactText = "대피소 이동";
    }

    public override void Interact(InteractableObject obj)
    {
        if (obj != this)
            return;

        GameManager.Instance.StageClear(isPass);
    }
}
