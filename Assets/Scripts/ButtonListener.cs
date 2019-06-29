using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour {

    public void setIP()
    {
        GameObject.Find("NetworkManager").GetComponent<NetworkManager>().networkAddress =
            GameObject.Find("InputField").GetComponent<InputField>().text;
        SceneManager.LoadScene("StartupScene");
    }

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
