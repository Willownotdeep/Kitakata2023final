using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenbeiController : RamenController
{
    // Start is called before the first frame update
    void Start()
    {
        this.accelaration = DropItemConstants.SENBEIACC;
        this.fallSpeed = DropItemConstants.SENBEISPEED;
        this.point = DropItemConstants.SENBEIPOINT;
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
        SenbeiRotate();
        if (transform.position.y < -10.0f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameOverseer.UpdateScore(point);

            //パーティクルのついたオブジェクトを移動させて再生
            GameObject go = GameObject.Find("ParticleCube");
            go.transform.position = this.transform.position;

            go.GetComponent<ParticleSystem>().Play();
            go.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

    }

    private void SenbeiRotate()
    {
        transform.Rotate(3.0f, 0, 1.0f, Space.World);
    }
}
