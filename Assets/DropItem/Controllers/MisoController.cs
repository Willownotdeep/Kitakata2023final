using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoController : SakabinController
{
    private float x = 1.0f;
    private float y = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.point = DropItemConstants.MISOPOINT;
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
            MisoFall();
            MisoRotate();
        }
        
        if (transform.position.y < -10.0f) Destroy(gameObject);
    }

    private void MisoFall()
    {
        transform.Translate(x, -y, 0, Space.World);

        //’[‚É’B‚µ‚½‚çŒü‚«‚ð•Ï‚¦‚é
        if (transform.position.x <= GameOverseer.leftLimit + 10.0f ||
            transform.position.x >= GameOverseer.rightLimit - 10.0f) x *= -1;
    }

    private void MisoRotate()
    {
        transform.Rotate(4.0f, 2.0f, 3.0f, Space.World);
    }

}
