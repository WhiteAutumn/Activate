using UnityEngine;

/// <summary>
/// Class <c>KeycapCollisionValidator</c> ensures that fast moving controllers will not clip through keycaps.
/// This component should be attached the VR controller game object or a child of the controller game object.
/// This component requires a <see cref="SphereCollider"/>.
/// <seealso cref="Keycap"/>
/// </summary>
public class KeycapCollisionValidator : MonoBehaviour
{
    Transform ourTransform;
    SphereCollider ourCollider;
    Vector3 previousPos;
    Vector3 colliderOffset;
    readonly Vector3[] colliderEdgeOffsets = new Vector3[6];
    float colliderRadius;
    float minimumVelocity;
    
    void Awake()
    {
        //Cache components
        ourTransform = transform;
        ourCollider = GetComponent<SphereCollider>();
        
        previousPos = ourTransform.position; 
        colliderOffset = ourCollider.center;
        
        //Calculate true radius of sphere collider
        float trueRadius = ourCollider.radius;
        Transform currentTransform = ourTransform;
        while (true)
        {
            Vector3 localScale = currentTransform.localScale;
            trueRadius *= Mathf.Max(localScale.x, localScale.y, localScale.z);

            if (currentTransform.parent == null) 
                break;
            
            currentTransform = currentTransform.parent;
        }
        
        
        colliderEdgeOffsets[0] =  new Vector3(trueRadius, 0, 0);
        colliderEdgeOffsets[1] =  new Vector3(-trueRadius, 0, 0);
        colliderEdgeOffsets[2] =  new Vector3(0, trueRadius, 0);
        colliderEdgeOffsets[3] =  new Vector3(0, -trueRadius, 0);
        colliderEdgeOffsets[4] =  new Vector3(0, 0, trueRadius);
        colliderEdgeOffsets[5] =  new Vector3(0, 0, -trueRadius);
        
        colliderRadius = trueRadius;
        minimumVelocity = trueRadius / 2;
    }
    
    void Update()
    {
        float currentVelocity = Vector3.Distance(ourTransform.position, previousPos);
        
        //If object is moving fast enough to possibly clip through objects
        if (currentVelocity > minimumVelocity)
        {
            Vector3 center = ourTransform.position + colliderOffset;
            Vector3 previousCenter = previousPos + colliderOffset;

            //For each edge, draw a ray between previous edge pos and current edge pos
            foreach (var edgeOffset in colliderEdgeOffsets)
            {
                Vector3 edge = center + edgeOffset;
                Vector3 previousEdge = previousCenter + edgeOffset;
                
                bool hitKeycap = false;
                RaycastHit[] hits = Physics.RaycastAll(previousEdge, edge - previousEdge, Vector3.Distance(edge, previousEdge));
                foreach (var hit in hits)
                {
                    //Find keycap component
                    Keycap keycap = hit.collider.GetComponent<Keycap>();
                    
                    if (keycap == null)
                        continue;
                    
                    Transform keycapTransform = keycap.transform;

                    //Update keycap position
                    keycapTransform.position =
                        ourTransform.position +
                        keycapTransform.TransformDirection(new Vector3(0, -colliderRadius)) +
                        keycapTransform.TransformVector(new Vector3(0, -keycap.ColliderValidationOffset, 0));
                    keycapTransform.localPosition = new Vector3(0, keycapTransform.localPosition.y, 0);

                    hitKeycap = true;
                    break;
                }

                if (hitKeycap)
                    break;
            }
        }

        previousPos = transform.position;
    }
}