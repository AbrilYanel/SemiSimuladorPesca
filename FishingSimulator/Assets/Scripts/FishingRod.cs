using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject hookPrefab;     // El prefab del anzuelo
    public Transform hookOrigin;      // El punto desde donde se lanza el anzuelo
    public float castForce = 10f;     // La fuerza de lanzamiento
    public GameObject fishingRodModel; // El modelo de la ca�a de pescar (se usar� para mostrar/ocultar)

    private GameObject currentHook;
    private FishingManager fishingManager; // Referencia al FishingManager
    private bool isRodActive = true;       // Indica si la ca�a est� activa o no

    void Start()
    {
        // Obtener la referencia al FishingManager
        fishingManager = FindObjectOfType<FishingManager>();
    }

    void Update()
    {
        // Activar o desactivar la ca�a de pescar con la tecla "C"
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleFishingRod();
        }

        // Si la ca�a no est� activa, no se puede usar
        if (!isRodActive)
        {
            return;
        }

        // Lanza el anzuelo con el click izquierdo del mouse
        if (Input.GetMouseButtonDown(0) && currentHook == null)
        {
            // Verificar si el inventario est� lleno
            if (fishingManager != null && fishingManager.CaughtFishCount >= fishingManager.MaxFishLimit)
            {
                // Mostrar mensaje de inventario lleno
                StartCoroutine(fishingManager.ShowFullInventoryMessage());
                return; // Salir si el inventario est� lleno
            }
            CastHook();
        }

        // Recoge el anzuelo con click derecho
        if (Input.GetMouseButtonDown(1) && currentHook != null)
        {
            RetrieveHook();
        }
    }

    // M�todo para lanzar el anzuelo
    void CastHook()
    {
        currentHook = Instantiate(hookPrefab, hookOrigin.position, Quaternion.identity);
        Rigidbody rb = currentHook.GetComponent<Rigidbody>();
        rb.AddForce(hookOrigin.forward * castForce, ForceMode.Impulse);
    }

    // M�todo para recoger el anzuelo
    void RetrieveHook()
    {
        Destroy(currentHook); // Recoge el anzuelo al destruirlo
        currentHook = null;
    }

    // M�todo para activar/desactivar la ca�a de pescar
    void ToggleFishingRod()
    {
        isRodActive = !isRodActive; // Cambia el estado de la ca�a
        fishingRodModel.SetActive(isRodActive); // Mostrar/ocultar el modelo de la ca�a

        if (!isRodActive && currentHook != null)
        {
            // Si desactivas la ca�a mientras el anzuelo est� lanzado, destruye el anzuelo
            RetrieveHook();
        }
    }
}
