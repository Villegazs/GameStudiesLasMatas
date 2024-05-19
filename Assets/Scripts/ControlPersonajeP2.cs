using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPersonajeP2 : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    [Header("Movimiento")] //agregar aceleracion y desaceleracion?
    private float movimientoHorizontal = 0f;
    [Range(30,500)][SerializeField] private float velocidadMovimiento;  
    [Range(0,0.5f)][SerializeField] private float suavizadorMovimiento;
    private Vector3 velocidad = Vector3.zero;
    public bool mirandoDerecha;  

    [Header("Salto")]

    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimesionControladorSuelo;
    [SerializeField] private LayerMask queEsSuelo;
    private bool saltando;
    private bool enSuelo;

    [Header("Doble Salto")]

    [SerializeField] private int saltosRestantes;
    [SerializeField] private int cantidadSaltos;
    [Range(0,0.5f)][SerializeField] private float tiempoEntreSaltos;
    private float timepoUltimoSalto;

    [Header("SaltoRegulable")]
    [Range(0,1)][SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multGravedad;
    private float escalaGravedad;
    private bool soltarSalto=true;

    [Header("Dash")]
    [SerializeField] private float velocidadDash;
    [Range(0,1)][SerializeField] private float tiempoDash;
    [SerializeField] private float tiempoUltimoDash;
    private bool puedeDashear= true;
    private bool sePuedeMover = true;
    [SerializeField] private TrailRenderer trailRenderer;

    //[Header("CosasDeCamara")]
    //private CamaraFollowObject camaraFollowObject;
    //[SerializeField] private GameObject camaraFollowGo;

    [Range(-100,-10)][SerializeField]private float maxVelocidadCaida;

    //agregar coyoteTime y buffer (temporizadores para salto y suelo)





    // Start is called before the first frame update
    void Start()
    {
        rb2D=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        escalaGravedad = rb2D.gravityScale;
        mirandoDerecha=true;

        //camaraFollowObject=camaraFollowGo.GetComponent<CamaraFollowObject>();
    }

    // Update is called once per frame
    void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("HorizontalP2")*velocidadMovimiento;
        animator.SetFloat("Movimiento", Mathf.Abs(movimientoHorizontal));

        if(Input.GetButtonDown("JumpP2")){
            saltando=true;
            animator.SetTrigger("Salto");
        }

        enSuelo=Physics2D.OverlapBox(controladorSuelo.position,dimesionControladorSuelo,0f,queEsSuelo);

        if(Input.GetButtonUp("JumpP2")){
            SoltarSalto();
        }

        if (enSuelo){
            saltosRestantes=cantidadSaltos;
        }
        timepoUltimoSalto+=Time.deltaTime;

        if (puedeDashear){tiempoUltimoDash+=Time.deltaTime;}

        if (Input.GetKeyDown(KeyCode.DownArrow) && tiempoUltimoDash>1.0f){
            StartCoroutine(Dash());
            tiempoUltimoDash=0;
        }
        
    }

    private void FixedUpdate(){

        if(sePuedeMover){ //no este dasheando
        Mover(movimientoHorizontal*Time.fixedDeltaTime);}

        if(saltando && soltarSalto && enSuelo){
            Saltar();
            timepoUltimoSalto=0f;
            tiempoUltimoDash+=1;
        }
        else if (saltando && saltosRestantes>0 && timepoUltimoSalto>=tiempoEntreSaltos){
            Saltar();
            saltosRestantes-=1;
            tiempoUltimoDash+=1;
        }

        if(rb2D.velocity.y < 0 && !enSuelo){ //cayendo
            rb2D.gravityScale = escalaGravedad*multGravedad;
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Max(rb2D.velocity.y,maxVelocidadCaida));
        }
        else if (Input.GetButton("Fall") && !enSuelo){
            rb2D.gravityScale = escalaGravedad*multGravedad*2;
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Max(rb2D.velocity.y,maxVelocidadCaida));
        }
        else{
            if(sePuedeMover){
            rb2D.gravityScale=escalaGravedad;}
        }
        //saltando=false;
    }

    private void Mover(float mover){
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity,velocidadObjetivo, ref velocidad, suavizadorMovimiento);

        if(mover>0 && !mirandoDerecha){Girar();}
        else if (mover<0 && mirandoDerecha){Girar();}
    }

    private void Girar(){
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

        //camaraFollowObject.CallTurn();
    }

    private void Saltar(){
        rb2D.velocity = new Vector2(rb2D.velocity.x,fuerzaSalto);
        enSuelo=false;
        soltarSalto=false;
        saltando=false;
    }

    private void SoltarSalto(){
        if(rb2D.velocity.y > 0){
            rb2D.AddForce(Vector2.down*rb2D.velocity.y *multiplicadorCancelarSalto, ForceMode2D.Impulse);
        }
        soltarSalto=true;
        if (saltosRestantes<=0){
            saltando=false;
        }
    }

    private IEnumerator Dash(){

        sePuedeMover=false;
        puedeDashear=false;
        rb2D.gravityScale=0f;
        rb2D.velocity = new Vector2(velocidadDash*transform.localScale.x,0);
        trailRenderer.emitting=true;

        //animator.SetTrigger("Dash");

        yield return new WaitForSeconds(tiempoDash);

        sePuedeMover=true;
        puedeDashear=true;
        rb2D.gravityScale=escalaGravedad;
        trailRenderer.emitting =false;
    
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position,dimesionControladorSuelo);
    }

    #region EfectosMapa
    public void DentroAgua(float reduccion){
        velocidadMovimiento-=reduccion;
    }
    public void FueraAgua(float reduccion){
        velocidadMovimiento+=reduccion;
    }
    #endregion
}
