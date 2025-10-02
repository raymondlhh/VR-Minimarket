using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public float degreePerSecond;
    private int RotateDirection = 1;
    //Color LightBlue = new Color(0.098f, 0.612f, 0.973f);
    Color OriginalColor;
    // Start is called before the first frame update
    void Start()
    {
        OriginalColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, RotateDirection * degreePerSecond * Time.deltaTime, 0);
    }

    public void Teleport()
    {
        Vector3 newPos;
        newPos = new Vector3(Random.Range(-5f,5f), Random.Range(0.5f, 2f), Random.Range(-5f, 5f));
        transform.position = newPos;
    }

    public void ChangeDirection()
    {
        RotateDirection = -1;
        degreePerSecond = degreePerSecond * 2;
        GetComponent<Renderer>().material.color = Color.yellow;
        
    }

    public void RevertDirection()
    {
        RotateDirection = 1;
        degreePerSecond = degreePerSecond / 2;
        GetComponent<Renderer>().material.color = OriginalColor;
    }
}
