using UnityEngine;
using System.Collections;

/// <summary>
/// BGMを一元管理するマネージャークラス
/// シングルトンパターンで実装
/// </summary>
public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeDuration = 1f; // フェード時間

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        // シングルトンの初期化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSourceの初期化
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    /// <summary>
    /// BGMをフェードイン付きで再生する
    /// </summary>
    public void PlayBGMWithFadeIn(AudioClip clip, float duration = -1, bool loop = true)
    {
        float targetDuration = duration < 0 ? fadeDuration : duration;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = 0f;
        audioSource.Play();

        FadeTo(1f, targetDuration);
    }

    /// <summary>
    /// BGMを停止する
    /// </summary>
    public void StopBGM()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        audioSource.Stop();
        audioSource.volume = 1f;
    }

    /// <summary>
    /// BGMをフェードアウト付きで停止する
    /// </summary>
    public void StopBGMWithFadeOut(float duration = -1)
    {
        float targetDuration = duration < 0 ? fadeDuration : duration;
        fadeCoroutine = StartCoroutine(FadeOutAndStop(targetDuration));
    }

    /// <summary>
    /// BGMを一時停止する
    /// </summary>
    public void PauseBGM()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// BGMを再開する
    /// </summary>
    public void ResumeBGM()
    {
        audioSource.Play();
    }

    /// <summary>
    /// 現在のBGMが再生中かどうか
    /// </summary>
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    /// <summary>
    /// BGMの音量を設定する（0～1）
    /// </summary>
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// 現在の音量を取得する
    /// </summary>
    public float GetVolume()
    {
        return audioSource.volume;
    }

    /// <summary>
    /// 指定の音量にフェードする
    /// </summary>
    public void FadeTo(float targetVolume, float duration = -1)
    {
        float targetDuration = duration < 0 ? fadeDuration : duration;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeCoroutine(targetVolume, targetDuration));
    }

    /// <summary>
    /// フェード処理のコルーチン
    /// </summary>
    private IEnumerator FadeCoroutine(float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    /// <summary>
    /// フェードアウトして停止するコルーチン
    /// </summary>
    private IEnumerator FadeOutAndStop(float duration)
    {
        yield return StartCoroutine(FadeCoroutine(0f, duration));
        audioSource.Stop();
        audioSource.volume = 1f;
    }
}