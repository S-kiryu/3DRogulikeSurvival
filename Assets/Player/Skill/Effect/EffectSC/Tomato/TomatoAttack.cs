using UnityEngine;

public class TomatoAttack : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _lifeTIme = 10;
    [SerializeField] private float _damage = 10;
    private float _timer;

    private void OnEnable()
    {
        _timer = 0;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("何かにトマトが当たった");
        if(other.CompareTag("Enemy")) 
        {
            //ダメージ処理
            Debug.Log("トマトが当たった！！！");
            var Enemy = other.GetComponent<EnemyStatus>();
            Enemy.TakeDamage(_damage);
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        TomatoPool.Instance.Return(gameObject);
    }
}
