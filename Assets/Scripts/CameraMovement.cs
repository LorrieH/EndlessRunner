using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	Transform player;
	public float horizontalOffset = 6f;
	private float cameraY = 4f;
	
	// Use this for initialization
	void Start () {
		Screen.SetResolution (800, 480, false);
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		var x = player.position.x;
		
		transform.position = new Vector3 (x + horizontalOffset, cameraY, -1);
	}
}
