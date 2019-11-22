using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	private IEnemyStates currentState;

	public GameObject Target { get; set; }

	[SerializeField] private float meleeRange;

	[SerializeField]private float throwRange;

	[SerializeField]public GameObject CharacterPlayer;

	private bool Dead = false;

	private float DeadCD = 2f;
	private float DeadTimer;
	public bool InMeleeRange{
		get{

			if (Target != null) {
				return Vector2.Distance (transform.position, Target.transform.position) <= meleeRange;

			}
			return false;
		}
	}

	public bool InThrowRange{
		get{

			if (Target != null) {
				return Vector2.Distance (transform.position, Target.transform.position) <= throwRange;

			}
			return false;
		}
	}

	//public override bool IsDead{
	//	get{
	//		return health <= 0;
	//	}
	//}


	// Use this for initialization
	public override void Start () {
		base.Start ();
		Player.Instance.DEAD += new DeadEventHandler (RemoveTarget);
		ChangeState (new IdelStae ());
	
	}
	
	// Update is called once per frame
	void Update () {
		currentState.Execute ();

		LookAtTarget ();

		IsDead ();

		if (Dead == true) {
			DeadTimer += Time.deltaTime;
			if (DeadTimer >= DeadCD) {
				Player CharacterScript = CharacterPlayer.GetComponent<Player> ();
				CharacterScript.kills += 1;
				Destroy (gameObject);
			}
		}

	}

	private void LookAtTarget(){
		if (Target != null && Dead != true) {
			float xDir = Target.transform.position.x - transform.position.x;

			if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
				ChangeDirection ();
			}
		}
	}

	public void RemoveTarget(){
		Target = null;
		ChangeState (new PatroleState ());
	}

	public void ChangeState( IEnemyStates newState){
		if (currentState != null) {
			currentState.Exit ();
		}

		currentState = newState;

		currentState.Enter (this);
	}

	public void Move()
	{
		if(!Attack && Dead != true){
			MyAnimator.SetFloat ("speed", 1);

			transform.Translate (GetDirection () *( speed * Time.deltaTime));
		}
	}

	public void DealDamage(){
		Player CharacterScript = CharacterPlayer.GetComponent<Player> ();
		CharacterScript.health -= 10;

	}

	public Vector2 GetDirection(){


		return facingRight ? Vector2.right : Vector2.left;
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		currentState.OnTriggerEnter (other);
	}

	public void Damage (int dmg){
		health -= dmg;
		MyAnimator.SetTrigger ("Damage");
	}

	public void IsDead(){
		if (health <= 0) {
			MyAnimator.SetTrigger ("Die");
			Dead = true;
		}
	}
		
}
