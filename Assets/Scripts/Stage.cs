using System;
using UnityEngine;

[Serializable]
public class Stage
{
    public string stageName;
    public bool safe;
    public DialogueData consoleDialogue;
    public DialogueData leftDialogue;
    public GameObject onObj;
    public GameObject offObj;

    public Stage(string stageName, bool safe)
    {
        this.stageName = stageName;
        this.safe = safe;
    }

    public void InitStageObj()
    {
        if(onObj != null)
        {
            onObj.SetActive(!onObj.activeSelf);
        }
        
        if(offObj != null)
        {
            offObj.SetActive(!offObj.activeSelf);
        }
    }
}
