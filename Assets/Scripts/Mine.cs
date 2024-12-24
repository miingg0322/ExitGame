using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public bool isSafe = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSafe)
            return;

        if (other.GetComponent<Player>())
        {
            Debug.Log("BOOOM");
        }
    }
}
