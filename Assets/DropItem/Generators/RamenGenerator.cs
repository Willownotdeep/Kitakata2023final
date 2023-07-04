using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamenGenerator : StoneGenerator
{
    private static float span;
    private static float freq = 5.0f;

    void Start()
    {
        span = GameOverseer.dropItemSpan - freq;
        Generate(-10000.0f);
    }

    //時間経過による生成頻度の更新
    public float UpdateSpan(float freq)
    {
        float newSpan = GameOverseer.dropItemSpan - freq;
        if (newSpan <= 0.7f) newSpan = 0.7f;

        return newSpan;
    }

    void Update()
    {
        Generate(span);
        span = UpdateSpan(freq);
    }
}
