using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class PlayerLookDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }


    private bool Look(StateController controller)
    {

        Debug.Log("Look Decision");
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

        if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange)
            && hit.collider.CompareTag("Player"))
        {
            controller.actorTarget = hit.collider.gameObject;
            controller.chaseTarget = hit.transform.position;
            controller.actorTarget = hit.collider.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }
}