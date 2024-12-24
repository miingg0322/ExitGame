using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontLookBack : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject monster;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rot = Mathf.Abs(player.transform.rotation.y);
        if(rot > 90)
        {

        }
    }

    void AfterLookBack()
    {
        player.isTalking = true;
    }
}

