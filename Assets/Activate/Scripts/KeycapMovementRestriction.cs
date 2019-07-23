using UnityEngine;

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
        else
        {
            ourTransform.localPosition = new Vector3(0, ourTransform.localPosition.y, 0);
        }
    }
}