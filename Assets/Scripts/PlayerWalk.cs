using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public int WalkSpeed;
    public bool OnWalk = true;
    public float AngleToStartWalk = 30.0f;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam = GetComponentInChildren<Camera>().transform;

        //if (Input.GetButtonDown("Fire2") && OnWalk)
        if (cam.eulerAngles.x > AngleToStartWalk && cam.eulerAngles.x < 90.0f)
        {
            transform.position = transform.position + Camera.main.transform.forward * WalkSpeed * Time.deltaTime;
        }

        //if (Input.GetButtonDown ("Jump"))
        //{
        //    transform.position = transform.position + Camera.main.transform.up * WalkSpeed * Time.deltaTime;
        //}
        
    }

    public void WalkFnOn()
    {
        OnWalk = true;
    }

    public void WalkFnOff()
    {
        OnWalk = false;
    }
}
