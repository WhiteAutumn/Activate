using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;

/// <summary>
/// A demonstration class of how haptic feedback could be implemented using <see cref="IPassiveKeyListener"/> and <see cref="IActiveKeyListener"/>
/// </summary>
public class HapticKeyFeedback : MonoBehaviour, IPassiveKeyListener, IActiveKeyListener
{
    List<GameObject> touchingObjects = new List<GameObject>();
    
    public void OnKeyDown(GameObject source, string signal)
    {
        if (source == transform.parent.parent.gameObject)
        {
            foreach (GameObject touchingObject in touchingObjects)
            {
                TrackedPoseDriver driver = touchingObject.transform.parent.parent.gameObject.GetComponent<TrackedPoseDriver>();
                if (driver != null)
                {
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
        }
    }

    public void OnKeyUpdate(GameObject source, string signal, float value)
    {
        //TODO: Implemented dynamic haptic feedback based on how much the key is being pressed down
    }

    void OnTriggerEnter(Collider other)
    {
        touchingObjects.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        touchingObjects.Remove(other.gameObject);
    }
}