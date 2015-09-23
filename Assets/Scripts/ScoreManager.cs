using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private int score = 0;
	private int scoreIncrement = 1;
	Text txt;
	public static int increasedDifficulty = 0;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text> ();
		txt.text = "Score : " + score;
	}
	
	// Update is called once per frame
	void Update () {
		score = score + scoreIncrement;
		txt.text = "Score : " + score;
		if (PlayerMovement.activeBoost == true) {
			scoreIncrement = 2;
		} else {
			scoreIncrement = 1;
		}

		if (score >= 1000 * increasedDifficulty) {
			increasedDifficulty ++;
			ChunkManager.difficulty ++;
		}
	}
}
