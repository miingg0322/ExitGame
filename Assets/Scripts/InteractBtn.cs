using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractBtn : MonoBehaviour
{
    Button btn;
    TMP_Text btnText;
    void Awake()
    {
        btn = GetComponent<Button>();
        btnText = GetComponentInChildren<TMP_Text>();
        Player.OnInteractObjChanged += OnInteractObjChanged;
        gameObject.SetActive(false);
    }

    public void OnInteractObjChanged(InteractableObject obj)
    {
        if (obj)
        {
            if (!gameObject.activeSelf)
            {
                if (!obj.interactable)
                    return;
                gameObject.SetActive(true);
                btnText.text = obj.interactText;
            }
        }
        else
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
