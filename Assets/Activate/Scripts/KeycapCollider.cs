using UnityEngine;

/// <summary>
/// Class <c>KeycapCollider</c> is required for <see cref="KeycapCollisionValidator"/> to function properly.
/// This component should be attached to the keycap game object. The keycap should ideally have a <see cref="BoxCollider"/>.
/// </summary>
public class KeycapCollider : MonoBehaviour
{
    /// <summary>
    /// Adjust this in the editor so that the marker is at the same y level as the top of the collider.
    /// </summary>
    public float Offset;

    void OnDrawGizmosSelected()
    {
        Transform ourTransform = transform;
        Vector3 transformedOffset = ourTransform.position + ourTransform.TransformVector(new Vector3(0, Offset, 0));

        //Draw offset marker
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transformedOffset + ourTransform.TransformVector(Vector3.forward),transformedOffset + ourTransform.TransformVector(Vector3.back));
        Gizmos.DrawLine(transformedOffset + ourTransform.TransformVector(Vector3.right),transformedOffset + ourTransform.TransformVector(Vector3.left));
    }
}