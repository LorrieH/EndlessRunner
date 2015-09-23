using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public string sceneName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel (sceneName);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (sceneName);
		}
	}

	void OnMouseDown()
	{
		Application.LoadLevel (sceneName);
	}

}
