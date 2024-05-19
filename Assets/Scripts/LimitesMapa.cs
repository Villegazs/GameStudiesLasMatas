using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitesMapa : MonoBehaviour
{
    private SistemaCombate P1;
    private SistemaCombateP2 P2;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            P1 = other.GetComponent<SistemaCombate>();
            P1.Reaparecer(1.0f);
            //StartCoroutine(P1.Respawn(1.0f));
        }
        if (other.gameObject.tag == "Player2"){
            P2 = other.GetComponent<SistemaCombateP2>();
            StartCoroutine(P2.Respawn(1.0f));
        }
    }
}
