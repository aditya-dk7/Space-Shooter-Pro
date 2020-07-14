using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartGameText;

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprite;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void UpdateScore(int playerScore = 0)
    {
       _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives = 3)
    {
        _LivesImg.sprite = _liveSprite[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
            
        }
    }

    public void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();

    }

    IEnumerator GameOverFlickerRoutine()
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
