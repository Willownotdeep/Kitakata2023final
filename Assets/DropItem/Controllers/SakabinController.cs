using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakabinController : RamenController
{
    private float ascendSpeed = 0.01f; //取得時の上昇アニメーション

    private bool isGot = false; //取得されるとtrue
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

        //最初の蔵だけドアの座標が若干上にズレているので修正する
        if (GameOverseer.score < 8000) toDoor.y -= 15.0f;

        toDoor /= (toDoor.magnitude * 10f); //単位ベクトルを生成

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
            toDoor = CalcVector(); //ドアに向かうベクトルの計算
        }
        if(GameOverseer.score >= 8000) Debug.Log("Ontrigger Passed");
    }
}
