using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    public bool _isGameOver;

    [SerializeField]
    public bool _isCoopMode;


    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        Debug.Log("GameManager:: GameOver() called? ");
        _isGameOver = true;

    }
    

    public void Restart()
    {
        Debug.Log("The process is initiated and it is been utilized.");
    }
    public void CoopMode()
    {
        Debug.Log("GameManager:: CoopMode() called? ");
        _isCoopMode= true;
    }
}
