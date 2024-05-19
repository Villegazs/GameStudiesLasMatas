using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoLluvia : MonoBehaviour
{
    [SerializeField] private List<GameObject> lluvias;
    [SerializeField] private int lluviaActual;
    [SerializeField] private float tiempoLLuvia;
    [SerializeField] private float tiempoLLuviaMax;
    [SerializeField] private float tiempoEntrelluvias;
    [SerializeField] private float tiempoEntrelluviasMax;
    private bool lloviendo;

    private void Start() {
        foreach (GameObject lluvia in lluvias){
            lluvia.SetActive(false);
        }
        tiempoEntrelluvias =5;
        lloviendo = false;
        tiempoLLuvia = tiempoLLuviaMax;
    }

    private void Update() {
        if (!lloviendo){
            tiempoEntrelluvias-=Time.deltaTime;
            if (tiempoEntrelluvias<0){
                lluviaActual = Random.Range(0, lluvias.Count);
                ActivarLluvia(lluviaActual);
                tiempoEntrelluvias=tiempoEntrelluviasMax;
                lloviendo = true;
            }
        }
        else{
            tiempoLLuvia-=Time.deltaTime;
            if(tiempoLLuvia<0){
                DesactivarLluvia(lluviaActual);
                tiempoLLuvia=tiempoLLuviaMax;
                lloviendo = false;
            }         
        }
    }

    public void ActivarLluvia(int lluviaActual) {
        lluvias[lluviaActual].SetActive(true);
    }
    public void DesactivarLluvia(int lluviaActual){
        lluvias[lluviaActual].SetActive(false);
    }
}
