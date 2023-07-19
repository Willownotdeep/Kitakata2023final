using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class PlayerConstants
{
    public const float defSpeed = 0.8f;
    public const int AUTOSTART = 0; 
    public const int AUTOEND = 0; //‚±‚±‚ðƒ[ƒ‚É‚·‚ê‚ÎƒIƒt
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
            UnableAuto();
            transform.Translate(this.Speed, 0, 0);
        }

        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            AutoLeft = true;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            UnableAuto();
            transform.Translate(-this.Speed, 0, 0);
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            AutoRight = true;
        }

        else if (AutoLeft)
        {
            if (++AutoCnt > PlayerConstants.AUTOSTART && AutoCnt < PlayerConstants.AUTOEND) transform.Translate(-this.Speed * 0.2f, 0, 0);
            if (++AutoCnt > PlayerConstants.AUTOEND) { UnableAuto(); }
        }

        else if (AutoRight)
        {
            if (++AutoCnt > PlayerConstants.AUTOSTART && AutoCnt < PlayerConstants.AUTOEND) transform.Translate(this.Speed * 0.2f, 0, 0);
            if (AutoCnt > PlayerConstants.AUTOEND) { UnableAuto(); }
        }

        Debug.Log(AutoCnt);




    }

    private void UnableAuto()
    {
        AutoCnt = 0;
        AutoRight = false;
        AutoLeft = false;
    }

    void Update()
    {
        Move();
        Debug.Log(cnt);
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
        cnt = 0;
        isHit = false;
    }


}

