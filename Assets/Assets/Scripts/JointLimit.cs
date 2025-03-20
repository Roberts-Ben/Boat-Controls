using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class JointLimit : MonoBehaviour
{
    public float startZPos;
    public float minLimit;
    public float maxLimit;

    private void Start()
    {
        startZPos = transform.localPosition.z;
    }

    void LateUpdate()
    {
        if(transform.localPosition.z > startZPos + maxLimit)
        {
            transform.localPosition = new(transform.localPosition.x, transform.localPosition.y, startZPos + maxLimit);
        }
        if (transform.localPosition.z < startZPos - minLimit)
        {
            transform.localPosition = new(transform.localPosition.x, transform.localPosition.y, startZPos - minLimit);
        }
    }
}
