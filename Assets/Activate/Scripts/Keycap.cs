using System;
using UnityEngine;

/// <summary>
/// TODO
/// </summary>
public class Keycap : MonoBehaviour
{
    /// <summary>
    /// Represents the height the <see cref="KeycapCollisionValidator"/> will use as the top of the collider.
    /// </summary>
    public float ColliderValidationOffset;

    Transform ourTransform;
    Rigidbody ourRigidbody;
    bool cached;
    
    void Cache()
    {
        if (!cached)
        {
            //Cache components
            ourTransform = transform;
            ourRigidbody = GetComponent<Rigidbody>();

            cached = true;
        }
    }

    void Update()
    {
        Cache();
        
        //Ensures movement is restricted to vertical and that game object does not move above local y 0
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

    void OnDrawGizmosSelected()
    {
        Cache();
        
        Vector3 transformedOffset = ourTransform.position + ourTransform.TransformVector(new Vector3(0, ColliderValidationOffset, 0));

        //Draw collider validation offset marker
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transformedOffset + ourTransform.TransformVector(Vector3.forward),transformedOffset + ourTransform.TransformVector(Vector3.back));
        Gizmos.DrawLine(transformedOffset + ourTransform.TransformVector(Vector3.right),transformedOffset + ourTransform.TransformVector(Vector3.left));
    }
}