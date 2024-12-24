using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMan : MovingNPC
{
    [SerializeField] GameObject lod;
    Animator lodAnim;
    public bool isUsing = false;
    void Start()
    {
        lodAnim = lod.GetComponent<Animator>();
    }

    internal override void Update()
    {
        base.Update();

        lodAnim.SetInteger("State", (int)state);
        if (state.Equals(AnimState.DEAD))
        {
            anim.SetBool("Dead", true);
            lodAnim.SetBool("Dead", true);
            return;
        }
        else
        {
            anim.SetInteger("State", (int)state);
        }
        if(isUsing)
        {
            anim.SetBool("Use", true);
            lodAnim.SetBool("Use", true);
        }
    }
}
