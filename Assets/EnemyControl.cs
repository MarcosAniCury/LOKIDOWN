using System;
using UnityEngine;


public class EnemyControl : MonoBehaviour {

	[SerializeField]
	Transform[] waypoints;

	[SerializeField]
	Transform[] decisionWaypoints;

	[SerializeField]
	int[] decisionOption1;

	
	[SerializeField]
	int[] decisionOption2;

	
	[SerializeField]
	int[] decisionOption3;

	[SerializeField]
	float moveSpeed = 2f;

	int waypointIndex = 0;

	void Start () {
		transform.position = waypoints [waypointIndex].transform.position;
	}

	void Update () {
		Move ();
	}

	void Move()
	{

		foreach (Transform waypoint in decisionWaypoints) {
			if (waypoint.position == transform.position) {
				waypointIndex = choosePath();
			}
		}

		transform.position = Vector3.MoveTowards (transform.position,
												waypoints[waypointIndex].transform.position,
												moveSpeed * Time.deltaTime);

		if (transform.position == waypoints [waypointIndex].transform.position) {
			waypointIndex += 1;
		}

		if (waypointIndex == waypoints.Length) {
			Destroy(this.gameObject);
			Manager.vidas--;
		}
	}

	int choosePath() 
	{
		int decisionWaypointsIndex = 0;

		for (int i = 0;i < decisionWaypoints.Length;i++){
			if (transform.position == decisionWaypoints[i].position) {
				decisionWaypointsIndex = i;
				break;
			}
		}

		int[] paths = { 
						decisionOption1[decisionWaypointsIndex],
						decisionOption2[decisionWaypointsIndex],
						decisionOption3[decisionWaypointsIndex] 
					};

		int numberRandom = 0;
		int numberChoose = 0;
		while (numberChoose == 0) {
			numberRandom = DateTime.Now.Millisecond % 3;
			numberChoose = paths[numberRandom];
		}
		return numberChoose;
	}

	public void setSpeed(string calculo) {

		this.moveSpeed = (calculo == "*") ? this.moveSpeed * 2 : this.moveSpeed / 2;
	}

}
