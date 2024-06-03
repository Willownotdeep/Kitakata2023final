using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenbeiGenerator : SakabinGenerator
{
    private static float span;
    private static float freq = 5.0f;

    void Start()
    {
        span = GameOverseer.dropItemSpan - freq;
    }

    void Update()
    {
        //“ñ‚Â–Ú‚Ì‘ “oêˆÈ~‚É¶¬‚·‚é
        if (GameOverseer.score > KuraConstants.SECOND)
        {
            Generate(span);
            span = UpdateSpan(freq);
            //§ŒÀŠÔ‚ğ’´‰ß‚µ‚½‚ç’â~
            if (GameOverseer.isOver) span = 1000000f;

        }
    }
}
