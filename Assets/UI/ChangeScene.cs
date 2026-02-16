using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingImage;

    public void ChangeToScene(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        loadingImage.SetActive(true);

        yield return null; // Å© Ç±ÇÍÇì¸ÇÍÇÈÇæÇØ

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);

        while (!op.isDone)
            yield return null;
    }

}
