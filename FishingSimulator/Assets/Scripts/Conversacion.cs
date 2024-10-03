using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Conversacion : MonoBehaviour
{
    public GameObject npc; // El NPC con el que interactuamos
    public Text instructionText; // Texto de instrucciones en pantalla al iniciar el juego
    public Text interactText; // Texto para indicar "Presiona E para interactuar"
    public Text dialogueText; // Texto para los diálogos
    public GameObject dialoguePanel; // Panel de diálogo para mostrar los textos
    public GameObject missionPanel; // Panel para mostrar la misión activa
    public Text missionText; // Texto para la misión
    public GameObject player; // El jugador
    public BoxCollider npcCollider; // El collider del NPC
    private bool isPlayerNearNPC = false; // Para detectar si el jugador está cerca del NPC
    private bool hasCollectedFish = false; // Para verificar si ya recolectó el pez
    private bool hasTalkedOnce = false; // Verifica si la primera conversación ocurrió

    void Start()
    {
        instructionText.text = "Dirigete hacia el hombre que esta pescando e interactua con el";
        interactText.gameObject.SetActive(false); // Ocultar el texto de "Presiona E" al inicio
        dialoguePanel.SetActive(false); // Ocultar el panel de diálogo
        missionPanel.SetActive(false); // Ocultar el panel de misión
    }

    void Update()
    {
        // Detectar si el jugador está cerca y presiona E para interactuar
        if (isPlayerNearNPC && Input.GetKeyDown(KeyCode.E))
        {
            if (!hasTalkedOnce)
            {
                StartCoroutine(StartFirstConversation());
            }
            else if (hasCollectedFish)
            {
                StartCoroutine(SecondConversation());
            }
        }
    }

    // Detectar si el jugador entra en el trigger del NPC
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerNearNPC = true;
            interactText.gameObject.SetActive(true); // Mostrar "Presiona E para interactuar"
        }
    }

    // Detectar si el jugador sale del trigger del NPC
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerNearNPC = false;
            interactText.gameObject.SetActive(false); // Ocultar "Presiona E"
        }
    }

    // Primera conversación con el NPC
    IEnumerator StartFirstConversation()
    {
        interactText.gameObject.SetActive(false); // Ocultar el texto de interacción
        dialoguePanel.SetActive(true);
        dialogueText.text = "NPC: Hola, bienvenido al lago.";
        yield return new WaitForSeconds(2f);

        dialogueText.text = "Player: Buenass, gracias ¿Que se hace aca?";
        yield return new WaitForSeconds(3f);

        dialogueText.text = "NPC: La gente se junta a pescar, activa tu caña con 'C' e intenta lanzarla al agua presionando click derecho, luego recuperala con click izquierdo.";
        yield return new WaitForSeconds(7f);

        dialogueText.text = "Player: Bueno dale ahi lo intento.";
        yield return new WaitForSeconds(2f);

        dialoguePanel.SetActive(false); // Ocultar el panel de diálogo
        hasTalkedOnce = true; // Marcar que ya se habló una vez

        // Ocultar el texto de instrucciones al terminar la conversación
        instructionText.gameObject.SetActive(false);

        // Mostrar la misión
        missionPanel.SetActive(true);
        missionText.text = "Mision: Recolecta un pez.";
    }

    // Segunda conversación con el NPC después de recolectar el pez
    IEnumerator SecondConversation()
    {
        interactText.gameObject.SetActive(false);
        dialoguePanel.SetActive(true);
        dialogueText.text = "NPC: Buen trabajo, ahora podes verlo en tu inventario (I), intenta venderlo en la tienda.";
        yield return new WaitForSeconds(6f);

        dialogueText.text = "NPC: Si juntas suficiente dinero, vas a poder mejorar tu equipamiento.";
        yield return new WaitForSeconds(3f);

        dialoguePanel.SetActive(false); // Ocultar el panel de diálogo
        missionPanel.SetActive(false); // Ocultar la misión después de completar la conversación
    }

    // Método para llamar cuando el jugador recolecta un pez
    public void CollectFish()
    {
        hasCollectedFish = true;
        missionText.text = "Mision completa: Pez recolectado.";
    }
}
