using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    int nowIndex;
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        nowIndex = scene.buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("trigger");
        ChangeScene();
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nowIndex + 1);
    }
}
