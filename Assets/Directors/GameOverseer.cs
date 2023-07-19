using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Constants
{
    public const float TIMELIMIT = 80;
    public const float STARTTIME = 20; //チュートリアル終了時間
    
    //アイテムの生成頻度の係数、大きくすると頻度の増加が大きくなる。
    public const float FREQCOEF = 0.0007f;
}

public class GameOverseer : MonoBehaviour
{
    GameObject timetxt;
    GameObject scoretxt;
    GameObject stoptxt;
    GameObject player;
    GameObject starttxt;

    public static float dropItemSpan = 10.0f; //落下物生成頻度の基準値
    public static float rightLimit;
    public static float leftLimit;
    public static float upLimit;
    public static float playerZ;

    float time; //制限時間
    public static int score = 0;
    public static bool isOver;
    public static bool isStart; //チュートリアル中か
   

    //スクリーンの端の座標を取得（透明な壁のx座標）
    private static void SetScreenLimit()
    {
        rightLimit = GameObject.Find("RightWallCube").transform.position.x;
        leftLimit = GameObject.Find("LeftWallCube").transform.position.x;
        upLimit  = GameObject.Find("CeillingCube").transform.position.y; 
        playerZ = GameObject.Find("player").transform.position.z;
    }

    //必要なオブジェクトを見つける
    private void FindObjects()
    {
        this.timetxt = GameObject.Find("Time");
        this.scoretxt = GameObject.Find("Score");
        this.stoptxt = GameObject.Find("StopText");
        this.starttxt = GameObject.Find("StartText");
    }

    //時間切れでシーン遷移
    private void ChangeScene()
    {
        SceneManager.LoadScene("ClearScene");
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
        if (Time.timeSinceLevelLoad > Constants.STARTTIME)
        {
            score += point;
            if (score <= 0) score = 0;
        }
    }

    //文字情報を更新する
    private void UpdateText()
    {
        if (Time.timeSinceLevelLoad < Constants.STARTTIME) this.scoretxt.GetComponent<Text>().text = "練習中！！";
        
        //elseはカンマ区切り表示、０のときなぜか表示されないので０の時は普通に表示
        else if (score == 0)this.scoretxt.GetComponent<Text>().text = "売上 " + score.ToString() + " 円";

        //更新されたスコアを反映する
        else this.scoretxt.GetComponent<Text>().text = "売上 " + string.Format("{0:#,#}", score) + " 円";

        //時間を更新して反映する
        time -= Time.deltaTime;
        if (time <= 0) time = 0;
        this.timetxt.GetComponent<Text>().text = "残り時間　" + time.ToString("F0");

    }

    private void Stop()
    {
        //終了テキストの表示、徐々にフォントを拡大
        this.stoptxt.GetComponent<Text>().text = "終了！";
        int fontsize = this.stoptxt.GetComponent<Text>().fontSize;
        if (fontsize < 100) this.stoptxt.GetComponent<Text>().fontSize += 1;

        //プレイヤーのコライダーをはがす
        player = GameObject.Find("player");
        player.GetComponent<BoxCollider>().enabled = false;


        if (Input.GetKey(KeyCode.Space)) ChangeScene();  
    }
    //チュートリアル
    private void Starting()
    {
        float startingTime = Constants.STARTTIME - Time.timeSinceLevelLoad;
        if (startingTime < 1.0f) this.starttxt.GetComponent<Text>().text = "スタート！";
        else this.starttxt.GetComponent<Text>().text = "スタートまで：" + startingTime.ToString("F0");
    }

    //Startの前に呼ばれる
    private void Awake()
    {
        time = Constants.TIMELIMIT;　//制限時間
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

        //開始６秒前から開始までの間Startingを呼ぶ
        if (Time.timeSinceLevelLoad >= (Constants.STARTTIME - 6) &&
            Time.timeSinceLevelLoad < Constants.STARTTIME) isStart = true;
        else if (Time.timeSinceLevelLoad >= (Constants.STARTTIME + 3.0f)) isStart = false;

        //isStart中ならStartingをよび、終わったら開始テキストを消す
        if (isStart) Starting();
        else this.starttxt.GetComponent<Text>().text = ""; 
             
        if (this.time <= 0) isOver = true;
        if (isOver) Stop();
        
        //１０秒ごとにアイテムの生成頻度の更新
        if ((int)time % 10 == 0) UpdateFrequency(time);

        //ゲームのリセット、タイトルに戻る(esc + Q)
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Q)) SceneManager.LoadScene(0);

        Debug.Log(Time.timeSinceLevelLoad);
    }
}
