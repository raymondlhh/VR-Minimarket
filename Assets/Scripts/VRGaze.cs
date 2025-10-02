using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRGaze : MonoBehaviour
{
    public Image imgGaze;
    public float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;
    public int distanceOfRay = 10;
    private RaycastHit _hit;
    private Transform lastGazedObject;

    public GameObject mySpaces; // Assign this in Unity Editor
    public GameObject cashierSpaces; // Assign this in Unity Editor


    void Start()
    {
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }

    void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            if (imgGaze.fillAmount == 1 && _hit.transform.CompareTag("Teleport"))
            {
                _hit.transform.gameObject.GetComponent<Teleport>().TeleportPlayer();
            }
            else if (imgGaze.fillAmount == 1 && _hit.transform.CompareTag("Items"))
            {
                if (lastGazedObject == null || lastGazedObject != _hit.transform)
                {
                    // Hide the previous item's description
                    if (lastGazedObject != null)
                    {
                        lastGazedObject.gameObject.GetComponent<ShowDescription>().ShowUIDescription(false);
                    }

                    // Show new item's description
                    _hit.transform.gameObject.GetComponent<ShowDescription>().ShowUIDescription(true);
                    lastGazedObject = _hit.transform; // Update the last gazed object
                }
            }
            else if (imgGaze.fillAmount == 1 && _hit.transform.CompareTag("CheckOut"))
            {
                if (lastGazedObject == null || lastGazedObject != _hit.transform)
                {
                    // Hide the previous item's description
                    if (lastGazedObject != null)
                    {
                        lastGazedObject.gameObject.GetComponent<ShowDescription>().ShowUIDescription(false);
                    }

                    // Toggle visibility of spaces
                    mySpaces.SetActive(false); // Hide mySpaces
                    cashierSpaces.SetActive(true); // Show cashierSpaces

                    // Show new item's description
                    _hit.transform.gameObject.GetComponent<ShowDescription>().ShowUIDescription(true);

                    lastGazedObject = _hit.transform; // Update the last gazed object
                }
            }
            else if (imgGaze.fillAmount == 1 && _hit.transform.CompareTag("Exit"))
            {    
                Debug.Log("Exit successfully");
                Application.Quit();       
            }
        }
        else if (lastGazedObject != null)
        {
            lastGazedObject.gameObject.GetComponent<ShowDescription>().ShowUIDescription(false);
            lastGazedObject = null; // Reset the last gazed object since gaze is not on any item

            // Reset visibility of spaces
            mySpaces.SetActive(true); // Show mySpaces
            cashierSpaces.SetActive(false); // Hide cashierSpaces
        }
    }

    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROFF()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;

        // Hide the UI when gazing stops
        if (lastGazedObject != null)
        {
            lastGazedObject.gameObject.GetComponent<ShowDescription>().ShowUIDescription(false);
            lastGazedObject.gameObject.GetComponent<ShowDescription>().ShowUICashier(false);
            lastGazedObject = null;
        }

        // Reset visibility of spaces to default
        mySpaces.SetActive(true); // Show mySpaces
        cashierSpaces.SetActive(false); // Hide cashierSpaces
    }
}