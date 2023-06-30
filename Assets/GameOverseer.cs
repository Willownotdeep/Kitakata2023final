using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Constants
{
    public const float TIMELIMIT = 120;
    
    //アイテムの生成頻度の係数、大きくすると頻度の増加が大きくなる。
    public const float FREQCOEF = 0.00006f;
}

public class GameOverseer : MonoBehaviour
{
    GameObject player;
    GameObject timetxt;
    GameObject scoretxt;
    //GameObject rightWall;
    //GameObject leftWall;

    public static float dropItemSpan = 10.0f; //落下物生成頻度の基準値
    public static float rightLimit;
    public static float leftLimit;
    public static float upLimit;

    public static int score = 0;
    float time = Constants.TIMELIMIT;　//制限時間

    //スクリーンの端の座標を取得（透明な壁のx座標）
    private static void SetScreenLimit()
    {
        rightLimit = GameObject.Find("RightWallCube").transform.position.x;
        leftLimit = GameObject.Find("LeftWallCube").transform.position.x;
        upLimit  = GameObject.Find("CeillingCube").transform.position.y; 
    }

    //必要なオブジェクトを見つける
    private void FindObjects()
    {
        this.player = GameObject.Find("player");
        this.timetxt = GameObject.Find("Time");
        this.scoretxt = GameObject.Find("Score");
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
        Debug.Log("Point, Score: " + point + "," + score);
        score += point;
        if (score <= 0) score = 0;
    }

    //文字情報を更新する
    private void UpdateText()
    {
        //更新されたスコアを反映する
        this.scoretxt.GetComponent<Text>().text = score.ToString();

        //時間を更新して反映する
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
        //アイテムの生成頻度の更新
        if ((int)time % 10 == 0) UpdateFrequency(time);
    }
}
