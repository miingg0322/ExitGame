using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState
{
    IDLE, WALK, DEAD
}
public class MovingNPC : InteractableObject
{   
    [Header("Patrol Check")]
    [SerializeField] internal bool isPatrol = false;

    internal Animator anim;
    public AnimState state;

    internal Vector3 firstPos;
    public float patDist = 5f;
    [SerializeField]internal Vector3 patrolPos;
    [SerializeField]internal Vector3 targetPos;
    public float holdTime = 5f;
    public float moveSpeed = 1f;
    public bool isBusy = false;
    public bool holding = false;
    public float holdRange = 5f;
    internal CapsuleCollider ccollider;
      void Awake()
    {
        anim = GetComponent<Animator>();
        ccollider = GetComponent<CapsuleCollider>();
        firstPos = transform.position;
        if (isPatrol)
        {
            patrolPos = firstPos + transform.forward * patDist;
            targetPos = patrolPos;
            ccollider.isTrigger = true;
        }
        Player.OnPressInteract += HoldOnInteract;
    }

     internal override void Update()
    {
        if (isBusy)
            return;

        anim.SetInteger("State", (int)state);
        switch (state)
        {
            case AnimState.IDLE:
                StartCoroutine(HoldPosCo(holdTime));
                break;
            case AnimState.WALK:
                StartCoroutine(PatrolCo());
                break;
            case AnimState.DEAD:
                anim.SetBool("Dead", true);
                break;
            default:
                break;
        }
    }

    internal virtual IEnumerator PatrolCo()
    {
        transform.LookAt(targetPos);
        isBusy = true;

        Vector3 moveVec;
        while (transform.position != targetPos)
        {
            moveVec = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            transform.position = moveVec;
            if (holding)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        isBusy = false;
        state = AnimState.IDLE;
        if (!holding)
        {
            if (targetPos == firstPos)
            {
                targetPos = patrolPos;
            }
            else
            {
                targetPos = firstPos;
            }
        }
    }

    internal virtual IEnumerator HoldPosCo(float holdTime)
    {
        isBusy = true;
        
        if (holding)
        {
            float dist = 0;
            Transform player = Camera.main.transform.parent;
            while(dist < holdRange)
            {
                dist = Vector3.Distance(transform.position, player.position);
                yield return new WaitForEndOfFrame();
            }
            holding = false;
            ccollider.isTrigger = true;
        }
        else
        {
            float timer = 0;
            while (timer < holdTime)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }         
        }
        if(isPatrol)
            state = AnimState.WALK;
        isBusy = false;
    }

    public virtual void Hold(Vector3 target)
    {
        if (!holding)
        {
            holding = true;
            transform.LookAt(target);
            ccollider.isTrigger = false;
        }
    }

    private void HoldOnInteract(InteractableObject obj)
    {
        if(obj == this)
        {
            if (!obj.interactable)
                return;
            holding = true;
            Transform player = Camera.main.transform.parent;
            transform.LookAt(player);
        }
    }
}
