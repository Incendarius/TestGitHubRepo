using UnityEngine;
using System.Collections;
using AI.FiniteStateMachine;


public class EnemyAttack : FSMState<EnemyController>
{
	private float _attackRange = 2f;

	public override void begin() {
		Debug.Log( "Enemy prepare to attack" );
	}

	public override void reason() {
		// can we hit the player? If not, we get out of here

		if (!IsPlayerInRange())
		{
			_machine.changeState<EnemyChase>();
		}
	}
		
	public override void update( float deltaTime ) {
		//Check if the player is in rnage
		if (IsPlayerInRange())
		{
			// Attack the player
			AttackPlayer();
		}
	}

	private bool IsPlayerInRange()
	{
		float distanceToPlayer = Vector3.Distance(_context.transform.position, _context.playerTransform.position);
		return distanceToPlayer <= _attackRange;
	}

	private void AttackPlayer() 
	{
		Debug.Log("Enemy attacks Player!");
		//Implement Attack Logic
	}
}
