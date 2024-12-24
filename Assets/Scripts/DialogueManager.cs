using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            return instance;
        }
        set
        {

        }
    }
    public GameObject dialoguePanel;
    public TMP_Text dname;
    public TMP_Text dialogue;
    public bool next = false;
    private Player player;
    [SerializeField] private float typingTime = 0.05f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = Camera.main.transform.parent.GetComponent<Player>();
        Player.OnPressInteract += OnDialogue;
    }

    void Update()
    {

    }

    public void OnDialogue(InteractableObject obj)
    {
        if (dialoguePanel.activeSelf)
        {
            next = true;
        }
        else
        {
            if(obj.dialogueData != null)
            {
                StartCoroutine(DialogueCo(obj.dialogueData));
            }
        }
    }

    IEnumerator DialogueCo(DialogueData data)
    {
        player.isTalking = true;
        dialoguePanel.SetActive(true);
        List<Dialogue> dialogues = data.dialogues;
        for (int i = 0; i < dialogues.Count; i++)
        {
            dname.text = dialogues[i].name;
            dialogue.text = "";
            int j = 0;
            while (j < dialogues[i].content.Length)
            {
                dialogue.text = dialogues[i].content.Substring(0, j);
                j++;
                if (next)
                {
                    dialogue.text = dialogues[i].content;
                    next = false;
                    break;
                }
                yield return new WaitForSeconds(typingTime);
            }
            yield return new WaitUntil(() => next);
            next = false;
        }
        dialoguePanel.SetActive(false);
        player.isTalking = false;
    }

}
