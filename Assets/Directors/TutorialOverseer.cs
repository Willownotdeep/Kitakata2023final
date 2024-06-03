using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialOverseer : MonoBehaviour
{
    GameObject timetxt;
    GameObject scoretxt;
    GameObject stoptxt;
    GameObject player;

    public static float dropItemSpan = 10.0f; //�����������p�x�̊�l
    public static float rightLimit;
    public static float leftLimit;
    public static float upLimit;
    public static float playerZ;

    float time; //��������
    public static int score = 0;
    public static bool isOver;


    //�X�N���[���̒[�̍��W���擾�i�����ȕǂ�x���W�j
    private static void SetScreenLimit()
    {
        rightLimit = GameObject.Find("RightWallCube").transform.position.x;
        leftLimit = GameObject.Find("LeftWallCube").transform.position.x;
        upLimit = GameObject.Find("CeillingCube").transform.position.y;
        playerZ = GameObject.Find("player").transform.position.z;
    }

    //�K�v�ȃI�u�W�F�N�g��������
    private void FindObjects()
    {
        this.timetxt = GameObject.Find("Time");
        this.scoretxt = GameObject.Find("Score");
        this.stoptxt = GameObject.Find("StopText");
    }

    //���Ԑ؂�ŃV�[���J��
    private void ChangeScene()
    {
        time += Time.time;
        if (time > 4.0f) SceneManager.LoadScene("ClearScene");
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
        score += point;
        if (score <= 0) score = 0;
    }

    //���������X�V����
    private void UpdateText()
    {
        //�X�V���ꂽ�X�R�A�𔽉f����
        //else�̓J���}��؂�\���A�O�̂Ƃ��Ȃ����\������Ȃ��̂łO�̎��͕��ʂɕ\��
        if (score == 0) this.scoretxt.GetComponent<Text>().text = "���� " + score.ToString() + " �~";
        else this.scoretxt.GetComponent<Text>().text = "���� " + string.Format("{0:#,#}", score) + " �~";

        //���Ԃ��X�V���Ĕ��f����
        time -= Time.deltaTime;
        if (time <= 0) time = 0;
        this.timetxt.GetComponent<Text>().text = "�c�莞�ԁ@" + time.ToString("F0");

    }

    private void Stop()
    {
        player = GameObject.Find("player");
        player.GetComponent<BoxCollider>().enabled = false;
        int fontsize = this.stoptxt.GetComponent<Text>().fontSize;
        if (fontsize < 100) this.stoptxt.GetComponent<Text>().fontSize += 1;
    }

    //Start�̑O�ɌĂ΂��
    private void Awake()
    {
        time = Constants.TIMELIMIT;�@//��������
        isOver = false;
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
        //��������0�̂Ƃ��̏���
        if (this.time <= 0)
        {
            isOver = true;
            this.stoptxt.GetComponent<Text>().text = "�I���I";
            if (Input.GetKey(KeyCode.Space)) ChangeScene();
        }

        if (isOver) Stop();

        //�P�O�b���ƂɃA�C�e���̐����p�x�̍X�V
        if ((int)time % 10 == 0) UpdateFrequency(time);

        //�Q�[���̃��Z�b�g�A�^�C�g���ɖ߂�(esc + Q)
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Q)) SceneManager.LoadScene(0);

        Debug.Log("Accer: " + Input.acceleration);
    }
}
