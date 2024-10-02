using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Hook;

public class FishingManager : MonoBehaviour
{
    public GameObject hookPrefab; // Prefab del gancho
    public Text fishListText; // UI Text para mostrar la lista de peces
    public Text catchMessageText; // UI Text para mostrar el mensaje de pez atrapado
    public Text fullInventoryMessageText; // UI Text para mostrar el mensaje de inventario lleno
    private List<Fish> caughtFish = new List<Fish>(); // Lista para almacenar los peces atrapados
    private const int maxFishLimit = 5; // Límite máximo de peces

    public int CaughtFishCount => caughtFish.Count; // Propiedad para obtener el número de peces atrapados
    public static int MaxFishLimit => maxFishLimit;
    void Start()
    {
        // Instanciar el gancho al inicio o cuando sea necesario
        Instantiate(hookPrefab, transform.position, Quaternion.identity);
        catchMessageText.gameObject.SetActive(false); // Asegurarse de que el mensaje esté oculto al inicio
        fullInventoryMessageText.gameObject.SetActive(false); // Asegurarse de que el mensaje de inventario lleno esté oculto
    }

    public void CatchFish(Fish newFish)
    {
        if (caughtFish.Count >= maxFishLimit)
        {
            StartCoroutine(ShowFullInventoryMessage()); // Mostrar mensaje si el inventario está lleno
            return; // Salir si el inventario está lleno
        }

        Debug.Log($"Pez atrapado: {newFish.name} - {newFish.weight} kg");
        // Añadir el pez a la lista
        caughtFish.Add(newFish);
        // Actualizar la UI
        UpdateFishListUI();
        // Mostrar mensaje de pez atrapado
        StartCoroutine(ShowCatchMessage());
    }

    void UpdateFishListUI()
    {
        fishListText.text = "Inventario\n"; // Reiniciar el texto
        foreach (Fish fish in caughtFish)
        {
            fishListText.text += $"{fish.name} - {fish.weight:F2} kg\n"; // Añadir cada pez a la lista
        }
    }

    IEnumerator ShowCatchMessage()
    {
        catchMessageText.text = "¡Pez Atrapado!";
        catchMessageText.gameObject.SetActive(true); // Mostrar el mensaje
        yield return new WaitForSeconds(2); // Esperar 2 segundos
        catchMessageText.gameObject.SetActive(false); // Ocultar el mensaje
    }

    public IEnumerator ShowFullInventoryMessage()
    {
        fullInventoryMessageText.text = "¡Inventario Lleno!";
        fullInventoryMessageText.gameObject.SetActive(true); // Mostrar el mensaje
        yield return new WaitForSeconds(2); // Esperar 2 segundos
        fullInventoryMessageText.gameObject.SetActive(false); // Ocultar el mensaje
    }
}