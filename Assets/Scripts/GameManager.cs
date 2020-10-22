using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameOver;

    private void Start()
    {
        _isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }
        
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
