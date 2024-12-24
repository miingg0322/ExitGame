using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePath : MonoBehaviour
{
    public List<Transform> rows = new List<Transform>();
    private int rowCount = 0;
    int[] pathIndex;
    void Start()
    {
        rowCount = rows[0].childCount;
        //SetSafePath();
    }


    public void SetSafePath()
    {
        int randCount = rows.Count / 2;
        if (rows.Count % 2 == 1 && rows.Count > 4)
            randCount++;
        pathIndex = new int[randCount];
        for (int i = 0; i < randCount; i++)
        {
            pathIndex[i] = Random.Range(0, rowCount);
            Debug.Log(pathIndex[i]);
        }

        int idx = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            if (i % 2 == 0)
            {
                rows[i].GetChild(pathIndex[idx]).GetComponent<Mine>().isSafe = true;
            }
            else
            {
                int from = pathIndex[idx];
                int to = pathIndex[idx];
                if (idx != pathIndex.Length - 1)
                {
                    to = pathIndex[idx + 1];
                }

                if(from >= to)
                {
                    for (int j = from; j >= to; j--)
                    {
                        rows[i].GetChild(j).GetComponent<Mine>().isSafe = true;
                    }
                }
                else
                {
                    for (int j = from; j <= to; j++)
                    {
                        rows[i].GetChild(j).GetComponent<Mine>().isSafe = true;
                    }
                }
                idx++;
            }
        }

    }
}
