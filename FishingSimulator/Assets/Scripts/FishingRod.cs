using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject hookPrefab;     // El prefab del anzuelo
    public Transform hookOrigin;      // El punto desde donde se lanza el anzuelo
    public float castForce = 10f;     // La fuerza de lanzamiento
    public GameObject fishingRodModel; // El modelo de la caña de pescar (se usará para mostrar/ocultar)

    private GameObject currentHook;
    private FishingManager fishingManager; // Referencia al FishingManager
    private bool isRodActive = true;       // Indica si la caña está activa o no

    void Start()
    {
        // Obtener la referencia al FishingManager
        fishingManager = FindObjectOfType<FishingManager>();
    }

    void Update()
    {
        // Activar o desactivar la caña de pescar con la tecla "C"
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleFishingRod();
        }

        // Si la caña no está activa, no se puede usar
        if (!isRodActive)
        {
            return;
        }

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

    // Método para lanzar el anzuelo
    void CastHook()
    {
        currentHook = Instantiate(hookPrefab, hookOrigin.position, Quaternion.identity);
        Rigidbody rb = currentHook.GetComponent<Rigidbody>();
        rb.AddForce(hookOrigin.forward * castForce, ForceMode.Impulse);
    }

    // Método para recoger el anzuelo
    void RetrieveHook()
    {
        Destroy(currentHook); // Recoge el anzuelo al destruirlo
        currentHook = null;
    }

    // Método para activar/desactivar la caña de pescar
    void ToggleFishingRod()
    {
        isRodActive = !isRodActive; // Cambia el estado de la caña
        fishingRodModel.SetActive(isRodActive); // Mostrar/ocultar el modelo de la caña

        if (!isRodActive && currentHook != null)
        {
            // Si desactivas la caña mientras el anzuelo está lanzado, destruye el anzuelo
            RetrieveHook();
        }
    }
}
