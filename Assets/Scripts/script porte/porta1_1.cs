using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Porta : MonoBehaviour
{
    [Header("Configurazione")]
    [SerializeField] private string nomeScenaDaCaricare;
    
    private Animator anim;
    private bool giaAttivata = false;

    // Questa variabile può essere attivata dal tuo script "Indizio"
    public static bool indizioTrovato = false; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se è il player e non abbiamo ancora attivato la porta
        if (collision.CompareTag("Player") && !giaAttivata)
        {
            if (indizioTrovato)
            {
                StartCoroutine(SequenzaApertura());
            }
            else
            {
                Debug.Log("La porta è bloccata! Devi prima trovare l'indizio.");
            }
        }
    }

    IEnumerator SequenzaApertura()
    {
        giaAttivata = true; // Impedisce di attivare la coroutine più volte

        // 1. Attiva il trigger 'open' che hai creato nell'Animator
        anim.SetTrigger("open");

        // 2. Aspettiamo la durata della tua clip (0.85 secondi)
        yield return new WaitForSeconds(0.85f);

        // 3. Carichiamo la scena
        SceneManager.LoadScene(nomeScenaDaCaricare);
    }
}