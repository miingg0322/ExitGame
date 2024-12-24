using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    private Transform left;
    private Transform right;
    private Transform originLeft;
    private Transform originRight;
    public float width = 1.75f;
    public float doorSpeed = 1f;
    public bool isOpend = false;
    public bool locked = false;
    private BoxCollider leftCollider;
    private BoxCollider rightCollider;
    void Start()
    {
        left = transform.GetChild(0);
        leftCollider = left.GetComponent<BoxCollider>();
        originLeft = left;
        right = transform.GetChild(1);
        rightCollider = right.GetComponent<BoxCollider>();
        originRight = right;
        interactText = "열기";
        Player.OnPressInteract += Interact;
    }

    public override void Interact(InteractableObject obj)
    {
        if (obj != this)
            return;

        if (!isOpend)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }

    }
    public void OpenDoor()
    {
        StartCoroutine(OpenDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        float leftX = left.localPosition.x - width;
        float rightX = right.localPosition.x + width;
        SetDoorCollider();
        while (left.localPosition.x > leftX && right.localPosition.x < rightX)
        {
            left.Translate(Vector3.left * doorSpeed * Time.deltaTime, Space.Self);
            right.Translate(Vector3.right * doorSpeed * Time.deltaTime, Space.Self);
            yield return new WaitForFixedUpdate();
        }
        isOpend = true;
        SetDoorCollider();
        interactText = "닫기";
    }

    public void CloseDoor()
    {
        StartCoroutine(CloseDoorCoroutine());
    }

    private IEnumerator CloseDoorCoroutine()
    {
        float leftX = left.localPosition.x + width;
        float rightX = right.localPosition.x - width;
        SetDoorCollider();
        while (left.localPosition.x < leftX && right.localPosition.x > rightX)
        {
            left.Translate(Vector3.right * doorSpeed * Time.deltaTime, Space.Self);
            right.Translate(Vector3.left * doorSpeed * Time.deltaTime, Space.Self);
            yield return new WaitForFixedUpdate();
        }
        SetDoorCollider();
        isOpend = false;
        interactText = "열기";
    }

    private void SetDoorCollider()
    {
        leftCollider.enabled = !leftCollider.enabled;
        rightCollider.enabled = !rightCollider.enabled;
    }
    public void InitDoor()
    {
        interactText = "열기";
        left.position = originLeft.position;
        right.position = originRight.position;
        locked = false;
    }
}
