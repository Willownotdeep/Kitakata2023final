using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Constants
{
    public const float TIMELIMIT = 80;
    public const float STARTTIME = 20; //�`���[�g���A���I������
    
    //�A�C�e���̐����p�x�̌W���A�傫������ƕp�x�̑������傫���Ȃ�B
    public const float FREQCOEF = 0.0007f;
}

public class GameOverseer : MonoBehaviour
{
    GameObject timetxt;
    GameObject scoretxt;
    GameObject stoptxt;
    GameObject player;
    GameObject starttxt;

    public static float dropItemSpan = 10.0f; //�����������p�x�̊�l
    public static float rightLimit;
    public static float leftLimit;
    public static float upLimit;
    public static float playerZ;

    float time; //��������
    public static int score = 0;
    public static bool isOver;
    public static bool isStart; //�`���[�g���A������
   

    //�X�N���[���̒[�̍��W���擾�i�����ȕǂ�x���W�j
    private static void SetScreenLimit()
    {
        rightLimit = GameObject.Find("RightWallCube").transform.position.x;
        leftLimit = GameObject.Find("LeftWallCube").transform.position.x;
        upLimit  = GameObject.Find("CeillingCube").transform.position.y; 
        playerZ = GameObject.Find("player").transform.position.z;
    }

    //�K�v�ȃI�u�W�F�N�g��������
    private void FindObjects()
    {
        this.timetxt = GameObject.Find("Time");
        this.scoretxt = GameObject.Find("Score");
        this.stoptxt = GameObject.Find("StopText");
        this.starttxt = GameObject.Find("StartText");
    }

    //���Ԑ؂�ŃV�[���J��
    private void ChangeScene()
    {
        SceneManager.LoadScene("ClearScene");
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
        if (Time.timeSinceLevelLoad > Constants.STARTTIME)
        {
            score += point;
            if (score <= 0) score = 0;
        }
    }

    //���������X�V����
    private void UpdateText()
    {
        if (Time.timeSinceLevelLoad < Constants.STARTTIME) this.scoretxt.GetComponent<Text>().text = "���K���I�I";
        
        //else�̓J���}��؂�\���A�O�̂Ƃ��Ȃ����\������Ȃ��̂łO�̎��͕��ʂɕ\��
        else if (score == 0)this.scoretxt.GetComponent<Text>().text = "���� " + score.ToString() + " �~";

        //�X�V���ꂽ�X�R�A�𔽉f����
        else this.scoretxt.GetComponent<Text>().text = "���� " + string.Format("{0:#,#}", score) + " �~";

        //���Ԃ��X�V���Ĕ��f����
        time -= Time.deltaTime;
        if (time <= 0) time = 0;
        this.timetxt.GetComponent<Text>().text = "�c�莞�ԁ@" + time.ToString("F0");

    }

    private void Stop()
    {
        //�I���e�L�X�g�̕\���A���X�Ƀt�H���g���g��
        this.stoptxt.GetComponent<Text>().text = "�I���I";
        int fontsize = this.stoptxt.GetComponent<Text>().fontSize;
        if (fontsize < 100) this.stoptxt.GetComponent<Text>().fontSize += 1;

        //�v���C���[�̃R���C�_�[���͂���
        player = GameObject.Find("player");
        player.GetComponent<BoxCollider>().enabled = false;


        if (Input.GetKey(KeyCode.Space)) ChangeScene();  
    }
    //�`���[�g���A��
    private void Starting()
    {
        float startingTime = Constants.STARTTIME - Time.timeSinceLevelLoad;
        if (startingTime < 1.0f) this.starttxt.GetComponent<Text>().text = "�X�^�[�g�I";
        else this.starttxt.GetComponent<Text>().text = "�X�^�[�g�܂ŁF" + startingTime.ToString("F0");
    }

    //Start�̑O�ɌĂ΂��
    private void Awake()
    {
        time = Constants.TIMELIMIT;�@//��������
        isOver = false;
        isStart = false;
        dropItemSpan = 10.0f;
        score = 0;

    }

    void Start()
    {
        FindObjects();
        SetScreenLimit();
    }

    void Update()
    {
        UpdateText();

        //�J�n�U�b�O����J�n�܂ł̊�Starting���Ă�
        if (Time.timeSinceLevelLoad >= (Constants.STARTTIME - 6) &&
            Time.timeSinceLevelLoad < Constants.STARTTIME) isStart = true;
        else if (Time.timeSinceLevelLoad >= (Constants.STARTTIME + 3.0f)) isStart = false;

        //isStart���Ȃ�Starting����сA�I�������J�n�e�L�X�g������
        if (isStart) Starting();
        else this.starttxt.GetComponent<Text>().text = ""; 
             
        if (this.time <= 0) isOver = true;
        if (isOver) Stop();
        
        //�P�O�b���ƂɃA�C�e���̐����p�x�̍X�V
        if ((int)time % 10 == 0) UpdateFrequency(time);

        //�Q�[���̃��Z�b�g�A�^�C�g���ɖ߂�(esc + Q)
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Q)) SceneManager.LoadScene(0);

        Debug.Log(Time.timeSinceLevelLoad);
    }
}
