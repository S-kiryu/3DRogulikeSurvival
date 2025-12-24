using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private UIManager _uiManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _playerStatus.OnGameClear += GameClear;
        _playerStatus.OnDead += GameOver;
    }

    private void GameClear()
    {
        _uiManager.ShowGameClearUI();
    }

    private void GameOver()
    {
       _uiManager.ShowGameOverUI();
    }
}
