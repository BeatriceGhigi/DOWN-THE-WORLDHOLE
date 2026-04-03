using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottone : MonoBehaviour
{
    

    [SerializeField] private string messaggioIndizio = "La password della porta è 1234...";
    [SerializeField] private Color colorePressione = Color.yellow;
    private Color coloreOriginale;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coloreOriginale = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 1. Sblocca le porte (resta true per sempre una volta attivato)
            Porta.indizioTrovato = true;

            // 2. Mostra l'indizio ogni volta che il player entra nel trigger
            Debug.Log("INDIZIO: " + messaggioIndizio);

            // 3. Cambia colore per far capire che il tasto è stato premuto
            spriteRenderer.color = colorePressione;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Quando il player si allontana, il pulsante torna al colore originale
            spriteRenderer.color = coloreOriginale;
        }
    }
}


