using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakabinGenerator : StoneGenerator
{
    private static float span;
    private static float freq = 8.0f;

    void Start()
    {
        span = GameOverseer.dropItemSpan - freq;
    }

    //���Ԍo�߂ɂ�鐶���p�x�̍X�V
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
