using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force3D : MonoBehaviour
{
    private Rigidbody rb3D;

    public float thrust = 10.0f;

    public bool randomDirection = true;
    public Vector3 direction = new Vector3(0, 0,0);

    void Start()
    {
        rb3D = GetComponent<Rigidbody>();
        //rb3D.AddForce(transform.up * thrust);
        if (randomDirection)
        {
            rb3D.AddForce(Random.insideUnitCircle.normalized * thrust);
        }
        else
        {
            rb3D.AddForce(direction.normalized * thrust);
        }
    }
}
