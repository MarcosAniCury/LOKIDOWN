using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject[] espacosTorres;
    public static int vidas = 20;
    public static int money = 100;
    public GameObject[] torres;
    public GameObject prefabJacare;
    public GameObject wpInicial;

    int inimigos;
    int numHorda = 1;
    float timer = 0f;
    public bool spawning = false;

    int torreSelecionada = -1;

    public Text numVidas;
    public Text dinheiro;
    public Text onda;
    public GameObject painel;
    public Button botaoComeçar;

    private void Start()
    {
        int c = 0;
        while(c < espacosTorres.Length)
        {
            EspacoTorre e = espacosTorres[c].GetComponent<EspacoTorre>();
            e.posicaoVetor = c;
            e.manager = this.gameObject;
            c++;
        }
    }

    void Update()
    {
        numVidas.text = "" + vidas;
        dinheiro.text = "" + money;
        onda.text = "Onda " + numHorda;

        if(spawning)
        {
            timer += Time.deltaTime;

            if(timer >= 1.5f)
            {
                EnemyControl jacare = Instantiate(prefabJacare, wpInicial.transform.position, Quaternion.identity).GetComponent<EnemyControl>();
                jacare.wpAtual = wpInicial;
                inimigos--;
                timer = 0f;

                if(inimigos <= 0)
                {
                    spawning = false;
                    numHorda++;
                    botaoComeçar.gameObject.SetActive(true);
                }
            }
        }

        if (vidas <= 0) { 
        
            //Ir pra tela d game over
        }
    }

    public void spaceSelected(int torre) {

        //Debug.Log("Clicou no espaco " + torre);

        torreSelecionada = torre;
        if (espacosTorres[torre].GetComponentInChildren<EspacoTorre>().built == false) {

            painel.SetActive(true);
        }
    }

    public void buildTower(string torreValor)
    {
        string[] sep = torreValor.Split(' ');

        int torre = int.Parse(sep[0]);
        int valor = int.Parse(sep[1]);

        if(espacosTorres[torreSelecionada].GetComponentInChildren<EspacoTorre>().built == false && money >= valor)
        {
            Instantiate(torres[torre], new Vector3(espacosTorres[torreSelecionada].transform.position.x - 1f, espacosTorres[torreSelecionada].transform.position.y + 36f), Quaternion.identity);
            money -= valor;
            espacosTorres[torreSelecionada].GetComponentInChildren<EspacoTorre>().built = true;
            foreach (GameObject wp in espacosTorres[torreSelecionada].GetComponentInChildren<EspacoTorre>().wps) {

                wp.GetComponent<Waypoint>().torres++;
                Debug.Log(wp.GetComponent<Waypoint>().torres);
            }
            painel.SetActive(false);
            espacosTorres[torreSelecionada].GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
    }

    public void setTamOnda()
    {
        inimigos = 5 + (5 * numHorda);
        spawning = true;
    }
}
