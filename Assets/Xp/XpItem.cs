using UnityEngine;

public class XpItem : MonoBehaviour
{
    [SerializeField] private float lifetime = 30.0f;
    private float timer;

    private void OnEnable()
    {
        timer = lifetime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReturnToPool();
        }
    }

    //Xp‚ðƒv[ƒ‹‚É–ß‚·
    private void ReturnToPool()
    {
        XPpool.Instance.Return(gameObject);
    }
}
