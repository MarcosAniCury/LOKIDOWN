using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject prefabJacare;
    public GameObject[] pontosSpawn;
    EnemyControl controle;

    // Start is called before the first frame update
    void Start()
    {
        controle = GetComponent<EnemyControl>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        foreach (GameObject ponto in pontosSpawn)
        {
            EnemyControl jacare = Instantiate(prefabJacare, ponto.transform.position, transform.rotation).GetComponent<EnemyControl>();
            jacare.wpAtual = controle.wpAtual;
            jacare.flip = controle.flip;
            jacare.caminhos = controle.caminhos;
            jacare.wp = controle.wp;
            jacare.indexMelhorCaminho = controle.indexMelhorCaminho;
            jacare.barulhoDano = controle.barulhoDano;
        }
    }
}
