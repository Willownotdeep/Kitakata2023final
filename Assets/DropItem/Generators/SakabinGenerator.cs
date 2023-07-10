using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakabinGenerator : StoneGenerator
{
    private static float span;
    private static float freq = 2.0f;

    public static bool isActivate;

    private int cnt = 0;

    void Start()
    {
        span = GameOverseer.dropItemSpan - freq;
        isActivate = false;
    }

    //ŠÔŒo‰ß‚É‚æ‚é¶¬•p“x‚ÌXV
    public float UpdateSpan(float freq)
    {
        float newSpan = GameOverseer.dropItemSpan - freq * 0.8f;
        if (newSpan <= DropItemConstants.MINSPAN) newSpan = DropItemConstants.MINSPAN;

        return newSpan;
    }

    void Update()
    {
        //3‚Â–Ú‚Ì‘ “oêˆÈ~‚É¶¬‚·‚é
        if (GameOverseer.score > KuraConstants.THIRD)
        {
            
                       
            if (isActivate)
            {
                span = 0.1f;
                cnt++;
                if (cnt >= 300) isActivate = false;
            }

            else span = UpdateSpan(freq);
            Generate(span);

            //§ŒÀŠÔ‚ğ’´‰ß‚µ‚½‚ç’â~
            if (GameOverseer.isOver) span = 1000000f;

        }
    }
}
