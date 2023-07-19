using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoGenerator : StoneGenerator
{
    private static float span;
    private static float freq = 1.0f;

    void Start()
    {
       span = GameOverseer.dropItemSpan - freq;
    }

    void Update()
    {
        //ˆê‚Â–Ú‚Ì‘ “oêˆÈ~‚É¶¬‚·‚é
        if (GameOverseer.score > KuraConstants.FOURTH)
        {
            Generate(span);

        }
    }
}
