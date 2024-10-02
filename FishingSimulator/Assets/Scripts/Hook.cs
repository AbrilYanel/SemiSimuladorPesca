using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Clase que representa un pez atrapado
[System.Serializable]
public class Fish
{
    public string name;
    public float weight;

    public Fish(string name, float weight)
    {
        this.name = name;
        this.weight = weight;
    }
}

public class Hook : MonoBehaviour
{
    private bool isInWater;
    public GameObject fishPrefab; // Prefab del pez
    private FishingManager fishingManager; // Referencia al FishingManager

    void Start()
    {
        // Buscar el FishingManager en la escena
        fishingManager = FindObjectOfType<FishingManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
            StartCoroutine(Fishing());
        }
    }

    IEnumerator Fishing()
    {
        yield return new WaitForSeconds(Random.Range(2, 5)); // Tiempo de espera aleatorio
        Debug.Log("¡Pez atrapado!");

        // Generar un pez con un nombre aleatorio y peso
        Fish newFish = GenerateRandomFish();

        // Spawnea un pez en la posición del anzuelo
        Instantiate(fishPrefab, transform.position, Quaternion.identity);

        // Notificar al FishingManager que se ha atrapado un pez
        fishingManager.CatchFish(newFish);
    }

    Fish GenerateRandomFish()
    {
        string[] fishNames = { "Trucha", "Salmón", "Carpa" };
        string randomName = fishNames[Random.Range(0, fishNames.Length)];
        float randomWeight = Random.Range(0.5f, 10.0f); // Peso aleatorio entre 0.5 y 10.0

        return new Fish(randomName, randomWeight);
    }
}

