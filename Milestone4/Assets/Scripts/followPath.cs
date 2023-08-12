using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPath : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1f;
    float rotspeed = 2f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWaypointIndex = 0;
    Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;
        currentNode = wps[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            return;
        }

        currentNode = graph.getPathPoint(currentWaypointIndex);

        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position, this.transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotspeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }

    }

    public void GoToMount()
    {
        graph.AStar(currentNode, wps[0]);
        currentWaypointIndex = 0;
    }

    public void GoToRacks()
    {
        graph.AStar(currentNode, wps[3]);
        currentWaypointIndex = 0;
    }
    public void GoToCC()
    {
        graph.AStar(currentNode, wps[16]);
        currentWaypointIndex = 0;
    }
    public void GoToPumps()
    {
        graph.AStar(currentNode, wps[8]);
        currentWaypointIndex = 0;
    }
    public void GoToTanker()
    {
        graph.AStar(currentNode, wps[10]);
        currentWaypointIndex = 0;
    }
    public void GoToRadar()
    {
        graph.AStar(currentNode, wps[17]);
        currentWaypointIndex = 0;
    }
    public void GoToPost()
    {
        graph.AStar(currentNode, wps[4]);
        currentWaypointIndex = 0;
    }
    public void GoToCentre()
    {
        graph.AStar(currentNode, wps[6]);
        currentWaypointIndex = 0;
    }
}
