using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionTrigger : MonoBehaviour {

	private BoxCollider2D playerColider;

	[SerializeField]
	private BoxCollider2D platformColider;

	[SerializeField]
	private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start () {
		playerColider = GameObject.Find ("Player").GetComponent<BoxCollider2D> ();
		Physics2D.IgnoreCollision(platformColider, platformColider, true);

	}
	
	void onTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			Physics2D.IgnoreCollision (platformColider, playerColider, true);
		}
	}

	void onTriggerExit2D( Collider2D other){
		if (other.gameObject.name == "Player") {
			Physics2D.IgnoreCollision (platformColider, playerColider, false);
		}
			
	}
}
