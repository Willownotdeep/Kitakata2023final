using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuraGenerator : MonoBehaviour
{
    public GameObject MiniKura;
    public GameObject SecondKuraPrefab;
    public GameObject LastKuraPrefab;
    public GameObject go;

    public static GameObject door;
    public static GameObject door2;
    public static GameObject door3;

    private Vector3 position; //���̐����ʒu�A�ŏ��̑��Ɠ����ʒu�ɒu��
    private int flag = 0; //�����A�b�v�O���[�h�̉�
    

    void Start()
    {
        go = GameObject.Find("mini_kura");
        door = GameObject.Find("Door");
        position = go.transform.position;
        Debug.Log(door.transform.position);
    }

    private void Update()
    {
        if(GameOverseer.score >= 8000 && flag == 0)
        {
            changeKura(2);
            flag++;
        }

        if(GameOverseer.score >= 18000 && flag == 1)
        {
            changeKura(3);
            flag++;
        }
    }

    private void changeKura(int kuraNumber)
    {
        Destroy(door);
        Destroy(go);
        

        if (kuraNumber == 2)
        {
            go = Instantiate(SecondKuraPrefab);
            door2 = GameObject.Find("Door2");
        }

        else if (kuraNumber == 3)
        {
            go = Instantiate(LastKuraPrefab);
            //go.transform.position = position;
        }

        
        Debug.Log(door.transform.position);
    }


  
}
