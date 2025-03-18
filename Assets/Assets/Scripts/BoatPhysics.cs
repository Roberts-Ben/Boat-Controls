using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class BoatPhysics : MonoBehaviour
{
    public Rigidbody rb;
    public WaterSurface waterSurface;

    WaterSearchParameters waterSearchParameters = new();
    WaterSearchResult waterSearchResult = new();

    public float submergeDepth;
    public float displacmentAmount;
    public float waterDrag;
    public float angularWaterDrag;

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity, transform.position, ForceMode.Acceleration);

        waterSearchParameters.startPosition = transform.position;
        waterSurface.FindWaterSurfaceHeight(waterSearchParameters, out waterSearchResult);
        
        if(transform.position.y < waterSearchResult.height)
        {
            float displacementMultiplier = Mathf.Clamp01(waterSearchResult.height - transform.position.y / submergeDepth) * displacmentAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * Time.fixedDeltaTime * waterDrag * -rb.velocity, ForceMode.VelocityChange);
            rb.AddTorque(angularWaterDrag * displacementMultiplier * Time.fixedDeltaTime * -rb.angularVelocity, ForceMode.VelocityChange);
        }
    }
}
