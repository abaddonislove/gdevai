using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiControl : MonoBehaviour
{
    [SerializeField]
    [Range(0, 2)]
    int type;

    NavMeshAgent agent;

    public GameObject target;

    public WASDMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<WASDMovement>();
    }

    void seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void flee(Vector3 location)
    {
        Vector3 fleeDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDirection);
    }

    void pursue(Vector3 location)
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);
        seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void evade(Vector3 location)
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);
        flee(target.transform.position + target.transform.forward * lookAhead);
    }

    Vector3 wanderTarget;

    void wander()
    {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter, 0, Random.Range(-1f, 1f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        seek(targetWorld);
    }

    void hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = world.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = world.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = world.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                distance = spotDistance;
            }
        }

        seek(chosenSpot);
    }

    void smartHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGameObject = world.Instance.GetHidingSpots()[0];  

        int hidingSpotsCount = world.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = world.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = world.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 20;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDir = hideDirection;
                chosenGameObject = world.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }
        float rayDistance = 50;
        Collider hideCollider = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        
        hideCollider.Raycast(back, out info, rayDistance);
        seek(info.point + chosenDir.normalized * 5f);
    }

    bool canSeeTarget()
    {
        RaycastHit raycastHitInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastHitInfo))
        {
            return raycastHitInfo.transform.gameObject.tag == "Player";
        }
        return false;
    }

    bool isClose()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) < 15f)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSeeTarget() == true && isClose() == true)
        {
            switch(type)
            {
                case 0:
                    pursue(target.transform.position);
                    break;
                case 1:
                    evade(target.transform.position);
                    break;
                case 2:
                    smartHide();
                    break;
            }
            
        }

        else { wander(); }
    }
}
