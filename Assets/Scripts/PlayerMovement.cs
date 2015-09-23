using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Transform GroundCheck;
	public float speed = 5f;
	public float jumpHeight = 650f;
	public float overlapRadius = 0.2f;
	public LayerMask whatIsGround;
	private bool isGrounded = false;
	public int collectedDoritos;
	private AudioSource source;
	private GameObject boost;
	public Sprite boost0, boost1, boost2, boost3, boost4, boost5;
	private bool timing;
	private bool invincibilityTimer;
	private float countdown = 5f;
	public AudioClip ripPlayer;
	public AudioClip pickup;
	private float invincibleCountdown = 1f;
	private bool leftKeyPressed = false;
	private bool rightKeyPressed = false;
	public GameObject explosion;
	public static bool activeBoost = false;
	public static bool shakerino = false;

	ParticleSystem particleSystem;
	//Animator anim;

	// Use this for initialization
	void Start () {
		ChunkManager.difficulty = 8f;
		StartInvincibilityTimer ();
		boost = GameObject.FindGameObjectWithTag ("Boost");
		//find the groundcheck gameobject
		GroundCheck = transform.Find ("GroundCheck");
		source = GetComponent<AudioSource> ();
		particleSystem = GetComponentInChildren<ParticleSystem> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		//if player collides with an obstacle AND invicibility timer is not running
		if (col.tag == "Obstacle" && invincibilityTimer == false) {
			source.clip = ripPlayer;
			source.Play();
			ChunkManager.difficulty = 0.5f;
			shakerino = true;
			Destroy(col.gameObject);
			//create explosion
			Instantiate(explosion,transform.position,transform.rotation);
			//wait .6 seconds with destroying player/switching to game over scene
			StartCoroutine(GameOver());
		}
		//if player collides with a dorito
		if (col.tag == "Pickup") {
			Destroy (col.gameObject);
			collectedDoritos++;
			source.clip = pickup;
			source.Play();
		}
	}

	IEnumerator GameOver()
	{
		yield return new WaitForSeconds (0.6f);
		Destroy (this.gameObject);
		Application.LoadLevel("GameOver");
	}

	void FixedUpdate(){
		//move player to the right.
		GetComponent<Rigidbody2D>().AddForce(transform.right * speed, ForceMode2D.Force);


		//check if player is grounded
		isGrounded = Physics2D.OverlapCircle (GroundCheck.position, overlapRadius, whatIsGround);
	}

	void StartInvincibilityTimer()
	{
		invincibilityTimer = true;
	}

	void StartBoostTimer()
	{
		timing = true;
	}

	// Update is called once per frame
	void Update () {
		//particles
		if (isGrounded == false) {
			particleSystem.enableEmission = false;
		} else {
			particleSystem.enableEmission = true;
		}
		//-------------------------------------------------------------------------------------------//
		//jump
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			GetComponent<Rigidbody2D> ().AddForce (transform.up * jumpHeight);
		}
		//-------------------------------------------------------------------------------------------//
		//rotation
		if (Input.GetKeyDown (KeyCode.LeftArrow) && !isGrounded) 
		{
			//transform.Rotate(0,0,10);
			leftKeyPressed = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow)) 
		{
			//transform.Rotate(0,0,10);
			leftKeyPressed = false;
		}

		if (leftKeyPressed == true) 
		{
			transform.Rotate(0,0,7);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) && !isGrounded) 
		{
			//transform.Rotate(0,0,10);
			rightKeyPressed = true;
		}
		
		if (Input.GetKeyUp(KeyCode.RightArrow)) 
		{
			//transform.Rotate(0,0,10);
			rightKeyPressed = false;
		}
		
		if (rightKeyPressed == true) 
		{
			transform.Rotate(0,0,-7);
		}
		//-------------------------------------------------------------------------------------------//
		//change boost sprite
		switch(collectedDoritos)
		{
		case 0:
			boost.gameObject.GetComponent<SpriteRenderer>().sprite = boost0;
			break;
		case 1:
			boost.gameObject.GetComponent<SpriteRenderer>().sprite = boost1;
			break;
		case 2:
			boost.gameObject.GetComponent<SpriteRenderer>().sprite = boost2;
			break;
		case 3:
			boost.gameObject.GetComponent<SpriteRenderer>().sprite = boost3;
			break;
		case 4:
			boost.gameObject.GetComponent<SpriteRenderer>().sprite = boost4;
			break;
		case 5:
			boost.gameObject.GetComponent<SpriteRenderer>().sprite = boost5;
			break;
		}
		//-------------------------------------------------------------------------------------------//
		//start boost if collected x doritos
		if (collectedDoritos >= 5) {
			//start timers
			StartBoostTimer();
			StartInvincibilityTimer();
		}
		//-------------------------------------------------------------------------------------------//
		//start the actual boost timer
		if (timing) 
		{
			//set level speed
			ChunkManager.difficulty = ScoreManager.increasedDifficulty + 30f;
			activeBoost = true;
			countdown -= Time.deltaTime;
			if(countdown <= 0)
			{
				ChunkManager.difficulty = ScoreManager.increasedDifficulty + 8f;
				timing = false;
				activeBoost = false;
				collectedDoritos = 0;
				countdown = 5f;
			}
		}
		//-------------------------------------------------------------------------------------------//
		//timer to ignore collision(while in boost and first second in the game)
		if (invincibilityTimer) 
		{
			invincibleCountdown -= Time.deltaTime;
			if(invincibleCountdown <=0)
			{
				invincibilityTimer = false;
				invincibleCountdown = 5.5f;
			}
		}
		//-------------------------------------------------------------------------------------------//
	}
}
