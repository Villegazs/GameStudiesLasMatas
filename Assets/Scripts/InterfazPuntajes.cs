using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InterfazPuntajes : MonoBehaviour
{
    public static InterfazPuntajes Instance;
    private float puntosP1;
    private float puntosP2;
    private float contador;
    [SerializeField]private float duracionRondas;
    private int escenas;

    private GameObject P1, P2;

    [SerializeField] private TextMeshProUGUI textoP1;
    [SerializeField] private TextMeshProUGUI textoP2; 

    [SerializeField] private PuntajeJugador puntajeATomar;

    private void Awake() {
        if (InterfazPuntajes.Instance == null) {
            InterfazPuntajes.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    private void Start() {
        escenas = 0;
        contador = 0;
        P1 = GameObject.Find("P1");
        P2 = GameObject.Find("P2");
    }

    private void Update() {
        //puntosP1 = P1.GetComponent<PuntajeJugador>().puntaje;
        //puntosP2 = P2.GetComponent<PuntajeJugador>().puntaje;
        contador += Time.deltaTime;
        textoP1.text = puntosP1.ToString("0");
        textoP2.text = puntosP2.ToString("0");
        if (contador>duracionRondas )
        {
            escenas++;
            SceneManager.LoadScene(Random.Range(1, 5));
            contador = 0;
        }
        if(escenas>=3)
        {
            if (puntosP1 > puntosP2)
            {
                puntosP1 = 0; puntosP2=0;
                SceneManager.LoadScene(5);
            }
            if (puntosP2 > puntosP1)
            {
                puntosP1 = 0; puntosP2 = 0;
                SceneManager.LoadScene(6);
            }
            escenas = 0;

        }
        

    }

    public void SumarPuntosP1(float cantidad, int tipo){
        if (tipo==1){
        puntosP1 += cantidad;
        }
        if (tipo==2){
            puntosP2 += cantidad;
        }
    }

}
