using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelStae : IEnemyStates {

	private Enemy enemy;

	private float idleTimer;

	private float idleDuration = 3;

	public void Execute ()
	{
		Debug.Log ("ideling");
		Idle ();

		if (enemy.Target != null) {
			enemy.ChangeState (new PatroleState ());
		}
	}

	public void Exit ()
	{

	}
	public void Enter(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void OnTriggerEnter (Collider2D other)
	{
		
	}

	private void Idle()
	{
		enemy.MyAnimator.SetFloat ("speed", 0);
		idleTimer += Time.deltaTime;

		if (idleTimer >= idleDuration) {
			enemy.ChangeState (new PatroleState ());
		}
	}
		
}
