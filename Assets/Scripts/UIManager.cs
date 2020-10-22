using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _sprites;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _scoreText.text = "Score: " + 0;

        if(_gameManager == null)
        {
            Debug.LogError("UIManager::_gameManager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _sprites[currentLives];
    }

    public void GameOver()
    {

        _gameManager.GameOver();

        _restartText.gameObject.SetActive(true);

        StartCoroutine(GameOverFlicker());
    }

    private IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";

            yield return new WaitForSeconds(0.5f);

            _gameOverText.text = "";

            yield return new WaitForSeconds(0.5f);

        }
    }

}
