using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {

        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(0);

        }
    }
    public void OnClickJugar()
    {
       SceneManager.LoadScene(Random.Range(1,5));
    }

    public void OnClickSalir() 
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene(0);
    }
}
