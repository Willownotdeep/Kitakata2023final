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

    public static float dropItemSpan = 10.0f; //落下物生成頻度の基準値
    public static float rightLimit;
    public static float leftLimit;
    public static float upLimit;
    public static float playerZ;

    float time; //制限時間
    public static int score = 0;
    public static bool isOver;


    //スクリーンの端の座標を取得（透明な壁のx座標）
    private static void SetScreenLimit()
    {
        rightLimit = GameObject.Find("RightWallCube").transform.position.x;
        leftLimit = GameObject.Find("LeftWallCube").transform.position.x;
        upLimit = GameObject.Find("CeillingCube").transform.position.y;
        playerZ = GameObject.Find("player").transform.position.z;
    }

    //必要なオブジェクトを見つける
    private void FindObjects()
    {
        this.timetxt = GameObject.Find("Time");
        this.scoretxt = GameObject.Find("Score");
        this.stoptxt = GameObject.Find("StopText");
    }

    //時間切れでシーン遷移
    private void ChangeScene()
    {
        time += Time.time;
        if (time > 4.0f) SceneManager.LoadScene("ClearScene");
    }

    //落下物の頻度の計算、時間が立つと頻度が高くなる。
    public static void UpdateFrequency(float time)
    {
        float freq = Constants.TIMELIMIT - time;
        dropItemSpan -= freq * Constants.FREQCOEF;
    }

    //スコアの計算と更新
    public static void UpdateScore(int point)
    {
        score += point;
        if (score <= 0) score = 0;
    }

    //文字情報を更新する
    private void UpdateText()
    {
        //更新されたスコアを反映する
        //elseはカンマ区切り表示、０のときなぜか表示されないので０の時は普通に表示
        if (score == 0) this.scoretxt.GetComponent<Text>().text = "売上 " + score.ToString() + " 円";
        else this.scoretxt.GetComponent<Text>().text = "売上 " + string.Format("{0:#,#}", score) + " 円";

        //時間を更新して反映する
        time -= Time.deltaTime;
        if (time <= 0) time = 0;
        this.timetxt.GetComponent<Text>().text = "残り時間　" + time.ToString("F0");

    }

    private void Stop()
    {
        player = GameObject.Find("player");
        player.GetComponent<BoxCollider>().enabled = false;
        int fontsize = this.stoptxt.GetComponent<Text>().fontSize;
        if (fontsize < 100) this.stoptxt.GetComponent<Text>().fontSize += 1;
    }

    //Startの前に呼ばれる
    private void Awake()
    {
        time = Constants.TIMELIMIT;　//制限時間
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
        //制限時間0のときの処理
        if (this.time <= 0)
        {
            isOver = true;
            this.stoptxt.GetComponent<Text>().text = "終了！";
            if (Input.GetKey(KeyCode.Space)) ChangeScene();
        }

        if (isOver) Stop();

        //１０秒ごとにアイテムの生成頻度の更新
        if ((int)time % 10 == 0) UpdateFrequency(time);

        //ゲームのリセット、タイトルに戻る(esc + Q)
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Q)) SceneManager.LoadScene(0);

        Debug.Log("Accer: " + Input.acceleration);
    }
}
