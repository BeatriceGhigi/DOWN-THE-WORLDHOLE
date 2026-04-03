using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour   //la classe eredita da monobehaviour, quindi può essere attaccata a un gameobject
{
    // Start is called before the first frame update
    public float moveSpeed;     //velocità movimento personaggio
    private bool isMoving;      //capire se si sta muovendo
    public float stepSize = 1f;    // 0.2f;   //ogni passo sposta di 0.2f
    private Vector2 input;      //serve a vedere se c'è qualche movimento dalla tasteria di chi gioca
    /*memorizza l'input della tastera, ha due componenti: x e y*/
    private Animator animator;      //componente dell'animator creato nel gioco

    public LayerMask solidObjectsLayer;

/*Awake è una funzione speciale di unity, lifecycle method
  Unity la chiama automaticamente
  Unity cerca il componente Animator attaccato allo stesso 
  oggetto e lo salva nella variabile animator*/
    private void Awake()
    {
        //viene chiamata quando l'oggetto viene creato
        animator = GetComponent<Animator>();
    }
    void Start() {
        /*Start() viene chiamato una sola volta all'inizio del gioco
        serve a: inizializzare variabili, preparare oggetti, caricare dati*/
    }

    /* Update is called once per frame
    è una funzione che viene chiamata continuamente, ogni frame
    Qui lo usiamo perche l'input della tastiera va controllato continuamente*/
    private void Update()
    {
        /*qui viene eseguita tutta la logica del movimento*/
        if(!isMoving)
        {
            //vuol dire che se il personaggio non si sta muovendo, stai pronto a leggere l'input
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            //getAxisRaw si usa quando vuoi movimenti più a scatti

            //debug, stampa i valori dell'input
            Debug.Log("This is input.x" + input.x);
            Debug.Log("This is input.y" + input.y);


            if(input != Vector2.zero)   //Vector2.zero=(0,0), quindi ti dice: se ti vuoi muovere, allora...
            {
                //passiamo all'animator la direzione del movimento
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);
                
                var targetPos = transform.position;     //target è la destinazione, la impostiamo sulla posizione attuale
                //cambio posizione, assegnando le nuove coordinate al target
                targetPos.x+=input.x * stepSize;
                targetPos.y+=input.y * stepSize;


                if(isWalkable(targetPos)) {
                //Ora avvio il movimento per arrivare a target
                StartCoroutine(Move(targetPos));
                //Coroutine è una funzione che serve a fare operazioni distribuite nel tempo
                } 
               
            }
        }
    
        

        //l'animator ogni volta deve aggiornare il booleano tra Idle e Walk
        animator.SetBool("isMoving", isMoving);
    }

    /*Nell'input uso Vector2 perché tanto da tastiera riguarda solo orizzontale e verticale
    quindi due dimensioni vanno bene
    Per la posizione e l'aggiornamento della posizione si usa Vector3 che comprende anche l'asse z
    */

    /*
      COROUTINE
      funzione che si può fermare nel tempo, riprendere più tardi
      Serve per muovere il personaggio nel tempo
      Senza coroutine il personaggio si teletrasporterebbe
      IEnumerator serve a dire a unity che la funzione è coroutine e può usare yield
    */
    IEnumerator Move(Vector3 targetPos)     
    {
        while ((targetPos-transform.position).sqrMagnitude > 0.0001f)
        //while ((targetPos-transform.position).sqrMagnitude>Mathf.Epsilon)
        {
            //se c'è stato un movimento (target-posizione corrente) ed è 
            // maggiore di poco più di 0(mathf.epsilon) allora mi sto muovendo
            isMoving=true;

            //moveTowards sposta la posizione un piccolo passo verso la destinazione
            //Time.deltaTime è il tempo tra due frame
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed*Time.deltaTime);
            yield return null;
            //significa fermati e vai al frame successivo
        }

        //aggiorniamo la posizione esatta e segnaliamo che non ci stiamo più muovendo
        transform.position=targetPos;
        isMoving=false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.15f, solidObjectsLayer)!=null)  //era 0.2f
        {
            return false;
        }
        return true;
    }
}
