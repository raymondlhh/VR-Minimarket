using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDescription : MonoBehaviour
{
    public GameObject uiCanvas; // Assign this in the inspector with your UI canvas
    public GameObject uiCanvas2;
    public GameObject itemPrefab; // Prefab of the item to spawn
    public float itemPrice; // Price of the item

    private void Start()
    {
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false); // Initially, the canvas should be hidden
        }

        if (uiCanvas2 != null)
        {
            uiCanvas2.SetActive(false); // Initially, the canvas should be hidden
        }
    }

    public void ShowUIDescription(bool show)
    {
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(show); // Set the active state of the canvas based on the show parameter
        }
    }

    public void ShowUICashier(bool show)
    {
        if (uiCanvas2 != null)
        {
            uiCanvas2.SetActive(show); // Set the active state of the canvas based on the show parameter
        }
    }

    // Call this method when you want to purchase the item
    public void PurchaseItem()
    {
        GameManager.Instance.AddItemToCart(itemPrefab, itemPrice);
    }

    
}
