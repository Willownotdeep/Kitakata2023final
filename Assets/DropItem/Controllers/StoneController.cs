using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    float fallSpeed = DropItemConstants.STONESPEED;
    private int point = DropItemConstants.STONEPOINT;

    private void Update()
    {
        Fall();
        if (transform.position.y < 3.0f) Destroy(gameObject);
    }

    private void Fall()
    {
        transform.Translate(0, -fallSpeed, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameOverseer.UpdateScore(this.point);
            PlayerController.isHit = true;
            Destroy(gameObject);
        }


    }
}
