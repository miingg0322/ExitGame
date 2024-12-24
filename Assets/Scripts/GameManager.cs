using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if(instance == null)
            {
                instance = value;
            }
            else
            {
                Destroy(value);
            }
        }
    }
    public static event Action StageCompleted;
    public bool paused = false;
    public int stages;
    public int curStage = 0;
    public int cleared = 0;

    [SerializeField] private Exit nextExit;
    [SerializeField] private Exit backExit;
    public MinePath minepath;

    public Stage tutorial;
    public List<Stage> stageList = new List<Stage>();
    void Start()
    {
        stages = stageList.Count;
    }

    void Update()
    {
    }

    public void PassFailed()
    {
        curStage = 0;
        cleared = 0;
    }

    public void StageClear(bool isPass)
    {
        if (isPass && stageList[curStage].safe)
        {
            NextStage();
            return;
        }

        if(!isPass && !stageList[curStage].safe)
        {
            NextStage();
            return;
        }

        PassFailed();
    }

    public void NextStage()
    {
        cleared++;
    }
}

