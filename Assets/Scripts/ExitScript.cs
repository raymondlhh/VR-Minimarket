using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Exit successfully");
        Application.Quit();
    }
}
