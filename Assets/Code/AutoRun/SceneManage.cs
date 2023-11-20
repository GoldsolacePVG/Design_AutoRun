using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void SceneManagement(int sceneValue) {
        SceneManager.LoadScene(sceneValue);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
