using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameOver;

    private SpawnManager _spawnManager;

    private void Start()
    {

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    public void StartGame()
    {
        _spawnManager.StartSpawning();
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
