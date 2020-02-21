using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    public void ChangeSceneTo2() {
        SceneManager.LoadScene("Beckinsale");
    }

    public void ChangeSceneTo1() {
        SceneManager.LoadScene("Addersfield");
    }
}
