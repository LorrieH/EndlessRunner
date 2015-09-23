using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour {

	static bool AudioBegin = false;
	private AudioSource source;
	public AudioClip music;

	void Awake(){
		source = GetComponent<AudioSource> ();
		source.clip = music;
		if (AudioBegin == false) {
			source.Play();
			DontDestroyOnLoad(this.gameObject);
			AudioBegin = true;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "TestingGrounds" || Application.loadedLevelName == "TheoVersion")
		{
		source.Stop ();
		AudioBegin = false;
		}
	}

}