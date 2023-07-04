using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakabinController : RamenController
{
    private float ascendSpeed = 0.01f; //�擾���̏㏸�A�j���[�V����

    private bool isGot = false; //�擾������true
    private Vector3 toDoor;
    
    void Start()
    {
        this.accelaration = 1.001f;
        this.fallSpeed = 0.0015f;
        this.point = DropItemConstants.HEALTHPOINT;
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

    private Vector3 CalcVector()
    {

        if(GameOverseer.score > 8000) Debug.Log("Before toDoor");
        Vector3 toDoor = KuraGenerator.door.transform.position - this.transform.position;
        if(GameOverseer.score >= 8000) Debug.Log("toDoor created");

        //�ŏ��̑������h�A�̍��W���኱��ɃY���Ă���̂ŏC������
        if (GameOverseer.score < 8000) toDoor.y -= 15.0f;

        toDoor /= (toDoor.magnitude * 10f); //�P�ʃx�N�g���𐶐�

        return toDoor;
    }

    /*private void Ascend()
    {
        transform.Translate(0, ascendSpeed, 0);
        ascendSpeed *= 0.86f;
    }*/

    private void Move(Vector3 toDoor)
    {
        if((KuraGenerator.door.transform.position - transform.position).magnitude < 20.0f)
        {
            isGot = false;
            GameOverseer.UpdateScore(this.point);
            Destroy(gameObject);
        }

        transform.Translate(toDoor);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(GameOverseer.score >= 8000) Debug.Log("Trigger entered");
        if (collision.gameObject.tag == "Player")
        {
            isGot = true;
            toDoor = CalcVector(); //�h�A�Ɍ������x�N�g���̌v�Z
        }
        if(GameOverseer.score >= 8000) Debug.Log("Ontrigger Passed");
    }
}
