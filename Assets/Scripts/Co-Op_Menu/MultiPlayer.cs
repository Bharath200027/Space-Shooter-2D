using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlayer : MonoBehaviour
{

    public void LoadSinglePlayerGame()
    {
        Debug.Log("The Single Player mode is selected....");
        SceneManager.LoadScene("Single_Player");
    }

    public void LoadMultiPlayerGame()
    {
        Debug.Log("The Multi Player mode is selected....");
        SceneManager.LoadScene("Co-Op_Mode");
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
