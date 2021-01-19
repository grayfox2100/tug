using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	//public Transform target;
	public float smoothTime = 0.2f;
	private Vector3 _velocity = Vector3.zero;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug.Log();
		GameObject camTarget = GameObject.FindWithTag("Player");
		Vector3 targetPosition;
		//Debug.Log(camTarget);
		if (camTarget != null)
		{
			targetPosition = new Vector3(camTarget.transform.position.x, camTarget.transform.position.y, transform.position.z);
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
		}
		
	}
}
