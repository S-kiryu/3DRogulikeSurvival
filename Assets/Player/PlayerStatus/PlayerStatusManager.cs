using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager Instance;

    [SerializeField] private StatusSettings _defaultSettings;
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

        Status = new PlayerRuntimeStatus(_defaultSettings);
    }

    //ŽŸ‚É•K—v‚Èexp‚ð•Ï‚¦‚é
    public int ExpToNextLevel => Status.Level * 10;
}
