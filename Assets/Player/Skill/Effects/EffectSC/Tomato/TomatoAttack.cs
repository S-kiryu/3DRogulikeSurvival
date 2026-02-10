using UnityEngine;

public class TomatoAttack : MonoBehaviour
{
    private float _speed;
    private float _lifeTIme;
    private float _damage;
    private float _timer;

    public void ResetState()
    {
        _timer = 0f;
    }

    // 初期化メソッド
    public void Initialize(float damage, float speed, float lifeTime)
    {
        _damage = damage;
        _speed = speed;
        _lifeTIme = lifeTime;
        _timer = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        _timer += Time.deltaTime;
        if (_timer >= _lifeTIme)
        {
            ReturnToPool();
        }
    }

    // 衝突処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyStatus>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
                Debug.Log("トマトの攻撃がヒット！ダメージ: " + _damage);
            }
            ReturnToPool();
        }
    }

    // プールに戻すメソッド
    private void ReturnToPool()
    {
        TomatoPool.Instance.Return(gameObject);
    }

}
