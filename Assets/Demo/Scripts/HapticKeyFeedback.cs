using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;

/// <summary>
/// Class <c>HapticKeyFeedback</c> demonstrates how haptic feedback could be implemented using <see cref="IPassiveKeyListener"/>.
/// </summary>
public class HapticKeyFeedback : MonoBehaviour, IPassiveKeyListener
{
    GameObject ourKey;
    readonly List<GameObject> closeObjects = new List<GameObject>();

    void Awake()
    {
        //Cache variables
        ourKey = transform.parent.parent.gameObject;
    }

    public void OnKeyDown(GameObject source, string signal)
    {
        //Check if key event came from this key
        if (source != ourKey)
            return;
        
        foreach (var closeObject in closeObjects)
        {
            //Attempt to get TrackedPoseDriver
            Transform parent = closeObject.transform.parent;
            if (parent == null)
                continue;
            
            parent = parent.parent;
            if (parent == null)
                continue;

            TrackedPoseDriver driver = parent.gameObject.GetComponent<TrackedPoseDriver>();
            if (driver == null)
                continue;
            
            
            //Send haptic feedback
            if (driver.poseSource == TrackedPoseDriver.TrackedPose.RightPose)
            {
                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(0, 0.5f, 1);
            }
            else if (driver.poseSource == TrackedPoseDriver.TrackedPose.LeftPose)
            {
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(0, 0.5f, 1);
            }
        }
    }
    
    //Maintain a list of objects in range of key
    void OnTriggerEnter(Collider other)
    {
        closeObjects.Add(other.gameObject);
    }
    void OnTriggerExit(Collider other)
    {
        closeObjects.Remove(other.gameObject);
    }
}