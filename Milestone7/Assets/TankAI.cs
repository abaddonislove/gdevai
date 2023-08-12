using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TankAI : MonoBehaviour
{
    public int hp = 20;

    Animator anim;
    public GameObject player;
    public TMP_Text hpDisplay;
    public GameObject bullet;
    public GameObject turret;

    public GameObject getPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        anim.SetInteger("hp", hp);
        hpDisplay.text = "HP: " + hp.ToString();

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }

    public void stopFiring()
    {
        CancelInvoke("fire");
    }

    public void startFiring()
    {
        InvokeRepeating("fire", .5f, .5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "bullet")
        {
            hp -= 1;
        }
    }
}
