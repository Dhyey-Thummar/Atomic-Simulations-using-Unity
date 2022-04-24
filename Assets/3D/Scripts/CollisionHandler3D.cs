using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler3D : MonoBehaviour
{
    // Flag so that coroutine would be launched only once
    private static bool coroutineActive = false;

    // Rigidbody component cache
    private Rigidbody rb;
    private EnergyConservation3D ecwd;

    public FixedJoint fj;

    // Record of last magnitude
    public float lastMagnitude = 0;
    public int electrons = 1;
    public int electronsToShare = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Caching components
        ecwd = EnergyConservation3D.instance;
        rb = GetComponent<Rigidbody>();
        ecwd.atoms.Add(rb);
        //atoms.Add(rb);

        // Launching coroutine if it isn't running yet
        if (!coroutineActive)
        {
            coroutineActive = true;
            StartCoroutine(CalculateTKEDaemon(1.0f));
        }
    }

    // Total Kinetic Energy Caclulation demon
    IEnumerator CalculateTKEDaemon(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("T.K.E. = " + CalculateTKE());
        }
    }

    // Method for Total Kinetic Energy calculation
    float CalculateTKE()
    {
        float tke = 0;
        foreach (var atom in ecwd.atoms)
        {
            tke += atom.velocity.magnitude;
        }
        return tke;
    }

    void OnCollisionEnter(Collision col)
    {
        // If we don't have bond
        if (fj == null &&
           // And velocity above the threshold
           col.relativeVelocity.magnitude > ecwd.bondMagnitudeThreshold)
        {
            // check if other side have a bond
            var ch = col.gameObject.GetComponent<CollisionHandler3D>();
            if (ch == null) return; // hit the wall
            if (ch.fj != null) return;
            
            // Creating bond to it
            fj = gameObject.AddComponent<FixedJoint>();
            fj.connectedBody = col.rigidbody;
            fj.breakForce = ecwd.bondStrength;

            // setting the bond to other side
            ch.fj = fj;
        }
    }
}
