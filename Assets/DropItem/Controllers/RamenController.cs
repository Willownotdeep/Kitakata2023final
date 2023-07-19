using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamenController : MonoBehaviour
{
    public float fallSpeed;
    public float accelaration;
    public int point;
    
    private float rot;
    private float roty;
    private float rotz;

    public void Accelerate(float acer)
    {
        fallSpeed *= accelaration;
    }

   
    public void Fall()
    {
        transform.Translate(0, -fallSpeed, 0, Space.World);
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

    private void Start()
    {
        this.accelaration = DropItemConstants.RAMENACC;
        this.fallSpeed = DropItemConstants.RAMENSPEED;
        this.point = DropItemConstants.RAMENPOINT;
        
        rot = Random.Range(0.4f, 4.6f);
        roty = Random.Range(0.4f, 4.6f);
        rotz = Random.Range(0.4f, 4.6f);
    }

    private void Update()
    {
        Fall();
        RamenRotate();
        Accelerate(accelaration);

        if (transform.position.y < 0) Destroy(gameObject);
    }

    private void RamenRotate()
    {
        rot *= 1.0006f;
        transform.Rotate(-0.3f * rot, 0, 1.0f * rotz, Space.World);
    }

}
