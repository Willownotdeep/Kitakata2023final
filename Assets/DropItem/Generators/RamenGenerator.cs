using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamenGenerator : StoneGenerator
{
    private static float span;
    private static float freq = 2.0f;

    void Start()
    {
        span = GameOverseer.dropItemSpan - freq;
        Generate(-10000.0f); //最初に一つ生成
    }

    //時間経過による生成頻度の更新
    public float UpdateSpan(float freq)
    {
        float newSpan = freq - GameOverseer.dropItemSpan;
        if (newSpan <= DropItemConstants.MINSPAN) newSpan = DropItemConstants.MINSPAN;

        return newSpan;
    }

    void Update()
    {
        Generate(span);
        span = UpdateSpan(freq);
        
        //制限時間を超過したら停止
        if (GameOverseer.isOver) span = 1000000f;
    }
}
