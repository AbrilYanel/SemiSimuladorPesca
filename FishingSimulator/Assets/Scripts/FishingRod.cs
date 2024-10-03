using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject hookPrefab; // El prefab del anzuelo
    public Transform hookOrigin;  // El punto desde donde se lanza el anzuelo
    public float castForce = 10f; // La fuerza de lanzamiento

    private GameObject currentHook;
    private FishingManager fishingManager; // Referencia al FishingManager

    void Start()
    {
        // Obtener la referencia al FishingManager
        fishingManager = FindObjectOfType<FishingManager>();
    }

    void Update()
    {
        // Lanza el anzuelo con el click izquierdo del mouse
        if (Input.GetMouseButtonDown(0) && currentHook == null)
        {
            // Verificar si el inventario está lleno
            if (fishingManager != null && fishingManager.CaughtFishCount >= fishingManager.MaxFishLimit)
            {
                // Mostrar mensaje de inventario lleno
                StartCoroutine(fishingManager.ShowFullInventoryMessage());
                return; // Salir si el inventario está lleno
            }
            CastHook();
        }

        // Recoge el anzuelo con click derecho
        if (Input.GetMouseButtonDown(1) && currentHook != null)
        {
            RetrieveHook();
        }
    }

    void CastHook()
    {
        currentHook = Instantiate(hookPrefab, hookOrigin.position, Quaternion.identity);
        Rigidbody rb = currentHook.GetComponent<Rigidbody>();
        rb.AddForce(hookOrigin.forward * castForce, ForceMode.Impulse);
    }

    void RetrieveHook()
    {
        Destroy(currentHook); // Recoge el anzuelo al destruirlo
        currentHook = null;
    }
}
