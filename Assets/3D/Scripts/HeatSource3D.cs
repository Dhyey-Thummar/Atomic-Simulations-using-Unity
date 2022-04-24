using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSource3D : MonoBehaviour
{
  public float heat = 0.1f;

  private EnergyConservation3D ecwd;

  void Start()
  {
    ecwd = EnergyConservation3D.instance;
  }

  void OnTriggerStay(Collider col) {
    if(heat != 0) {
      float delta = heat / Mathf.Pow(Vector2.Distance(transform.position, col.transform.position),2) * Time.deltaTime;
      col.attachedRigidbody.velocity += col.attachedRigidbody.velocity.normalized * delta;
      ecwd.totalSystemEnergy += delta;
      //Debug.Log("HEAT+ " + col.gameObject.name);
    }
  }
}
