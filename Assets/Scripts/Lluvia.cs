using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lluvia : MonoBehaviour
{
    private SistemaCombate P1;
    private SistemaCombateP2 P2;
    [SerializeField] private float dano;
    [SerializeField] private bool enContacto1;
    [SerializeField] private bool enContacto2;

    private void Start() {
        enContacto1 = false;
        enContacto2 = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            P1=other.GetComponent<SistemaCombate>();
            enContacto1 = true;
        }

        if (other.gameObject.tag == "Player2") {
            P2=other.GetComponent<SistemaCombateP2>();
            enContacto2 = true;
        }
    }

    private void Update() {
        if (enContacto1) {
            P1.TomarDaño(dano*Time.deltaTime);}
        if (enContacto2) {
            P2.TomarDaño(dano*Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            P1=other.GetComponent<SistemaCombate>();
            enContacto1 =false;
        }

        if (other.gameObject.tag == "Player2") {
            P2=other.GetComponent<SistemaCombateP2>();
            enContacto2 = false;
        }
    }
}
