using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOverseer : MonoBehaviour
{
    void Start()
    {
        Debug.Log(KuraGenerator.flag);
        KuraGenerator.CallOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) SceneManager.LoadScene(0);
    }
}
