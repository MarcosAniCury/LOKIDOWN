using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vida = 1;
    public int dinheiro = 25;
    AudioSource barulhoMorte;

    // Start is called before the first frame update
    void Start()
    {
        barulhoMorte = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int dano)
    {
        vida -= dano;
        if(vida <= 0)
        {
            barulhoMorte.Play();
            Destroy(this.gameObject);
            Manager.money += dinheiro;
            Manager.numDerrotados++;
        }
    }
}
