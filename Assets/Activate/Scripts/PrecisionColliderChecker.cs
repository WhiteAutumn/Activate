using UnityEngine;

public class PrecisionColliderChecker : MonoBehaviour
{
    public float VelocityThreshold = 0.1f;
    Vector3 previousPos;
    Transform ourTransform;

    void Start()
    {
        ourTransform = transform;
        previousPos = ourTransform.position;
    }
    
    void Update()
    {
        float deltaPos = Vector3.Distance(ourTransform.position, previousPos);
        
        if (deltaPos > VelocityThreshold)
        {
            RaycastHit hit;
            if (Physics.Raycast(previousPos, transform.position - previousPos, out hit, deltaPos))
            {
                Debug.Log("HELLO");
            }
        }

        previousPos = transform.position;
    }
}
