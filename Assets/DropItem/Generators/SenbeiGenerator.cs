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
        //��ڂ̑��o��ȍ~�ɐ�������
        if (GameOverseer.score > KuraConstants.SECOND)
        {
            Generate(span);
            span = UpdateSpan(freq);
            //�������Ԃ𒴉߂������~
            if (GameOverseer.isOver) span = 1000000f;

        }
    }
}
