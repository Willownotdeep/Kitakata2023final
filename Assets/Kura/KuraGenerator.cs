using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KuraConstants
{
    //蔵の更新スコア
    public const int FIRST = 0;
    public const int SECOND = 40000;
    public const int THIRD = 100000;
    public const int FOURTH = 400000;
    public const int FIFTH = 1000000;
}

public class KuraGenerator : MonoBehaviour
{
    public GameObject MiniKuraPrefab;
    public GameObject MinikuraLargePrefab;
    public GameObject SecondKuraPrefab;
    public GameObject SecondKuraLargePrefab;
    public GameObject LastKuraPrefab;
    public GameObject go;

    public static Vector3 door;
    public static bool call = false;
    public static int flag = 0; //蔵がアップグレードした回数、ゼロにセットしてください
    
    private bool rotate = false;
    private Vector3 position; //クリア後蔵表示の位置調整するのに使う
    

    void Start()
    {
        Debug.Log(door);
    }

    private void Update()
    {
        if(GameOverseer.score >= KuraConstants.FIRST && flag == 0 && Time.time >= Constants.STARTTIME)
        {
            changeKura(++flag);
        }

        else if(GameOverseer.score >= KuraConstants.SECOND && flag == 1)
        {
            Destroy(go);
            changeKura(++flag);
        }

        else if(GameOverseer.score >= KuraConstants.THIRD && flag == 2)
        {
            Destroy(go);
            changeKura(++flag);
        }

        else if (GameOverseer.score >= KuraConstants.FOURTH && flag == 3)
        {
            Destroy(go);
            changeKura(++flag);
        }

        else if (GameOverseer.score >= KuraConstants.FIFTH && flag == 4)
        {
            Destroy(go);
            changeKura(++flag);
        }

        if (rotate) { Rotation();  }
        if (call) KuraView();
    }

    private void changeKura(int kuraNumber)
    {

        GetComponent<AudioSource>().Play();
        if(kuraNumber == 1) go = Instantiate(MiniKuraPrefab);
        else if (kuraNumber == 2) go = Instantiate(MinikuraLargePrefab);
        else if (kuraNumber == 3) go = Instantiate(SecondKuraPrefab);
        else if (kuraNumber == 4) go = Instantiate(SecondKuraLargePrefab);
        else if (kuraNumber == 5) go = Instantiate(LastKuraPrefab);

        rotate = true;
        if (call) { call = false; rotate = false; }
    }

    public void Rotation()
    {
        go.transform.Translate(0, 0.5f, 0);
        go.transform.Rotate(0, -1.5f, 0);

        door = GameObject.Find("Door").transform.position;

        //最初と２つめの蔵はドアの位置が上にあるので下にずらして修正
        if (flag == 1 || flag == 2) door.y -= 25.0f;

        Debug.Log(go.transform.rotation.y);
        if (go.transform.rotation.y <= -1.0f)
        {
            rotate = false;
        }
    }

    public static void CallOn()
    {
        if(flag > 0) call = true;
    }
    //蔵の召還
    public void KuraView()
    {

        if (flag == 1) position = new Vector3(479.8f, 161.7f, 323.8f);
        if (flag == 2) position = new Vector3(479.8f, 155.3f, 322.1f);
        if (flag == 3) position = new Vector3(479.8f, 203.8f, 268.3f);
        if (flag == 4) position = new Vector3(479.8f, 211.7f, 207.6f);
        if (flag == 5) position = new Vector3(479.8f, 210.0f, 191.6f);

        changeKura(flag);
        go.transform.position = position;
        Debug.Log("Kuraview");
    }

  
}
