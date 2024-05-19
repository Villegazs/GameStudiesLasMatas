using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaCombateP2 : MonoBehaviour
{
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private Transform otroJugador;
    [SerializeField] private float multEmpuje;
    private float fuerzaEmpuje;
    private Vector2 direccionEmpuje;

    [SerializeField] private float vida;
    [SerializeField] private float vidaMax;
    private float cargaAtaque;
    private bool cargandoAtaque;
    [Range(0f, 3.0f)][SerializeField] private float velocidadCarga;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float radioAtaqueMax;

    private Animator animator;
    private Animator animatorAtks; [SerializeField] private GameObject esfera;

    [SerializeField] private Vector2 startPosicion;
    [SerializeField] private float tiempoRespawn;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;

    [SerializeField] private barraVidaP2 barraVidaP2;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        cargaAtaque=0f;
        cargandoAtaque=false;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        animatorAtks = esfera.GetComponent<Animator>();
        startPosicion = transform.position;
        vida = vidaMax;

        barraVidaP2.InicializarBarra(vida);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L) ){
            cargandoAtaque=true; 
            
        }

        if (Input.GetKeyUp(KeyCode.L)){
            switch (cargaAtaque){ //este es importante para las animaciones, pero mecanicamente no significa mucho
                case <0.5f:
                Debug.Log("Ataque debil");
                animatorAtks.SetTrigger("atkDebil");
                break;
                case <1f:
                Debug.Log("Ataque normal");
                animatorAtks.SetTrigger("atkMedio");
                break;
                case <2f:
                Debug.Log("Ataque fuerte");
                animatorAtks.SetTrigger("atkFuerte");
                break;
                default:
                Debug.Log("Ataque super");
                animatorAtks.SetTrigger("atkSuper");
                break;
            }
            Golpe(cargaAtaque*10);   //Cuando se lanza el ataque, segun la carga, hace mas o menos daño
            cargandoAtaque=false;
            vida-=(int)cargaAtaque*10; //*Time.deltaTime y dentro de la carga para que se pierda progresivamente
            cargaAtaque=0f;
            radioAtaque=0.5f; 
        }

        if  (cargandoAtaque && cargaAtaque<3){ //mientras se esta cargando el ataque = aumento de rango, aumento de empuje
            cargaAtaque += Time.deltaTime*velocidadCarga;
            Debug.Log("Carga al: " + cargaAtaque);
            radioAtaque += Time.deltaTime*2;
            fuerzaEmpuje = cargaAtaque * multEmpuje;
        }
        animator.SetBool("CargandoAtk",cargandoAtaque);
        animatorAtks.SetBool("cargandoAtk",cargandoAtaque);
    }

    private void Golpe(float cant){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(puntoAtaque.position,radioAtaque);
        foreach (Collider2D colisionador in objetos){
            if (colisionador.CompareTag("Player")){
                vida+=(int)cant;
                colisionador.transform.GetComponent<SistemaCombate>().TomarDaño(cant);
                colisionador.transform.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje*fuerzaEmpuje,ForceMode2D.Impulse);
            }
        }
    }
    private void FixedUpdate() { //tomar en todo momento la distancia entre los jugadores, se hace un vector para la direccion del empuje
        direccionEmpuje = new Vector2(otroJugador.position.x - this.transform.position.x, otroJugador.position.y - this.transform.position.y);
    }

    public void TomarDaño(float damage){
        vida-=damage;

        barraVidaP2.CambiarVidaActual(vida);

        if (cargandoAtaque){
            cargandoAtaque=false;
        }
        if (vida<0){
            GetComponent<PuntajeJugador>().Muerte();
            StartCoroutine(Respawn(tiempoRespawn));}
    }

    public IEnumerator Respawn (float cd){
        rb2D.simulated=false;
        spriteRenderer.enabled=false;
        yield return new WaitForSeconds(cd);
        transform.position = startPosicion;
        spriteRenderer.enabled=true;
        rb2D.simulated=true;
        vida = vidaMax;

        barraVidaP2.CambiarVidaActual(vida);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(puntoAtaque.position,radioAtaque);
    }
}
