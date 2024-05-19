using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AyudaDios : MonoBehaviour
{
    public static AyudaDios Instance;
    private InterfazPuntajes interfazPuntos;
    public float puntos1 = 0f;
    public float puntos2 = 0f;
    private int rondas; private int escenas;

    private void Awake() {
        if (AyudaDios.Instance == null) {
            AyudaDios.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start() {
        interfazPuntos = GameObject.Find("UI_Canva").GetComponent<InterfazPuntajes>();
    }

    public void almacenarPuntos(float p1, float p2) {
        puntos1 += p1-puntos1;
        puntos2 += p2-puntos2;
        rondas++;
    }
    private void Update() {
        if (rondas>3){
        if (puntos1 > puntos2)
            {
                puntos1 = 0; puntos2=0;
                SceneManager.LoadScene(5);
            }
            if (puntos2 > puntos1)
            {
                puntos1 = 0; puntos2 = 0;
                SceneManager.LoadScene(6);
            }
            escenas = 0;
        }
    }
}
