using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Hook;

public class FishingManager : MonoBehaviour
{
    public GameObject hookPrefab; // Prefab del gancho
    public GameObject inventoryPanel; // El panel del inventario
    public Text fishListText; // UI Text para mostrar la lista de peces dentro del Scroll View
    public Text catchMessageText; // UI Text para mostrar el mensaje de pez atrapado
    public Text fullInventoryMessageText; // UI Text para mostrar el mensaje de inventario lleno
    public List<Fish> caughtFish = new List<Fish>(); // Lista para almacenar los peces atrapados
    private int maxFishLimit = 5; // L�mite m�ximo de peces inicial

    private bool isInventoryOpen = false; // Controla si el inventario est� abierto o cerrado

    public int CaughtFishCount => caughtFish.Count; // Propiedad para obtener el n�mero de peces atrapados
    public int MaxFishLimit => maxFishLimit; // Propiedad para obtener el l�mite de inventario actual

    void Start()
    {
        // Instanciar el gancho al inicio o cuando sea necesario
        Instantiate(hookPrefab, transform.position, Quaternion.identity);
        catchMessageText.gameObject.SetActive(false); // Asegurarse de que el mensaje est� oculto al inicio
        fullInventoryMessageText.gameObject.SetActive(false); // Asegurarse de que el mensaje de inventario lleno est� oculto

        // Asegurarse de que el panel del inventario est� oculto al inicio
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Abrir/cerrar el inventario al presionar la tecla I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    // M�todo para atrapar un pez
    public void CatchFish(Fish newFish)
    {
        if (caughtFish.Count >= maxFishLimit)
        {
            StartCoroutine(ShowFullInventoryMessage()); // Mostrar mensaje si el inventario est� lleno
            return; // Salir si el inventario est� lleno
        }

        Debug.Log($"Pez atrapado: {newFish.name} - {newFish.weight} cm");
        // A�adir el pez a la lista
        caughtFish.Add(newFish);
        // Actualizar la UI
        UpdateFishListUI();
        FindObjectOfType<Conversacion>().CollectFish();
        // Mostrar mensaje de pez atrapado
        StartCoroutine(ShowCatchMessage()); // Mostrar el mensaje de pez atrapado
    }

    // M�todo para actualizar la lista de peces en la UI
    public void UpdateFishListUI()
    {
        fishListText.text = "Inventario\n"; // Reiniciar el texto
        foreach (Fish fish in caughtFish)
        {
            fishListText.text += $"{fish.name} - {fish.weight:F2} cm\n"; // A�adir cada pez a la lista
        }
    }

    public IEnumerator ShowCatchMessage()
    {
        catchMessageText.text = "�Pez atrapado!";
        catchMessageText.gameObject.SetActive(true); // Mostrar el mensaje
        yield return new WaitForSeconds(2); // Mostrar durante 2 segundos
        catchMessageText.gameObject.SetActive(false); // Ocultar el mensaje
    }

    public IEnumerator ShowFullInventoryMessage()
    {
        fullInventoryMessageText.text = "�Inventario Lleno!";
        fullInventoryMessageText.gameObject.SetActive(true); // Mostrar el mensaje
        yield return new WaitForSeconds(2); // Esperar 2 segundos
        fullInventoryMessageText.gameObject.SetActive(false); // Ocultar el mensaje
    }

    // M�todo para alternar la visibilidad del inventario
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen; // Cambiar el estado
        inventoryPanel.SetActive(isInventoryOpen); // Mostrar/ocultar el panel del inventario
    }

    // Nuevo m�todo para aumentar el l�mite de inventario
    public void IncreaseInventoryLimit()
    {
        maxFishLimit = 10; // Aumentar el l�mite de peces a 10
        Debug.Log("El l�mite de inventario ha aumentado a 10.");
    }
}