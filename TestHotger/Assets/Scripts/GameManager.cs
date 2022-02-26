using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string CountOfGamesKey = "CountOfGames";
    
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject walls;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private ButtonUp buttonUp;
    [SerializeField] private Text countOfGames;
    [SerializeField] private Text timeLastGame;

    private float _durationGame;
    private ObstacleSpawner _obstacleSpawner;
    private ParallaxWall _parallaxWall;
    private DifficultLevel _difficult;
    private IEnumerator _bonusSpeed;
    
    private void Awake()
    {
        _parallaxWall = walls.GetComponent<ParallaxWall>();
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }

    public void SetDifficulty(DifficultLevel difficult)
    {
        _difficult = difficult;
    }
    
    public void StartGame()
    {
        if (!_difficult)
        {
            return;
        }

        _durationGame = Time.time;
        
        PlayerPrefs.SetInt(CountOfGamesKey, PlayerPrefs.GetInt(CountOfGamesKey) + 1);
        
        var player = Instantiate(playerPrefab);
        
        walls.SetActive(true);
        buttonUp.SetReferences();
        _parallaxWall.SetDefaultValues();
        
        player.GetComponent<PlayerController>().DiePlayer += () =>
        {
            _parallaxWall.gameObject.SetActive(false);
            _obstacleSpawner.DestroyAllObstacles();
            losePanel.SetActive(true);
            gamePanel.SetActive(false);
            
            countOfGames.text = new StringBuilder($"Count of games: {PlayerPrefs.GetInt(CountOfGamesKey)}").ToString();
            timeLastGame.text = new StringBuilder($"Duration game: {(int)(Time.time - _durationGame)} seconds").ToString();
            
            StopCoroutine(_bonusSpeed);
        };
        
        losePanel.SetActive(false);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        
        _obstacleSpawner.StartSpawn(_difficult.intervalSpawnObstacles);
        
        _bonusSpeed = SetBonusSpeed(_difficult.speedIncreaseValue, _difficult.intervalSpeedRaise);
        StartCoroutine(_bonusSpeed);
    }
    
    private IEnumerator SetBonusSpeed(float speed, float intervalIncrease)
    {
        do
        {
            yield return new WaitForSeconds(intervalIncrease);
            _parallaxWall.AddSpeedParallax(speed);
            _obstacleSpawner.AddBonusSpeed(speed);
        } while (true);
    } 
}
