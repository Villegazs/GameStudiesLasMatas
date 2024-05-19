using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraVidaP1 : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }



    public void CambiarVidaActual(float vidaActual){
        slider.value = vidaActual;
    }

    public void InicializarBarra (float cantidadVida){
        CambiarVidaActual(cantidadVida);
    }
}
