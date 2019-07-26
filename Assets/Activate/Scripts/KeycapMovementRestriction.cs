using System;
using UnityEngine;

/// <summary>
/// Ensures that the keycap only moves vertically and does not go above local y 0
/// </summary>
public class KeycapMovementRestriction : MonoBehaviour
{
    Transform ourTransform;
    Rigidbody ourRigidbody;
    
    void Start()
    {
        ourTransform = gameObject.transform;
        ourRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (ourTransform.localPosition.y > 0)
        {
            ourRigidbody.velocity = Vector3.zero;
            ourTransform.localPosition = Vector3.zero;
        }
        else if (Math.Abs(ourTransform.localPosition.x) > 0.01f || Math.Abs(ourTransform.localPosition.z) > 0.01f)
        {
            ourTransform.localPosition = new Vector3(0, ourTransform.localPosition.y, 0);
        }
    }
}