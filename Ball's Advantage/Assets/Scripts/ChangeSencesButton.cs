using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSencesButton : MonoBehaviour
{
    int nowIndex;
    Scene scene;
    Button button;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        nowIndex = scene.buildIndex;
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
        Debug.Log(nowIndex);
    }

    void ChangeScene()
    {
        Debug.Log("WTF");
        SceneManager.LoadScene(nowIndex + 1);
    }
}
