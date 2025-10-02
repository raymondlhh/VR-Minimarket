using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Singleton instance


    public Transform[] mySpaces; // Assign cart space transforms here in the inspector
    public Transform[] cashierSpaces; // Assign cart space transforms here in the inspector
    public int totalItems = 0;
    public float totalPrice = 0.0f;


    public Text totalItemsText; // Reference to the UI text displaying total items
    public Text totalPriceText; // Reference to the UI text displaying total price
    public GameObject fullMessageCanvas; // Canvas that shows the "full" message


    private void Awake()
    {
        // Ensure there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adds an item to the cart and updates the UI accordingly
    public void AddItemToCart(GameObject itemPrefab, float price)
    {
        for (int i = 0; i < mySpaces.Length; i++)
        {
            if (mySpaces[i].childCount == 0 && cashierSpaces[i].childCount == 0) // Ensure both spaces are empty
            {
                GameObject newItemMySpace = Instantiate(itemPrefab, mySpaces[i].position, Quaternion.identity, mySpaces[i]);
                GameObject newItemCashierSpace = Instantiate(itemPrefab, cashierSpaces[i].position, Quaternion.identity, cashierSpaces[i]);
                totalItems++;
                totalPrice += newItemMySpace.GetComponent<ItemDetails>().itemPrice; // Assume price is the same for both, add only once
                UpdateUI();
                return;
            }
        }
        ShowFullMessage();
    }

    public void DropItem()
    {
        for (int i = mySpaces.Length - 1; i >= 0; i--)
        {
            if (mySpaces[i].childCount > 0 && cashierSpaces[i].childCount > 0) // Check both spaces
            {
                GameObject itemToRemoveMySpace = mySpaces[i].GetChild(0).gameObject;
                GameObject itemToRemoveCashierSpace = cashierSpaces[i].GetChild(0).gameObject;
                totalPrice -= itemToRemoveMySpace.GetComponent<ItemDetails>().itemPrice; // Subtract price once
                Destroy(itemToRemoveMySpace);
                Destroy(itemToRemoveCashierSpace);
                totalItems--;
                UpdateUI();
                return;
            }
        }
    }

    public void ResetCart()
    {
        for (int i = 0; i < mySpaces.Length; i++)
        {
            foreach (Transform item in mySpaces[i])
                Destroy(item.gameObject);

            foreach (Transform item in cashierSpaces[i])
                Destroy(item.gameObject);
        }
        totalItems = 0;
        totalPrice = 0.0f;
        UpdateUI();
    }

    // Update the UI elements to reflect current cart status
    void UpdateUI()
    {
        if (totalItemsText != null)
            totalItemsText.text = "Total Items: " + totalItems;

        if (totalPriceText != null)
            totalPriceText.text = "Total Price: RM" + totalPrice.ToString("0.00");
    }

    private void ShowFullMessage()
    {
        StartCoroutine(BlinkFullMessage());
    }

    private IEnumerator BlinkFullMessage()
    {
        float endTime = Time.time + 5f; // End the blinking after 5 seconds
        bool isVisible = true;

        while (Time.time < endTime)
        {
            fullMessageCanvas.SetActive(isVisible);
            isVisible = !isVisible; // Toggle visibility
            yield return new WaitForSeconds(0.5f); // Wait for half a second before toggling again
        }

        fullMessageCanvas.SetActive(false); // Ensure it's hidden after blinking ends
    }
}