using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamenController : MonoBehaviour
{
    public float fallSpeed;
    public float accelaration;
    public int point;

    public void Accelerate(float acer)
    {
        fallSpeed *= accelaration;
    }

   
    public void Fall()
    {
        transform.Translate(0, -fallSpeed, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameOverseer.UpdateScore(point);
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        this.accelaration = 1.001f;
        this.fallSpeed = 0.0015f;
        this.point = DropItemConstants.RAMENPOINT;
    }

    private void Update()
    {
        Fall();
        Accelerate(accelaration);

        if (transform.position.y < -10.0f) Destroy(gameObject);
    }

}
