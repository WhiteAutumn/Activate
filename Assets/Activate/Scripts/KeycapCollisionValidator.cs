using UnityEngine;

public class KeycapCollisionValidator : MonoBehaviour
{
    Transform ourTransform;
    SphereCollider ourCollider;
    Vector3 previousPos;
    Vector3 colliderOffset;
    readonly Vector3[] colliderEdgeOffsets = new Vector3[6];
    float colliderRadius;
    float minimumVelocity;
    
    void Start()
    {
        ourTransform = transform;
        previousPos = ourTransform.position;
        ourCollider = GetComponent<SphereCollider>();
        
        colliderOffset = ourCollider.center;
        float radius = ourCollider.radius;

        Transform currentTransform = ourTransform;
        while (true)
        {
            Vector3 localScale = currentTransform.localScale;
            radius *= Mathf.Max(localScale.x, localScale.y, localScale.z);

            if (currentTransform.parent == null) 
                break;
            
            currentTransform = currentTransform.parent;
        }
        
        colliderEdgeOffsets[0] =  new Vector3(radius, 0, 0);
        colliderEdgeOffsets[1] =  new Vector3(-radius, 0, 0);
        colliderEdgeOffsets[2] =  new Vector3(0, radius, 0);
        colliderEdgeOffsets[3] =  new Vector3(0, -radius, 0);
        colliderEdgeOffsets[4] =  new Vector3(0, 0, radius);
        colliderEdgeOffsets[5] =  new Vector3(0, 0, -radius);
        
        colliderRadius = radius;
        minimumVelocity = radius / 2;
    }
    
    void Update()
    {
        float currentVelocity = Vector3.Distance(ourTransform.position, previousPos);
        
        if (currentVelocity > minimumVelocity)
        {
            Vector3 center = ourTransform.position + colliderOffset;
            Vector3 previousCenter = previousPos + colliderOffset;

            foreach (Vector3 edgeOffset in colliderEdgeOffsets)
            {
                Vector3 edge = center + edgeOffset;
                Vector3 previousEdge = previousCenter + edgeOffset;
                
                bool hitKeycap = false;
                RaycastHit[] hits = Physics.RaycastAll(previousEdge, edge - previousEdge, Vector3.Distance(edge, previousEdge));
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.CompareTag("keycap"))
                    {
                        KeycapCollider keycapCollider = hit.collider.gameObject.GetComponent<KeycapCollider>();
                        Transform keyTransform = keycapCollider.transform;

                        keyTransform.position =
                            ourTransform.position +
                            keyTransform.TransformDirection(new Vector3(0, -colliderRadius)) +
                            keyTransform.TransformVector(new Vector3(0, -keycapCollider.Offset, 0));
                        keyTransform.localPosition = new Vector3(0, keyTransform.localPosition.y, 0);

                        hitKeycap = true;
                        break;
                    }
                }

                if (hitKeycap)
                    break;
            }
        }

        previousPos = transform.position;
    }
}