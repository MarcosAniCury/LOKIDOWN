using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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

	Renderer sprite;
	bool slow = false;
	bool flip = false;

	void Start () {
		transform.position = waypoints [waypointIndex].transform.position;
		sprite = GetComponent<SpriteRenderer>();
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
		if((transform.position.x > waypoints[waypointIndex].transform.position.x && !flip) || (transform.position.x < waypoints[waypointIndex].transform.position.x && flip))
		{
			transform.Rotate(0f, 180f, 0f);
			flip = !flip;
		}

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

	public void freeze()
	{
		if(!slow)
		{
			StartCoroutine(lentidao());
		}
	}

	IEnumerator lentidao()
	{
		moveSpeed /= 2;
		sprite.material.SetColor("_Color", new Color(0.5f, 0.5f, 1f));
		slow = true;
		yield return new WaitForSeconds(3f);
		moveSpeed *= 2;
		sprite.material.SetColor("_Color", new Color(1f, 1f, 1f));
		slow = false;
	}

}
