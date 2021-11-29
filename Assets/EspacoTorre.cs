using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspacoTorre : MonoBehaviour
{

    public bool built = false;
    public GameObject manager;
    public int posicaoVetor;
    public GameObject[] wps;
    public AudioSource somClique;

    private void OnMouseDown()
    {
        somClique.Play();
        manager.GetComponent<Manager>().spaceSelected(posicaoVetor);
    }
}
