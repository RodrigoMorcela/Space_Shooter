using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    private Animator _animator;

    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("Enemy::_player is NULL");
        }

        _collider = this.GetComponent<PolygonCollider2D>();

        if(_collider == null)
        {
            Debug.LogError("Enemy::_collider is NULL");
        }

        _animator = this.GetComponent<Animator>();

        if(_animator == null)
        {
            Debug.LogError("Enemy::_animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7f)
        {

            float randomX = Random.Range(-8f, 8f);

            Vector3 randomPos = new Vector3(randomX, 9f, 0f);

            transform.position = randomPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            _player.OnHit();

            Explode();
        }

        if (other.CompareTag("Laser"))
        {

            _player.EnemyKilled();

            Destroy(other.gameObject);

            Explode();
        }
    }


    private void Explode()
    {
        _collider.enabled = false;

        _animator.SetTrigger("OnEnemyDeath");

        _speed = 0;

        Destroy(this.gameObject, 2.5f);
    }
}
