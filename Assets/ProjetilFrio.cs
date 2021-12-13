using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilFrio : MonoBehaviour
{
    public int dano;
    public GameObject alvo;
    public float speed = 1000f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (alvo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.transform.position, speed * Time.deltaTime);
            /*Vector3 rotacao = Vector3.RotateTowards(transform.forward, alvo.transform.position - transform.position, speed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(rotacao);*/
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (Vector3.Distance(transform.position, alvo.transform.position) <= 1)
        {
            alvo.GetComponent<EnemyHealth>().takeDamage(dano);
            if (alvo.GetComponent<EnemyControl>() != null)
            {

                alvo.GetComponent<EnemyControl>().freeze();
            } else
            {
                alvo.GetComponent<JacareJetpack>().freeze();
            }
            Destroy(this.gameObject);
        }

    }
}
