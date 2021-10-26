using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject[] espacosTorres;
    public static int vidas = 20;
    public int money = 100;
    public GameObject[] torres;
    
    int torreSelecionada = -1;

    public Text numVidas;
    public Text dinheiro;
    public GameObject painel;

    void Update()
    {
        numVidas.text = "" + vidas;
        dinheiro.text = "" + money;

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
            Instantiate(torres[torre], new Vector3(espacosTorres[torreSelecionada].transform.position.x, espacosTorres[torreSelecionada].transform.position.y + 36f), Quaternion.identity);
            money -= valor;
            espacosTorres[torreSelecionada].GetComponentInChildren<EspacoTorre>().built = true;
            painel.SetActive(false);
            espacosTorres[torreSelecionada].GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
    }
}
