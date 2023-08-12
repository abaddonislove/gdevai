using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magSpawn : MonoBehaviour
{
    public GameObject mag;
    public GameObject mons;
    GameObject[] agents;
    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(mons, hit.point, mons.transform.rotation);
                foreach (GameObject a in agents)
                {
                    a.GetComponent<aiControl>().DetectNewMonster(hit.point);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(mag, hit.point, mag.transform.rotation);
                foreach (GameObject a in agents)
                {
                    a.GetComponent<aiControl>().DetectNewMagnet(hit.point);
                }
            }
        }
    }
}
