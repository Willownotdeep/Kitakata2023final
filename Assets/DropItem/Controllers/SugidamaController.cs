using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugidamaController : MonoBehaviour
{
    private float fallspeed = 0.12f;
    private float startTime; 

    private void Fall()
    {
        transform.Translate(0, -fallspeed, 0);
    }

    private void Activate() { SakabinGenerator.isActivate = true; }
 


    void Start()
    {
        //スポーンした時点で次のスポーン停止
        SugidamaGenerator.span = 100000;
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
        //if (Time.time - startTime > 5.0f) DeActivate();
        if (transform.position.y < -10.0f)
        {
            SugidamaGenerator.span = 1.0f;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Activate();
            
            GameObject go = GameObject.Find("ParticleCube");
            go.transform.position = this.transform.position;

            go.GetComponent<ParticleSystem>().Play();
            go.GetComponent<AudioSource>().Play();

            Destroy(gameObject);

            //一度取られたら生成を止める
            SugidamaGenerator.span = 100000f;
        }
    }
}
