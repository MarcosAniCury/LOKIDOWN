using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torre1 : MonoBehaviour
{
    public int dano = 1;
    public GameObject bala;
    public GameObject alvo = null;
    public GameObject circulo;

    float timer = 0f;
    float alcance = 300f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            alvo = EscolheAlvo();
            if(alvo != null)
            {
                projetil tiro = Instantiate(bala, transform.position, Quaternion.identity).GetComponent<projetil>();
                tiro.dano = dano;
                tiro.alvo = alvo;
                timer = 0f;
            }
        }
    }

    GameObject EscolheAlvo()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        foreach(GameObject inimigo in inimigos)
        {
            float distancia = Vector3.Distance(this.transform.position, inimigo.transform.position);
            if(distancia <= alcance)
            {
                return inimigo;
            }
        }

        return null;
    }

    private void OnMouseEnter()
    {
        circulo.SetActive(true);
    }

    private void OnMouseExit()
    {
        circulo.SetActive(false);
    }
}
