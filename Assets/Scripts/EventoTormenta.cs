using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoTormenta : MonoBehaviour
{
    [SerializeField] private GameObject particula;
    [SerializeField] private Vector3 fuerzaEmpuje;
    [SerializeField] private float tiempoEmpuje;
    [SerializeField] private float tiempoEntreEmpujes;
    [SerializeField] private float tiempoEntreEmpujesMax;
    private int tipoDeEmpuje;
    private bool empujando;

    private void Start() {
        empujando =false;
        tiempoEntreEmpujes = tiempoEntreEmpujesMax;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Jugadores")){
            if (empujando){
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(fuerzaEmpuje, ForceMode2D.Force);
            }
        }
    }
    private void Update() {
        if (!empujando){
            tiempoEntreEmpujes -= Time.deltaTime;
            if (tiempoEntreEmpujes<0){
                empujando = true;
                tiempoEmpuje = Random.Range(2,6);
                particula.SetActive(true);
                NuevoEmpuje();
            }
        }
        else{
            tiempoEmpuje -= Time.deltaTime;
            if (tiempoEmpuje<0){
                empujando=false;
                tiempoEntreEmpujes = tiempoEntreEmpujesMax;
                particula.SetActive(false);
            }
        }
    }

    private void NuevoEmpuje(){
        tipoDeEmpuje= Random.Range(0,3);
        switch (tipoDeEmpuje){
            case 0:
            fuerzaEmpuje = new Vector3(-100, -20,0);
            break;
            case 1:
            fuerzaEmpuje = new Vector3(50, 100,0);
            break;
            case 2:
            fuerzaEmpuje = new Vector3(150, -100,0);
            break;
            default:
            fuerzaEmpuje = new Vector3(0, 300,0);
            tiempoEmpuje = 1.0f;
            break;
        }
    }
    
}
