using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour {
	

	private float speed;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		speed = ChunkManager.difficulty;
		transform.Translate (Vector3.left * speed * Time.deltaTime);
		if (transform.position.x < -10) {
			Destroy(this.gameObject);
		}
	}
	

}