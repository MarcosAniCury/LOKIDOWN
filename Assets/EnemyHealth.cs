using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vida = 10;
    public int dinheiro = 25;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Destroy(this.gameObject);
            Manager.money += dinheiro;
            Manager.numDerrotados++;
        }
    }
}
