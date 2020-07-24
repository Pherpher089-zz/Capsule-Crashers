using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item {

    bool rTriggerDown, lTriggerDown;
    int swingTool = Animator.StringToHash("SwingTool");
    int swingToolVert = Animator.StringToHash("SwingToolVert");
    public Animator animator;
    public bool isAttacking;

    private void Start()
    {
        matIndex = 0;
    }

    private void Update()
    {
        if(isEquiped)
        {
            isAttacking = animator.gameObject.GetComponent<AnimEventManager>().isAttacking;
        }
        else
        {
            isAttacking = false;
        }
    }

    public override void OnEquipt(GameObject character)
    {
        Debug.Log(character.gameObject.name);
        base.OnEquipt(character);
        animator = character.GetComponent<Animator>();
    }

    public override void OnUnequipt()
    {
        base.OnUnequipt();   
    }

    public override void PrimaryAction(float input)
    {
        if (!rTriggerDown && input > 0)
        {
            animator.SetTrigger(swingTool);
        }

        if (input > 0)
        {
            rTriggerDown = true;
        }
        else
        {
            rTriggerDown = false;
        }
    }
    public override void SecondaryAction(float input)
    {
        if (!lTriggerDown && input > 0)
        {
            animator.SetTrigger(swingToolVert);
        }

        if (input > 0)
        {
            lTriggerDown = true;
        }
        else
        {
            lTriggerDown = false;
        }
    }

}
