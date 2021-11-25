using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //public Text texto;
    // Start is called before the first frame update
    void Start()
    {
       /* StreamReader arquivo = new StreamReader("GameOver.txt");
        int ondas = int.Parse(arquivo.ReadLine());
        int inimigos = int.Parse(arquivo.ReadLine());

        texto.text = ondas + " ondas foram concluidas com sucesso.\n" + inimigos + " tropas foram derrotadas.";*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MudaCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
}
