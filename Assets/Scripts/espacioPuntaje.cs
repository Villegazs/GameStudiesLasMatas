using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class espacioPuntaje : MonoBehaviour
{
    private PuntajeJugador puntajeJugador;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Jugadores")){
            puntajeJugador=other.gameObject.GetComponent<PuntajeJugador>();
            puntajeJugador.CambioDeEstado();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Jugadores")){
            puntajeJugador=other.gameObject.GetComponent<PuntajeJugador>();
            puntajeJugador.CambioDeEstado();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        //Metes condicionales para animaciones de la plataformas u actualizar cosas
    }
}
