using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIManager : MonoBehaviour
{
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    protected Animator animator;


    public bool SelectTarget()
    {


        return false;
    }

    public void GoToTarget(GameObject target)
    {
        agent.SetDestination(target.transform.position);
    }
    public bool GoToWall(GameObject target)
    {
        if (Wall.instance.currentWallHealth>0)
        {
            agent.SetDestination(target.transform.position);
            return true;

        }

        return false;

       

    }

    public bool CheckArrive(float range)
    {
        if (agent.hasPath && agent.remainingDistance < range)
        {

            animator.SetBool("isRun", false);

            agent.isStopped = true;

            return true;
        }

        else if (agent.hasPath && agent.remainingDistance > range)
        {
            agent.isStopped = false;

            animator.SetBool("isRun", true);
        }

        return false;



    }

    public void CheckArrive()
    {

        if (agent.hasPath && agent.remainingDistance < 0.5f)
        {
            animator.SetBool("isRun", false);

            agent.isStopped = true;

            agent.ResetPath();


        }

        else if (agent.hasPath && agent.remainingDistance > 0.25f)
        {
            agent.isStopped = false;

            animator.SetBool("isRun", true);
        }

    }
}
