using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security;

public class UImanager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _RestatText;
    [SerializeField] private Image _Livesimage;
    [SerializeField] private Sprite[] _lifeSprites;
    [SerializeField] private Button _restartButton;

    private GameManager _gameManager;
    private Player _player;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _restartButton.gameObject.SetActive(false);

        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }



    public void UpdateLifes(int currentLife)
    {
        _Livesimage.sprite = _lifeSprites[currentLife];

        if(currentLife == 0)
        {
            /*_RestatText.gameObject.SetActive(true);*/
            _restartButton.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
            _gameManager.GameOver();
        }
    }

    IEnumerator GameOverFlicker()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
 
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        _gameManager._pauseMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShootButton()
    {
        _player.FireLaser();
    }

    public void PauseButton()
    {
        _gameManager.PauseMenu();
    }
}
