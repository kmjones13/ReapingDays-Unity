using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeState : IEnemyStates {
	private Enemy enemy; 

	private float throwTimer;
	private float throwCoolDown = 3;
	private bool canThrow = true;

	public void Execute ()
	{
		//ThrowObject ();
		if (enemy.InMeleeRange) {
			enemy.ChangeState (new MeleScript ());
		}

		else if (enemy.Target != null) {
			enemy.Move ();
		} else {
			enemy.ChangeState (new IdelStae ());
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

	private void ThrowObject(){
		throwTimer += Time.deltaTime;

		if (throwTimer >= throwCoolDown) {
			canThrow = true;
			throwTimer = 0;
		}

		if (canThrow) {
			canThrow = false;
			enemy.MyAnimator.SetTrigger ("throw");
		}
	}
}
