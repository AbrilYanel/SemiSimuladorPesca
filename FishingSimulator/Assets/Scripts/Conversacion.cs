using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Conversacion : MonoBehaviour
{
    public GameObject npc; // El NPC con el que interactuamos
    public Text instructionText; // Texto de instrucciones en pantalla al iniciar el juego
    public Text interactText; // Texto para indicar "Presiona E para interactuar"
    public Text dialogueText; // Texto para los di�logos
    public GameObject dialoguePanel; // Panel de di�logo para mostrar los textos
    public GameObject missionPanel; // Panel para mostrar la misi�n activa
    public Text missionText; // Texto para la misi�n
    public GameObject player; // El jugador
    public BoxCollider npcCollider; // El collider del NPC
    private bool isPlayerNearNPC = false; // Para detectar si el jugador est� cerca del NPC
    private bool hasCollectedFish = false; // Para verificar si ya recolect� el pez
    private bool hasTalkedOnce = false; // Verifica si la primera conversaci�n ocurri�

    void Start()
    {
        instructionText.text = "Dir�gete hacia el hombre que est� pescando e interact�a con �l";
        interactText.gameObject.SetActive(false); // Ocultar el texto de "Presiona E" al inicio
        dialoguePanel.SetActive(false); // Ocultar el panel de di�logo
        missionPanel.SetActive(false); // Ocultar el panel de misi�n
    }

    void Update()
    {
        // Detectar si el jugador est� cerca y presiona E para interactuar
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

    // Primera conversaci�n con el NPC
    IEnumerator StartFirstConversation()
    {
        interactText.gameObject.SetActive(false); // Ocultar el texto de interacci�n
        dialoguePanel.SetActive(true);
        dialogueText.text = "NPC: Hola, bienvenido al lago.";
        yield return new WaitForSeconds(2f);

        dialogueText.text = "Player: �Qu� se hace ac�?";
        yield return new WaitForSeconds(3f);

        dialogueText.text = "NPC: La gente se junta a pescar, intenta lanzar la ca�a al agua presionando click derecho, luego recup�rala con click izquierdo.";
        yield return new WaitForSeconds(5f);

        dialogueText.text = "Player: De acuerdo.";
        yield return new WaitForSeconds(2f);

        dialoguePanel.SetActive(false); // Ocultar el panel de di�logo
        hasTalkedOnce = true; // Marcar que ya se habl� una vez

        // Ocultar el texto de instrucciones al terminar la conversaci�n
        instructionText.gameObject.SetActive(false);

        // Mostrar la misi�n
        missionPanel.SetActive(true);
        missionText.text = "Misi�n: Recolecta un pez.";
    }

    // Segunda conversaci�n con el NPC despu�s de recolectar el pez
    IEnumerator SecondConversation()
    {
        interactText.gameObject.SetActive(false);
        dialoguePanel.SetActive(true);
        dialogueText.text = "NPC: Buen trabajo, ahora puedes verlo en tu inventario (I), intenta venderlo en la tienda.";
        yield return new WaitForSeconds(6f);

        dialogueText.text = "NPC: Si juntas suficiente dinero, podr�s mejorar tu equipamiento.";
        yield return new WaitForSeconds(3f);

        dialoguePanel.SetActive(false); // Ocultar el panel de di�logo
        missionPanel.SetActive(false); // Ocultar la misi�n despu�s de completar la conversaci�n
    }

    // M�todo para llamar cuando el jugador recolecta un pez
    public void CollectFish()
    {
        hasCollectedFish = true;
        missionText.text = "Misi�n completa: Pez recolectado.";
    }
}
