using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torre2 : MonoBehaviour
{
    public int dano = 2;
    public GameObject bala;
    public GameObject alvo = null;
    public GameObject circulo;

    float timer = 0f;
    float alcance = 215f;
    AudioSource somTiro;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(2.5f, -25f, 0f);
        somTiro = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.75f)
        {
            alvo = EscolheAlvo();
            if (alvo != null)
            {
                somTiro.Play();
                StartCoroutine(atirar());
                timer = 0f;
            }
        }
    }

    GameObject EscolheAlvo()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        foreach (GameObject inimigo in inimigos)
        {
            float distancia = Vector3.Distance(this.transform.position, inimigo.transform.position);
            if (distancia <= alcance)
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

    IEnumerator atirar()
    {
        int c = 0;
        while(c < 3)
        {
            ProjetilFrio tiro = Instantiate(bala, transform.position, Quaternion.identity).GetComponent<ProjetilFrio>();
            tiro.dano = dano;
            tiro.alvo = alvo;
            yield return new WaitForSeconds(0.1f);
            c++;
        }
    }
}
