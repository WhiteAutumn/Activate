using UnityEngine;

public class KeycapCollider : MonoBehaviour
{
	public float Offset;

	void OnDrawGizmosSelected()
	{
		Transform ourTransform = transform;
		Vector3 transformedOffset = ourTransform.position + ourTransform.TransformVector(new Vector3(0, Offset, 0));

		Gizmos.color = Color.green;
		Gizmos.DrawLine(transformedOffset + ourTransform.TransformVector(Vector3.forward),transformedOffset + ourTransform.TransformVector(Vector3.back));
		Gizmos.DrawLine(transformedOffset + ourTransform.TransformVector(Vector3.right),transformedOffset + ourTransform.TransformVector(Vector3.left));
	}
}