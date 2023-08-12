using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentManager : MonoBehaviour
{
    GameObject[] agents;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("AI");    
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ai in agents)
        {
            ai.GetComponent<AIControl>().agent.SetDestination(player.transform.position);
        }
        
        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.Log("going");
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                foreach (GameObject ai in agents)
                {
                    ai.GetComponent<AIControl>().agent.SetDestination(hit.point);
                }
            }
        }*/
    }
}
