using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager Instance;

    [SerializeField] private StatusSettings defaultSettings;
    public PlayerRuntimeStatus Status;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Status = new PlayerRuntimeStatus(defaultSettings);
    }

    public int ExpToNextLevel => Status.Level * 10;
}
