using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	//protected Animator myAnimator;

	public Animator MyAnimator { get; private set;}

	[SerializeField]
	protected Transform ThrowPos;

	[SerializeField]
	private GameObject ThrowPrefab;

	[SerializeField]
	protected float speed = 10f;

	protected bool facingRight;

	protected bool Attack;


	[SerializeField]
	public int health;

	//public abstract bool IsDead{ get; }
	//public bool Attack {get;set;}

	// Use this for initialization
	public virtual void Start () {
		facingRight = true;
	    MyAnimator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeDirection(){

		facingRight = !facingRight;

		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.x);

	}

	//public virtual void ThrowObject(int value){
	//	if (facingRight) {
	//		//GameObject tmp = (GameObject)Instantiate(ThrowPrefab,ThrowPos.position,);
			//tmp.GetComponent<Object>().Initialize(Vector2.right);
	//	}else{
			//GameObject tmp = (GameObject)Instantiate(ThrowPrefab,ThrowPos.position,);
			//tmp.GetComponent<Object>().Initialize(Vector2.right);
	///	}

	//}

	//public abstract IEnumerator TakeDamage ();
}
