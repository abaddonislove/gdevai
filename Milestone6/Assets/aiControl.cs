using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiControl : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator animator;
    float speedMultiplier;

    float detectionRadius = 15;
    float magRadius = 10;

    void resetAgent()
    {
        speedMultiplier = Random.Range(.5f, 1f);
        agent.speed = 2 * speedMultiplier;
        agent.angularSpeed = 120;
        animator.SetFloat("speedMultiplier", speedMultiplier);
        animator.SetTrigger("isWalking");
        agent.ResetPath();
    }

    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        animator.SetFloat("wOffset", Random.Range(.1f, 1f));
        resetAgent();


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (agent.remainingDistance < 1)
        {
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    public void DetectNewMonster(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            Vector3 magDirection = (-location + this.transform.position).normalized;
            Vector3 newGoal = this.transform.position + magDirection * magRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);
            
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                animator.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    public void DetectNewMagnet(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius*2)
        {
            Vector3 newGoal = location;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);
            
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                animator.SetTrigger("isRunning");
                agent.speed = 3f;
                agent.angularSpeed = 400;
            }
        }
    }
}
