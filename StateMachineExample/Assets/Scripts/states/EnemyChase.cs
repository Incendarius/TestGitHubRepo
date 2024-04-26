using UnityEngine;
using System.Collections;
using AI.FiniteStateMachine;


public class EnemyChase : FSMState<EnemyController>
{
	private float _speed = 8f;
	public float _attackRange = 2f;

	public override void begin() {
		Debug.Log( "started chasing" );
	}
		
	public override void reason() {

		//Attack Logic
		if (IsPlayerWithinAttackRange())

		{
			// Transition to attack state
			_machine.changeState<EnemyAttack>();
		}

		else
		{

			// can we see the player? If not, we get out of here
			RaycastHit hit;
			var canSeePlayer = false;
			if (Physics.Raycast(_context.transform.position, _context.playerTransform.position - _context.transform.position, out hit))
			{
				if (hit.collider.name == "Player")
					canSeePlayer = true;
			}

			if (!canSeePlayer)
				_machine.changeState<EnemyPatrol>();
		}
	}
		
	public override void update( float deltaTime ) {
		// run after the player!
		var directionToPlayer = _context.playerTransform.position - _context.transform.position;
		_context.transform.rotation = Quaternion.LookRotation( directionToPlayer );
		_context.transform.position += ( directionToPlayer.normalized * _speed * deltaTime );
	}




	//Attack Logic
	private bool IsPlayerWithinAttackRange()
	{
		// Calculate the distance between the enemy and the player
		float distanceToPlayer = Vector3.Distance(_context.transform.position, _context.playerTransform.position);

		// Check if the player is within attack range
		return distanceToPlayer <= _attackRange;
	}
}
