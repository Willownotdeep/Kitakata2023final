using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoGenerator : StoneGenerator
{
    private static float span;
    private static float freq = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       span = GameOverseer.dropItemSpan - freq;
    }

    // Update is called once per frame
    void Update()
    {
        //ˆê‚Â–Ú‚Ì‘ “oêˆÈ~‚É¶¬‚·‚é
        if (GameOverseer.score > KuraConstants.FOURTH)
        {
            Generate(span);
            //§ŒÀŠÔ‚ğ’´‰ß‚µ‚½‚ç’â~
            if (GameOverseer.isOver) span = 1000000f;

        }
    }
}
