using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class patrol : NPCbase
{
    GameObject[] wayPoints;
    int currentWaypoint;

    private void Awake()
    {   
        wayPoints = GameObject.FindGameObjectsWithTag("waypoint").OrderBy(go => go.transform.GetSiblingIndex()).ToArray();
        foreach (GameObject point in wayPoints)
        {
            Debug.Log(point.name);  
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWaypoint = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (wayPoints.Length == 0) return;

        if (Vector3.Distance((wayPoints[currentWaypoint].transform.position), NPC.transform.position) < accuracy)
        {
            currentWaypoint++;
            if (currentWaypoint >= wayPoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        //rotate
        var direction = wayPoints[currentWaypoint].transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        NPC.transform.Translate(0, 0, Time.deltaTime * speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }


}
