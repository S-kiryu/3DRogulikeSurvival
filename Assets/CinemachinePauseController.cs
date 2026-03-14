using Unity.Cinemachine;
using UnityEngine;

public class CinemachinePauseController : MonoBehaviour
{
    private CinemachineBrain brain;

    void Awake()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        Time.timeScale = LevelUpManager.Instance.IsPaused ? 0f : 1f;
    }
}
