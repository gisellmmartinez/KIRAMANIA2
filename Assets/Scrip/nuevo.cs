using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Control Personaje")]
    [Header("Vida Personaje")]
    public int vidaTotal;
    public   int vidaActual;
    public TMP_Text textoVidaPlayer;

    public Vector3 posicionInicial;

    [Header("Movimiento Personaje")]
        float horizontal;
    public float fuerzaHorizontal;
    public float fuerzaVertical;  
        Rigidbody2D rigidbody2D;
        

    [Header("Control Rayo")]
    public float altruaRayo;   
    public LayerMask piso;    
    public bool tocandoPiso;



    // Start is called before the first frame update
    void Start()
    {
        vidaActual=vidaTotal;
        rigidbody2D= GetComponent<Rigidbody2D>();
    }

    private void Update() {
        //Movimiento Horizontal del personaje
        horizontal= Input.GetAxisRaw("Horizontal");
        if(horizontal!=0 ){
            transform.position += Vector3.right * horizontal * fuerzaHorizontal* Time.deltaTime;
        }

        Debug.DrawRay(transform.position, Vector3.down * altruaRayo, Color.blue, 0.1f );
        if(Physics2D.Raycast(transform.position, Vector2.down,altruaRayo, piso )){
            tocandoPiso= true;
            //Debug.Log("Hit"); 
            rigidbody2D.gravityScale=1f; 
        }else{
            tocandoPiso=false;
            rigidbody2D.gravityScale=2f;  
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)&& tocandoPiso){
            //Debug.Log("Salto");
            rigidbody2D.velocity= Vector2.up*fuerzaVertical;
        }  

        PlayerDead();   

        
    }
    

    // Update is called once per frame
    public void Vida(int vidaRecibe)
    {
        vidaActual+=vidaRecibe;
        textoVidaPlayer.text= "Vida:"+ vidaActual.ToString(); 
        Debug.Log("Cambio de vida: " +vidaRecibe );
    }

    public void PlayerDead(){
        if(vidaActual<=0){
            vidaActual= 100;
            transform.position= posicionInicial;
            textoVidaPlayer.text= "Vida:"+ vidaActual.ToString();  
            
        }
        
    }
}
