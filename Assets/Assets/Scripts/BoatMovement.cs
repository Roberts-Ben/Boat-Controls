using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class BoatMovement : MonoBehaviour
{
    public Rigidbody rb;
    public XRKnob _XRKnob;
    public XRSlider _XRSlider;

    public float throttle;
    public float wheelAngle;

    private readonly float airResistance = 0.5f;
    private readonly float turnResistance = 20.8f; 

    public float acceleration;
    public float topSpeed;

    public float angularAcceleration;
    public float angularTopSpeed;

    public float velocity;
    public float angularVelocity;

    void FixedUpdate()
    {
        // Input from throttle
        throttle = _XRSlider.GetPosition();
        velocity += (acceleration * throttle) * Time.fixedDeltaTime;

        if(velocity > 0)
        {
            velocity -= (acceleration * airResistance) * Time.fixedDeltaTime;
        }
        if (velocity < 0)
        {
            velocity += (acceleration * airResistance) * Time.fixedDeltaTime;
        }

        if (velocity > topSpeed)
        {
            velocity = topSpeed;
        }
        if (velocity < -topSpeed)
        {
            velocity = -topSpeed;
        }

        // Wheel rotation
        wheelAngle = _XRKnob.GetAngle();
        angularVelocity += angularAcceleration * (wheelAngle * throttle / 10) * Time.fixedDeltaTime;

        if (angularVelocity > 0)
        {
            angularVelocity -= (angularAcceleration * turnResistance) * Time.fixedDeltaTime;
        }
        if (angularVelocity < 0)
        {
            angularVelocity += (angularAcceleration * turnResistance) * Time.fixedDeltaTime;
        }

        if (angularVelocity > angularTopSpeed)
        {
            angularVelocity = angularTopSpeed;
        }
        if (angularVelocity < -angularTopSpeed)
        {
            angularVelocity = -angularTopSpeed;
        }

        rb.AddForce(transform.forward * velocity, ForceMode.Force);
        rb.AddTorque(transform.up * angularVelocity, ForceMode.Force);
    }
}
