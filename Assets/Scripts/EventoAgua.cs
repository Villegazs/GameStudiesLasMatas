using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoAgua : MonoBehaviour
{
    [SerializeField] private float velocidadReducida;
    //[SerializeField] private float dano;
    [SerializeField] private float velocidadDeSubida;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            other.GetComponent<ControlPersonajeP1>().DentroAgua(velocidadReducida);
        }

        if(other.gameObject.tag == "Player2") {
            other.GetComponent<ControlPersonajeP2>().DentroAgua(velocidadReducida);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            other.GetComponent<ControlPersonajeP1>().FueraAgua(velocidadReducida);
        }

        if(other.gameObject.tag == "Player2") {
            other.GetComponent<ControlPersonajeP2>().FueraAgua(velocidadReducida);
        }
    }

    private void Update() {
        transform.Translate (Vector3.up*velocidadDeSubida*Time.deltaTime);
    }
}
