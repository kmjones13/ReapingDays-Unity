using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStates {

	void Execute ();
	void Exit ();
	void Enter(Enemy enemy);
	void OnTriggerEnter (Collider2D other);
}
