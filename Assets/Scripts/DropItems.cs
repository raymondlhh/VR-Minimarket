using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    Transform cam;
    public float AngleToShowUI = 30.0f;

    // Update is called once per frame
    void Update()
    {

        if (cam.eulerAngles.x > AngleToShowUI && cam.eulerAngles.x < 90.0f)
        {
            
        }

    }
}
