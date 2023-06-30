using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamenController : MonoBehaviour
{
    float fallSpeed = 0.001f;
    float accelaration = 1.001f;
    private int point = 800;

    private void Accelerate(float acer)
    {
        fallSpeed *= accelaration;
    }

    void Update()
    {
        Fall();
        Accelerate(accelaration);

        if (transform.position.y < -10.0f) Destroy(gameObject);
    }

    public void Fall()
    {
        transform.Translate(0, -fallSpeed, 0);
    }

    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameOverseer.UpdateScore(this.point);
            Destroy(gameObject);
        }


    }
}
