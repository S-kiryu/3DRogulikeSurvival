using UnityEngine;

/// <summary>
///UI‚ð•\Ž¦‚·‚éƒNƒ‰ƒX
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;

    public void ShowGameOverUI()
    {
        _gameOverUI.SetActive(true);
    }
}
