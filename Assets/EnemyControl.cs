using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyControl : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 2f;

	[SerializeField]
	int dano = 1;

	Renderer sprite;
	bool slow = false;
	public bool flip = false;

	public GameObject wpAtual;

	public AudioSource barulhoDano;

	//Only to chooseBestWay
	List<int> allEdge;

	List<Waypoint> waypointPrevius;

	const int WAYPOINT_FINAL = 0;

	const int ONLY_ONE_WAY_WAYPOINT = 1;

	const int WEIGHT_EDGE_BEGGINING = 0;

	void Start () {
		//transform.position = waypoints [waypointIndex].transform.position;
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update () {
		Move ();
	}

	void Move()
	{
		if(Vector3.Distance(transform.position, wpAtual.transform.position) >= 0.01)
		{//Andar at� o waypoint atual
			transform.position = Vector3.MoveTowards(transform.position, wpAtual.transform.position, moveSpeed * Time.deltaTime);
		} else
		{
			Waypoint wp = wpAtual.GetComponent<Waypoint>();
			if(wp.proximosWPs.Length == WAYPOINT_FINAL)
			{ //Eh o waypoint final
				barulhoDano.Play();
				Destroy(this.gameObject);
				Manager.vidas -= dano;
			} else
			{ //Escolhe o pr�ximo waypoint
				GameObject prox = null;

				if(wp.proximosWPs.Length == ONLY_ONE_WAY_WAYPOINT)
				{ //Se tem s� um waypoint para ir, n�o precisa escolher
					prox = wp.proximosWPs[0];
				} else
				{ //Escolhe o caminho com menos torres entre os waypoint
				
					prox = wp.proximosWPs[chooseBestWay(wp)];
				}

				wpAtual = prox;
				if((transform.position.x > wpAtual.transform.position.x && !flip) || (transform.position.x < wpAtual.transform.position.x && flip))
				{
					transform.Rotate(0f, 180f, 0f);
					flip = !flip;
				}
			}
		}
	}

	int chooseBestWay(Waypoint wp) 
	{
		allEdge = new List<int>();
		waypointPrevius = new List<Waypoint>();
		chooseWay(wp, WEIGHT_EDGE_BEGGINING);
		int smallWeight = allEdge[0];
		int wayChoose = 0;
		for(int i = 1;i < allEdge.Capacity;i++) {
			if (allEdge[i] < smallWeight) {
				smallWeight = allEdge[i];
				for (int j = 1; j <= allEdge.Capacity/wp.proximosWPs.Length;j++) {
					if (i <= j * allEdge.Capacity/wp.proximosWPs.Length) {
						wayChoose = j-1;
					}
				}
			}
		}
		return wayChoose;
	}

	void chooseWay(Waypoint wp, int weightEdge)
	{
		int weightEdgeFromNow = weightEdge;
		while (wp.proximosWPs.Length != 0) {
			if (wp.proximosWPs.Length == 1) {
				weightEdgeFromNow += wp.torres;
				wp = wp.proximosWPs[0].GetComponent<Waypoint>();
			} else {
				weightEdgeFromNow += wp.torres;
				waypointPrevius.Add(wp);
				foreach (UnityEngine.GameObject waypointNext in wp.proximosWPs) {
					Waypoint wpNext = waypointNext.GetComponent<Waypoint>();
					if (!waypointPrevius.Contains(wpNext)) {
						chooseWay(wpNext, weightEdgeFromNow);
					}
				}
				waypointPrevius = new List<Waypoint>();
				return;
			}
		}
		if (wp.proximosWPs.Length == 0) {
			allEdge.Add(weightEdgeFromNow);
		}
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
