using UnityEngine;

public class ExitBotton : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
