using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject[] espacosTorres;
    public static int vidas = 100;
    public static int money = 550;
    public static int numDerrotados = 0;
    public GameObject[] torres;
    public GameObject prefabJacare;
    public GameObject prefabJacare2;
    public GameObject prefabTank;
    public GameObject wpInicial;

    int inimigos;
    int numHorda = 1;
    float timer = 0f;
    public bool spawning = false;
    public bool auto = false;
    float timeSpawn = 1.5f;

    int lifeLastTurn = 100;

    int torreSelecionada = -1;

    public Text numVidas;
    public Text dinheiro;
    public Text onda;
    public GameObject painel;
    public Button botaoComecar;

    private void Start()
    {
        vidas = 100;
        lifeLastTurn = vidas;
        money = 550;
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

        if(spawning || auto)
        {
            timer += Time.deltaTime;

            if(timer >= timeSpawn && inimigos > 0)
            {
                int inimigoGerado = UnityEngine.Random.Range(1, 101);

                if (inimigoGerado < 80-numHorda || inimigos < 3) {
                    EnemyControl jacare = Instantiate(prefabJacare, wpInicial.transform.position, Quaternion.identity).GetComponent<EnemyControl>();
                    jacare.wpAtual = wpInicial;
                    inimigos--;
                } else if (inimigoGerado < 95-numHorda || inimigos < 5) {
                    EnemyControl jacare2 = Instantiate(prefabJacare2, wpInicial.transform.position, Quaternion.identity).GetComponent<EnemyControl>();
                    jacare2.wpAtual = wpInicial;
                    inimigos -= 3;
                } else {
                    EnemyControl tank = Instantiate(prefabTank, wpInicial.transform.position, Quaternion.identity).GetComponent<EnemyControl>();
                    tank.wpAtual = wpInicial;
                    inimigos -= 5;
                }
                timer = 0f;
            }

            if (inimigos <= 0 && GameObject.FindGameObjectsWithTag("Inimigo").Length == 0)
            {
                spawning = false;
                numHorda++;
                
                if (auto)
                {
                    setTamOnda();
                }
                else
                {
                    botaoComecar.gameObject.SetActive(true);
                }

                if (lifeLastTurn == vidas) {
                    vidas += 5;
                    vidas = vidas > 100 ? 100 : vidas;
                }

                lifeLastTurn = vidas;

                money += 150;
            }
        }

        if (vidas <= 0) {

            /*StreamWriter arquivo = new StreamWriter("GameOver.txt");
            int ondasComp = numHorda - 1;
            arquivo.WriteLine(ondasComp);
            arquivo.WriteLine(numDerrotados);
            arquivo.Close();*/
            SceneManager.LoadScene("GameOver");
        }
    }

    public void spaceSelected(int torre) {

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
            }
            painel.SetActive(false);
            espacosTorres[torreSelecionada].GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
    }

    public void setTamOnda()
    {
        inimigos = (5 * numHorda);
        timeSpawn = 1.5f - (float)(Math.Log(numHorda, 5) / 2);
        timeSpawn = (timeSpawn <= 0.1) ? 0.1f : timeSpawn;
        spawning = true;
    }

    public void toggle()
    {
        auto = !auto;
        if(inimigos == 0 && GameObject.FindGameObjectsWithTag("Inimigo").Length == 0)
        {
            setTamOnda();
        }
    }

    public void pausa()
    {
        Time.timeScale = 0;
    }

    public void x1()
    {
        Time.timeScale = 1;
    }

    public void x2()
    {
        Time.timeScale = 2;
    }
}
