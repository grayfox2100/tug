using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public float smoothTime = 0.2f;
	private Vector3 _velocity = Vector3.zero;
	
	void Update()
	{
		GameObject camTarget = GameObject.FindWithTag("Player");
		Vector3 targetPosition;

		if (camTarget != null)
		{
			targetPosition = new Vector3(camTarget.transform.position.x, camTarget.transform.position.y, transform.position.z);
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
		}
		
	}
}
