using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image=UnityEngine.UI.Image;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class Player : Character
{
	private static Player instance;

	public event DeadEventHandler DEAD;

	public static Player Instance{
		get{

			if (instance == null) {
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return instance;
		}

	}

	public float jumpForce;

	public float groundRadius;

	public Collider2D AttackTrigger;

	public Vector3 ReSpawn;

	public LayerMask whatIsGround;

	private Rigidbody2D rb2d;

	public bool airControl;

	public int kills = 0;

	private bool Attacking = false;
	private float AttackTimer;
	private float AttackCD = 0.2f;

	private bool SwordAttack1;
	private bool SwordAttack2;
	private bool Kick;

	private bool slide;

	[SerializeField]
	private Transform[] groundPoints;

	private bool isGrounded;

	private bool jump;

	private Vector2 startPos;

	public bool Dead = false;

	private float DeadCD = 2f;
	private float DeadTimer;

	private GameObject playerC;

	private Image HealthBar;

	void Awake(){
		AttackTrigger.enabled = false;
	}

	public override void Start()
	{
		base.Start ();
		facingRight = true;
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		playerC = GameObject.Find("Player");
		HealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
	
	}

	void Update()
	{
		if (kills == 20) {
			SceneManager.LoadScene ("Finsher", LoadSceneMode.Single);
		}
		HandleInput ();

		if (health <= 0) {
			Dead = true;
		} else {
			Dead = false;
		}

		if (Attacking) {
			if (AttackTimer > 0) {
				AttackTimer -= Time.deltaTime;
			} else {
				Attacking = false;
				AttackTrigger.enabled = false;
			}
		}

		if (health <= 0) {
			DeadTimer += Time.deltaTime;
			if (DeadTimer >= DeadCD) {
				transform.position = ReSpawn;
				MyAnimator.ResetTrigger ("Die");
				//MyAnimator.ResetTrigger ("Die");
				health = 100;
				Dead = false;
				MyAnimator.SetTrigger ("Respawn");
				MyAnimator.ResetTrigger ("Respawn");
				MyAnimator.SetTrigger ("Respawn");

			}
		}

		CheckIfDead ();

		HealthBarUpadte ();
	}

	void FixedUpdate()
	{

		float horizontal = Input.GetAxis("Horizontal");

		isGrounded = IsGrounded ();

		handleMovement (horizontal);

		flip (horizontal);

		HandleAttacks ();

		HandleLayer ();

		RestValues ();


	}

	private void handleMovement(float horizontal)
	{
		if (rb2d.velocity.y < 0) {
			MyAnimator.SetBool ("Land", true);
		}

		if (!MyAnimator.GetBool("slide") && !this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") && (isGrounded || airControl)) {
			rb2d.velocity = new Vector2 (horizontal * speed, rb2d.velocity.y);
		}

		if (isGrounded && jump) {
			                
			isGrounded = false;
			rb2d.AddForce (new Vector2 (0, jumpForce));
			MyAnimator.SetTrigger ("Jump");
		}

		if (slide && !this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide2")) {
			MyAnimator.SetBool("slide", true);
		} else if (!this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide2")) {
			MyAnimator.SetBool("slide", false);
		}

		MyAnimator.SetFloat ("speed", Mathf.Abs(horizontal));
	}

	private void HandleAttacks()
	{
		if (SwordAttack1 && !this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {

			MyAnimator.SetTrigger ("SwordAttack1");
			rb2d.velocity = Vector2.zero;

		}

		if (SwordAttack2 && !this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {

			MyAnimator.SetTrigger ("SwordAttack2");
			rb2d.velocity = Vector2.zero;

		}

		if (Kick && !this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
			MyAnimator.SetTrigger ("Kick");
			rb2d.velocity = Vector2.zero;

		}
	}

	private void HandleInput(){
		if(Input.GetKeyDown(KeyCode.Z)) {
			SwordAttack1 = true;
			Attacking = true;
			AttackTimer = AttackCD;
			AttackTrigger.enabled = true;

		}
		if(Input.GetKeyDown(KeyCode.X)) {
			SwordAttack2 = true;
			Attacking = true;
			AttackTimer = AttackCD;
			AttackTrigger.enabled = true;
		}

		if(Input.GetKeyDown(KeyCode.C)) {
			Kick = true;
			Attacking = true;
			AttackTimer = AttackCD;
			AttackTrigger.enabled = true;
		}

		if(Input.GetKeyDown(KeyCode.RightShift)) {
			slide = true;
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			jump = true;
		}
	}
			
	private void flip ( float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			ChangeDirection ();
		}
	}

	private void RestValues ()
	{
		SwordAttack1 = false;
		SwordAttack2 = false;
		Kick = false;

		slide = false;
		jump = false;
	}

	private bool IsGrounded(){
		if (rb2d.velocity.y <= 0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++){
					if ( colliders[i].gameObject != gameObject)
					{
						MyAnimator.ResetTrigger ("Jump");
						MyAnimator.SetBool ("Land", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	private void HandleLayer(){
		if (!isGrounded) {
			MyAnimator.SetLayerWeight (1, 1);
		} else {
			MyAnimator.SetLayerWeight (1, 0);
		}
	}

	public void CheckIfDead(){
		if (Dead == true) {
			Dead = false;
			MyAnimator.SetTrigger ("Die");
			DEAD ();

		}
	}

	public void HealthBarUpadte (){

		float num = health / 100.0f;
		HealthBar.fillAmount = num;
		Debug.Log (num);

	}

	//public override void ThrowObject(int value){
	// 
	//	if (!isGrounded && value == 1 || isGrounded && value == 0) {
	//		base.ThrowObject (value);
	//	}

	//}
}