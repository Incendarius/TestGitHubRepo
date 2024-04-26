using UnityEngine;
using System.Collections.Generic;
using AI.FiniteStateMachine;

public class EnemyController : MonoBehaviour {
	public Transform playerTransform;
	public List<Vector3> waypoints = new List<Vector3>();
	private FSM<EnemyController> _machine;

	void Start() {
		// fetch our waypoint positions so we have a purpose in life
		GameObject waypointRoot = GameObject.FindGameObjectWithTag( "Waypoints" );
		Transform[] rawWaypoints = waypointRoot.GetComponentsInChildren<Transform>();
		// filter out the root objects position
		foreach( Transform t in rawWaypoints ) {
			if( !t.Equals( waypointRoot.transform ) )
				waypoints.Add( t.position );
		}

		// the initial state has to be passed to the constructor
		_machine = new FSM<EnemyController>( this, new EnemyPatrol() );

		// we can now add any additional states
		_machine.addState(new EnemyChase());
		_machine.addState(new EnemyAttack());
	}

	void Update() {
		// update the state machine
		_machine.update( Time.deltaTime );
	}
}
