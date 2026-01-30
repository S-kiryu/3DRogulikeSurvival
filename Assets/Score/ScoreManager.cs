using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance {  get; private set; }

    //初期スコア
    [SerializeField] private int _score = 0;
    public int Score => _score;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //スコアを増やす
    public void ScoreUP(int point) 
    {
        _score += point;

        Debug.Log(point);
    }

}
