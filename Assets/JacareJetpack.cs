using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacareJetpack : MonoBehaviour
{
	[SerializeField]
	float moveSpeed = 300f;

	[SerializeField]
	int dano = 2;

	Renderer sprite;
	bool slow = false;
	public bool flip = false;

	public GameObject wpAtual;

	public AudioSource barulhoDano;

	const int WAYPOINT_FINAL = 0;

	const int ONLY_ONE_WAY_WAYPOINT = 1;

	const int WEIGHT_EDGE_BEGGINING = 0;

	void Start()
	{
		//transform.position = waypoints [waypointIndex].transform.position;
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		Move();
	}

	void Move()
	{
		if (Vector3.Distance(transform.position, wpAtual.transform.position) >= 0.01)
		{//Andar at� o waypoint atual
			transform.position = Vector3.MoveTowards(transform.position, wpAtual.transform.position, moveSpeed * Time.deltaTime);
		}
		else
		{
			Waypoint ponto = wpAtual.GetComponent<Waypoint>();
			if (ponto.proximosWPs.Length == WAYPOINT_FINAL)
			{ //Eh o waypoint final
				barulhoDano.Play();
				Destroy(this.gameObject);
				Manager.vidas -= dano;
			}
			else
			{ //Escolhe o pr�ximo waypoint
				GameObject prox = null;

				if(ponto.proximosWPs.Length == ONLY_ONE_WAY_WAYPOINT)
				{ //Se tem so um waypoint para ir, nao precisa escolher
					prox = ponto.proximosWPs[0];
				} else
				{ //Escolhe o caminho com menos torres entre os waypoint
				
					
				}

				wpAtual = prox;
				if ((transform.position.x > wpAtual.transform.position.x && !flip) || (transform.position.x < wpAtual.transform.position.x && flip))
				{
					transform.Rotate(0f, 180f, 0f);
					flip = !flip;
				}
			}
		}
	}

	public void freeze()
	{
		if (!slow)
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
