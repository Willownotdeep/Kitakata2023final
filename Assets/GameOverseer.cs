using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Constants
{
    public const float TIMELIMIT = 120;
    
    //�A�C�e���̐����p�x�̌W���A�傫������ƕp�x�̑������傫���Ȃ�B
    public const float FREQCOEF = 0.00006f;
}

public class GameOverseer : MonoBehaviour
{
    GameObject player;
    GameObject timetxt;
    GameObject scoretxt;
    //GameObject rightWall;
    //GameObject leftWall;

    public static float dropItemSpan = 10.0f; //�����������p�x�̊�l
    public static float rightLimit;
    public static float leftLimit;
    public static float upLimit;

    public static int score = 0;
    float time = Constants.TIMELIMIT;�@//��������

    //�X�N���[���̒[�̍��W���擾�i�����ȕǂ�x���W�j
    private static void SetScreenLimit()
    {
        rightLimit = GameObject.Find("RightWallCube").transform.position.x;
        leftLimit = GameObject.Find("LeftWallCube").transform.position.x;
        upLimit  = GameObject.Find("CeillingCube").transform.position.y; 
    }

    //�K�v�ȃI�u�W�F�N�g��������
    private void FindObjects()
    {
        this.player = GameObject.Find("player");
        this.timetxt = GameObject.Find("Time");
        this.scoretxt = GameObject.Find("Score");
    }

    //�������̕p�x�̌v�Z�A���Ԃ����ƕp�x�������Ȃ�B
    public static void UpdateFrequency(float time)
    {
        float freq = Constants.TIMELIMIT - time;
        dropItemSpan -= freq * Constants.FREQCOEF;
    }

    //�X�R�A�̌v�Z�ƍX�V
    public static void UpdateScore(int point)
    {
        Debug.Log("Point, Score: " + point + "," + score);
        score += point;
        if (score <= 0) score = 0;
    }

    //���������X�V����
    private void UpdateText()
    {
        //�X�V���ꂽ�X�R�A�𔽉f����
        this.scoretxt.GetComponent<Text>().text = score.ToString();

        //���Ԃ��X�V���Ĕ��f����
        time -= Time.deltaTime;
        this.timetxt.GetComponent<Text>().text = time.ToString("F0");

    }

    void Start()
    {
        FindObjects();
        SetScreenLimit();
        Debug.Log(rightLimit);
    }

    void Update()
    {
        UpdateText();
        //�A�C�e���̐����p�x�̍X�V
        if ((int)time % 10 == 0) UpdateFrequency(time);
    }
}
