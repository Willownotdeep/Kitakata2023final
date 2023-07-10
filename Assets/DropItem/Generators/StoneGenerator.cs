using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : MonoBehaviour
{
    public GameObject Prefab;


    private static float span;
    private static float freq = 6.0f;
    public float delta = 10.0f;

    //ƒ‰ƒ“ƒ_ƒ€‚ÈˆÊ’u‚É¶¬
    public void Generate(float span)
    {
        this.delta += Time.deltaTime;
        if (this.delta > span)
        {
            this.delta = 0;
            GameObject go = Instantiate(Prefab);

            float px = Random.Range(GameOverseer.rightLimit - 30.0f, GameOverseer.leftLimit + 30.0f);
            go.transform.position = new Vector3(px, GameOverseer.upLimit, GameOverseer.playerZ);
        }
    }

    

    private void Start()
    {
        span = GameOverseer.dropItemSpan - freq;
    }

    private void Update()
    {
        Generate(span);

        //§ŒÀŠÔ‚ğ’´‰ß‚µ‚½‚ç’â~
        if (GameOverseer.isOver) span = 1000000f;
    }
}
