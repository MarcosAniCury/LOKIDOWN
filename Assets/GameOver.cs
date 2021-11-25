using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text texto;
    // Start is called before the first frame update
    void Start()
    {
        texto.text = Manager.ondasComp + " ondas foram concluidas com sucesso.\n" + Manager.numDerrotados + " tropas foram derrotadas.";
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
