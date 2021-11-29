using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFogo : MonoBehaviour
{
    public int dano = 5;
    public GameObject bala;
    public GameObject circulo;

    //float timer = 1.157f;
    float alcance = 215f;

    Animator animacao;

    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
        transform.Translate(2.5f, -25f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseEnter()
    {
        circulo.SetActive(true);
    }

    private void OnMouseExit()
    {
        circulo.SetActive(false);
    }

    public void Atirar()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        foreach(GameObject alvo in inimigos)
        {
            float distancia = Vector3.Distance(this.transform.position, alvo.transform.position);
            if (distancia <= alcance)
            {
                projetil tiro = Instantiate(bala, transform.position, Quaternion.identity).GetComponent<projetil>();
                tiro.dano = dano;
                tiro.alvo = alvo;
            }
        }
    }
}
