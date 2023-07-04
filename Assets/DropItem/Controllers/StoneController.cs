using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    float fallSpeed = 0.05f;
    private int point = DropItemConstants.STONEPOINT;

    public void Update()
    {
        Fall();
        if (transform.position.y < -7.0f) Destroy(gameObject);
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
