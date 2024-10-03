using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcShop : MonoBehaviour
{
    public Button sellButton;
    public Button upgradeButton;
    public Button closeButton;
    public GameObject shopPanel;
    public GameObject player;
    private bool triggeringPlayer;
    private bool shopEnabled;

    FirstPersonMovement playerMovement;
    FirstPersonLook playerLook;

    // Referencia al FishingManager
    public FishingManager fishingManager;

    // Referencia al texto de monedas en la UI
    public Text currencyText;

    public int playerCoins = 0;

    void Start()
    {
        playerMovement = player.GetComponent<FirstPersonMovement>();
        playerLook = player.GetComponentInChildren<FirstPersonLook>();

        sellButton.onClick.AddListener(SellFish);
        upgradeButton.onClick.AddListener(Upgrade);
        closeButton.onClick.AddListener(CloseShop);

        // Actualizar el texto de la moneda al iniciar
        UpdateCurrencyText();
    }

    void Update()
    {
        if (triggeringPlayer && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    void ToggleShop()
    {
        shopEnabled = !shopEnabled;
        shopPanel.SetActive(shopEnabled);

        // Desactiva o activa los controles del jugador
        playerMovement.controlsEnabled = !shopEnabled;
        playerLook.controlsEnabled = !shopEnabled;

        // Activa o desactiva el cursor del mouse
        Cursor.lockState = shopEnabled ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = shopEnabled;
    }

    public void SellFish()
    {
        // Usar la lista de peces atrapados del FishingManager
        List<Fish> caughtFishList = fishingManager.caughtFish;

        int totalCoins = 0;

        foreach (Fish fish in caughtFishList)
        {
            if (fish.weight < 5f)
            {
                totalCoins += 50;
            }
            else
            {
                totalCoins += 100;
            }
        }

        playerCoins += totalCoins;
        caughtFishList.Clear(); // Vacía la lista de pescados en el FishingManager

        // Actualizar la UI de la lista de peces
        fishingManager.UpdateFishListUI();

        // Actualizar el texto de la moneda del jugador
        UpdateCurrencyText();

        Debug.Log("Vendiste todos los pescados y ganaste " + totalCoins + " monedas.");
    }

    public void Upgrade()
    {
        Debug.Log("Mejoraste algo.");
    }

    public void CloseShop()
    {
        ToggleShop();
    }

    // Actualiza el texto de las monedas en la UI
    void UpdateCurrencyText()
    {
        currencyText.text = "Money: " + playerCoins;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggeringPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggeringPlayer = false;
        }
    }
}
