using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakabinController : RamenController
{ 
    public bool isGot = false; //�擾������true
    public Vector3 toDoor;
    
    void Start()
    {
        this.accelaration = DropItemConstants.SAKABINACC;
        this.fallSpeed = DropItemConstants.SKABINSPEED;
        this.point = DropItemConstants.SAKABINPOINT;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGot)
        {
            Move(toDoor);
        }

        else
        {
            Fall();
            Accelerate(accelaration);
        }

        if (transform.position.y < -10.0f) Destroy(gameObject);
    }

    public Vector3 CalcVector()
    {
        Vector3 toDoor = KuraGenerator.door - this.transform.position;
        toDoor /= toDoor.magnitude * 1.2f; //�P�ʃx�N�g���𐶐��i�W���ő��x�𒲐��j

        return toDoor;
    }

    //���̃h�A�܂ňړ�
    public void Move(Vector3 toDoor)
    {
        if((KuraGenerator.door - transform.position).magnitude < 20.0f ||
            this.transform.position.z > 230f)
        {
            isGot = false;
            GameOverseer.UpdateScore(this.point);

            GameObject go = GameObject.Find("ParticleCube");
            go.transform.position = this.transform.position;

            go.GetComponent<ParticleSystem>().Play();
            go.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

        transform.Translate(toDoor,Space.World);
        Rotate();
    }
    
    // ��]�A�j���[�V����
    public void Rotate()
    {
        this.transform.Rotate(0, 0, 30.0f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isGot = true;
            toDoor = CalcVector(); //�h�A�Ɍ������x�N�g���̌v�Z
        }
    }
}
