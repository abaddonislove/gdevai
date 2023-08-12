using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixUi : MonoBehaviour
{
    void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
