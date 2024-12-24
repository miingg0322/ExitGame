using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    PlayerInput input;
    CharacterController controller;
    public float moveSpeed;
    Vector2 moveVec;
    public bool isTalking = false;
    [Header("Interaction")]
    private Camera cam;
    [SerializeField] private float range = 5f;
    [SerializeField] private InteractableObject lookingObj = null;
    [SerializeField] private float lookObjRange = 3f;

    public static event Action<InteractableObject> OnInteractObjChanged;
    public static event Action<InteractableObject> OnPressInteract;
    public static event Action OnInteractNoDialogue;
    RaycastHit hit;

    void Awake()
    {
        input = new PlayerInput();
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Update()
    {
        PlayerMove();
        Interact();

        if (isTalking)
            return;

        InteractableObject obj = null;
        Vector3 origin = transform.position + controller.center;
        if (Physics.Raycast(origin, transform.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Door"))
            {
                obj = hit.collider.transform.parent.GetComponent<InteractableObject>();
            }
            else
            {
                obj = hit.collider.GetComponent<InteractableObject>();
            }
        }
        if(obj == null && lookingObj !=null)
        {
            float dist = Vector3.Distance(transform.position, lookingObj.transform.position);
            //Debug.Log(dist);
            if (dist < lookObjRange)
                return;
        }
        if(lookingObj != obj)
        {
            lookingObj = obj;
            OnInteractObjChanged?.Invoke(obj);
        }
    }
    public void Interact()
    {
        bool value = input.PlayerAction.Interact.triggered;
        if (value)
        {
            if(isTalking)
                OnPressInteract?.Invoke(lookingObj);
            if (lookingObj != null)
            {
                OnPressInteract?.Invoke(lookingObj);
            }
        }
    }
    private void PlayerMove()
    {
        if (isTalking)
            return;

        moveVec = input.PlayerAction.Move.ReadValue<Vector2>();
        //Debug.Log(moveVec);
        Vector3 dir = new Vector3(moveVec.x, 0, moveVec.y);
        dir = transform.TransformDirection(dir);
        //Debug.Log(dir);
        controller.Move(dir * moveSpeed * Time.deltaTime);
        if (!controller.isGrounded)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, 0, pos.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        MovingNPC obj = other.GetComponent<MovingNPC>();
        if (obj)
        {
            if (obj.isPatrol)
            {
                //Debug.Log("HOLD");
                obj.Hold(transform.position);
            }
        }
    }

}
