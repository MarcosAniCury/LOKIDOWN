using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class ButtonHandle : MonoBehaviour
{
    //Read from a file
    //string something = File.ReadAllText("C:\\Rfile.txt");
    private string character;
    public Image[] images;
    public Text[] descricao;
    public Button botao;

    private void Start()
    {
        int c = 0;
        while (c < images.Length && c < descricao.Length) {

            images[c].gameObject.SetActive(false);
            descricao[c].gameObject.SetActive(false);
            c++;
        }

        if (botao) {

            botao.gameObject.SetActive(false);
        }

        character = "Default";
    }

    public void selectChar(string personagem) {

        int dont = -1;

        switch (personagem) {

            case "General": 
                dont = 0;
                break;
            case "Maga":
                dont = 1;
                break;
        }

        int c = 0;
        while (c < images.Length && c < descricao.Length) {

            images[c].gameObject.SetActive((c == dont));
            descricao[c].gameObject.SetActive((c == dont));
            c++;
        }

        if (botao)
        {

            botao.gameObject.SetActive(true);
        }

        character = personagem;
    }

    public void ChangeScene(string sceneName)
    {
        //Ace-SampleScene#1
        /*int p1 = sceneName.IndexOf('-');
        character = sceneName.Substring(0, p1);
        string scene = sceneName.Substring(p1+1);*/
        if (character != "Default")//NOT This function has been called only to change the scene
        {
            //Saving data
            StreamWriter writer = new StreamWriter("SceneArgs.txt");
            writer.Write(character);
            writer.Close();
        }
        //
        //Changing Scene
        SceneManager.LoadScene(sceneName);
    }

    public void Sair()
    {
        Application.Quit();
    }
}