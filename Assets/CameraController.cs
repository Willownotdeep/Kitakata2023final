using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.5f;
    public float radius = 6.0f;
    public GameObject kura;

    private Vector3 center;
    private Vector3 axis;
    private float period = 15.0f;
    private bool isFound = false;

    private void Start()
    {
      
    }
    void Update()
    {
        if (!isFound) FindObjects();
        this.transform.RotateAround(
            this.center,
            this.axis,
            (-360 / period * Time.deltaTime)
        );
    }

    private void FindObjects()
    {
        if(GameObject.FindGameObjectsWithTag("Kura") != null)
        {
            kura = GameObject.FindGameObjectWithTag("Kura");
            center = kura.transform.position;
            axis = Vector3.up;
            isFound = true;
        }
    }
}
