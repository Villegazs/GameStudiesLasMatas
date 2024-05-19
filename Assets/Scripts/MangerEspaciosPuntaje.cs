using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangerEspaciosPuntaje : MonoBehaviour
{
    [SerializeField] private float duracionPunto;
    [SerializeField] private float MaxduracionPunto;
    [SerializeField] private float tiempoEntrePuntos;
    [SerializeField] private float MaxtiempoEntrePuntos;
    [SerializeField] private int espacioActual;
    [SerializeField] private int espacioAnterior; //para no repetir puntos
    
    private bool puntoActivo;
    [SerializeField] private List<GameObject> lugaresMapa = new List<GameObject>();

    private void Start() {
        duracionPunto = MaxduracionPunto;
        tiempoEntrePuntos = MaxtiempoEntrePuntos;
        puntoActivo = false;
        foreach (GameObject obj in lugaresMapa){
            obj.SetActive(false);
        }
    }
    private void Update() {
        if (puntoActivo){
            duracionPunto-=Time.deltaTime;
            if (duracionPunto < 0){
                FinDelPunto();
                puntoActivo = !puntoActivo;
                duracionPunto=MaxduracionPunto;
            }
        }   
        else {
            tiempoEntrePuntos-=Time.deltaTime;
            if(tiempoEntrePuntos<0){
                InicioPunto();
                puntoActivo = !puntoActivo;
                tiempoEntrePuntos=MaxtiempoEntrePuntos;
            }
        }
        if (espacioActual==espacioAnterior){
            espacioActual = Random.Range(0,lugaresMapa.Count);
        }
    }

    private void FinDelPunto(){
        lugaresMapa[espacioActual].SetActive(false);
        espacioAnterior = espacioActual;
    }
    private void InicioPunto(){
        lugaresMapa[espacioActual].SetActive(true);
    }
}
