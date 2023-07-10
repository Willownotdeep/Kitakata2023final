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
        //一つ目の蔵登場以降に生成する
        if (GameOverseer.score > KuraConstants.FOURTH)
        {
            Generate(span);
            //制限時間を超過したら停止
            if (GameOverseer.isOver) span = 1000000f;

        }
    }
}
