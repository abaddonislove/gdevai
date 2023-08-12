using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Drive : MonoBehaviour {

    public int hp = 20;
    public TMP_Text hpDisplay;
    public GameObject bullet;
    public GameObject turret;
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;

    void Update() {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        hpDisplay.text = "HP: " + hp.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }

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
