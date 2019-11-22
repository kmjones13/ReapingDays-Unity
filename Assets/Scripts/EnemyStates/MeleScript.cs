using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleScript : IEnemyStates {

	private float attackTimer;
	private float attackCoolDown = 1;
	private bool canAttack= true;

	private Enemy enemy;

	public void Execute ()
	{
		Attack ();
		if (enemy.InThrowRange && !enemy.InMeleeRange) {
			enemy.ChangeState (new RangeState ());
		} else if (!enemy.Target == null) {
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

	private void Attack(){
		attackTimer += Time.deltaTime;

		if (attackTimer >= attackCoolDown) {
			canAttack= true;
			attackTimer = 0;
		}

		if (canAttack) {
			
			canAttack= false;
			enemy.MyAnimator.SetTrigger ("Attack");
			enemy.DealDamage ();

		}
	}


}
