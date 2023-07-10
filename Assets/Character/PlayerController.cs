using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class PlayerConstants
{
    public const float defSpeed = 0.8f;
}

public class PlayerController : MonoBehaviour
{
    float Speed = PlayerConstants.defSpeed;


    public static bool isHit = false;
    private bool AutoLeft = false;
    private bool AutoRight = false;
    private int cnt = 0;
    private int AutoCnt = 0;
    private void Move()
    {
        if (this.transform.position.x > GameOverseer.rightLimit)
        {
            transform.position = new Vector3(GameOverseer.leftLimit, transform.position.y, transform.position.z);
        }

        else if (this.transform.position.x < GameOverseer.leftLimit)
        {
            transform.position = new Vector3(GameOverseer.rightLimit, transform.position.y, transform.position.z);
        }


        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //AutoRight = false;
            transform.Translate(this.Speed, 0, 0);
        }

        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //AutoLeft = true;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //AutoLeft = false;
            transform.Translate(-this.Speed, 0, 0);
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //AutoRight = true;
        }

        else if (AutoLeft)
        {
            if (++AutoCnt > 30) transform.Translate(-this.Speed * 0.2f, 0, 0);
            if (++AutoCnt < 100) transform.Translate(-this.Speed * 0, 0, 0);
        }

        else if (AutoRight)
        {
            if (++AutoCnt > 30) transform.Translate(this.Speed * 0.2f, 0, 0);
            if (++AutoCnt < 100) transform.Translate(this.Speed * 0, 0, 0);
        }



    }

    //ƒvƒŒƒCƒ„[‚Ì‘¬‚³‚ð‚à‚Æ‚É–ß‚·
    /*private void ResetSpeed()
    {
        this.Speed = PlayerConstants.defSpeed;
    }*/

    void Update()
    {
        Move();
        if (isHit)
        {
            HitAnimation();
            cnt++;
            if (cnt >= 100) Normalize();
        }
    } 
    public static void Hit()
    {
        isHit = true;
    }

    private void HitAnimation()
    {
       this.GetComponent<Renderer>().material.color = Color.gray;
    }

    private void Normalize()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
        isHit = false;
    }


}

