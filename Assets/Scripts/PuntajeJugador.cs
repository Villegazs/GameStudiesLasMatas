using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntajeJugador : MonoBehaviour
{
    public float puntaje;
    [SerializeField] private float cantidadPuntaje;
    private bool ganandoPuntos;
    [SerializeField] private int numeroJugador;

    private Animator animator;
    [SerializeField] private GameObject efectoNutriendose;

    private void Start() {
        puntaje=0f;
        ganandoPuntos=false;
        animator = GetComponent<Animator>();
    }

    public void CambioDeEstado()
    {
        ganandoPuntos= !ganandoPuntos;
    }

    private void Update(){
        if (ganandoPuntos){
            puntaje += cantidadPuntaje*Time.deltaTime;
            efectoNutriendose.SetActive(true);
            InterfazPuntajes.Instance.SumarPuntosP1(puntaje*Time.deltaTime,numeroJugador);
        } else {efectoNutriendose.SetActive(false);}
        animator.SetBool("Nutriendose",ganandoPuntos);
    }

    public void Muerte(){
        InterfazPuntajes.Instance.SumarPuntosP1((-100f),numeroJugador);
        puntaje-=100f;
        if (puntaje < 0f){
            puntaje = 0f;
        }
    }
    public void Kill(){ // no se usa
        if (puntaje > 0f){
            puntaje+=10f;
        }
    }
}
