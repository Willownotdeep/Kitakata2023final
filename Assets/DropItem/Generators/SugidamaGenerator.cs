using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugidamaGenerator : StoneGenerator
{

    public static float span;
    private static float freq;

    // Start is called before the first frame update
    void Start()
    {
        freq = Random.Range(1.0f, 5.0f);
        span = GameOverseer.dropItemSpan - freq;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOverseer.score > KuraConstants.THIRD) Generate(span);
    }
}
