using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{

    GameObject[] lines;
    [SerializeField] private float gameTime = 60;
    public float GameTime
    {
        get
        {
            return gameTime;
        }
    }

	// Use this for initialization
	void Start ()
    {
        lines = new GameObject[GameObject.FindGameObjectsWithTag("SpawnPoint").Length];
        for (int i = 1; i < lines.Length + 1; i++)
        {
            lines[i - 1] = GameObject.Find("HL_line" + i);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
		if (Input.GetButton("Cancel"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}

    public void GameOver()
    {
        int portalHP = (GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalController>().currHP + 9) / 10;
        for (int i = 0; i < portalHP; i++)
        {
            gameObject.GetComponent<CommunicationBehaviour>().SendDamageMessage();
        }
        SceneManager.LoadScene("GameOverScene");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

    public void HighlightLine(int lineNum)
    {
        var x = lines[lineNum].transform.position.x;
        var y = lines[lineNum].transform.position.y;
        lines[lineNum].transform.position = new Vector3(x, y, 3);
    }

    public void Dehighlight()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            var x = lines[i].transform.position.x;
            var y = lines[i].transform.position.y;
            lines[i].transform.position = new Vector3(x, y, -11);

        }
    }
}
