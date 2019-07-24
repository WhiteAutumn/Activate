using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;

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

	public void OnKeyUpdate(GameObject source, string signal, float value)
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		touchingObjects.Add(other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		touchingObjects.Remove(other.gameObject);
	}

	/*
	public GameObject Controller;
	InputDevice device;
	GameObject touchedObject;

	void OnValidate()
	{
		TrackedPoseDriver driver = Controller.GetComponent<TrackedPoseDriver>();
		if (driver.poseSource == TrackedPoseDriver.TrackedPose.RightPose)
		{
			device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
		}
		else if (driver.poseSource == TrackedPoseDriver.TrackedPose.LeftPose)
		{
			device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
		}
	}

	public void OnKeyDown(GameObject source, string signal)
	{
		Debug.Log("key down received!");
		
		if (touchedObject.transform.parent.gameObject == source)
		{
			device.SendHapticImpulse(0, 0.5f, 1);
		}
	}

	public void OnKeyUpdate(GameObject source, string signal, float value)
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("object touched! (" + other.gameObject.name + ")");
		touchedObject = other.gameObject;
	}
	*/
}