using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    // Questa riga farà apparire il rettangolo nell'Inspector dove scrivere il nome
    [SerializeField] private string nomeScenaDaCaricare; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeScenaDaCaricare);
        }
    }
}