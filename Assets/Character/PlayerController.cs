using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class PlayerConstants
{
    public const float defSpeed = 0.1f;
}

public class PlayerController : MonoBehaviour
{
    float Speed = PlayerConstants.defSpeed;
    float speedLimit = 0.2f;

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
            transform.Translate(this.Speed, 0, 0);
            if (this.Speed < this.speedLimit) this.Speed *= 1.0005f;
        }


        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-this.Speed, 0, 0);
            if (this.Speed < this.speedLimit) this.Speed *= 1.0005f;
        }

        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.Speed = 0.002f;
        }
    }

    //ƒvƒŒƒCƒ„[‚Ì‘¬‚³‚ð‚à‚Æ‚É–ß‚·
    private void ResetSpeed()
    {
        this.Speed = PlayerConstants.defSpeed;
    }

    void Update()
    {
        Move();
        Debug.Log(gameObject.transform.position);
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) ResetSpeed();
    }
}
