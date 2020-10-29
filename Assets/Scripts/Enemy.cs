using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    private AudioSource _audioSourceExplosion;

    [SerializeField]
    private GameObject _laser;

    private Player _player;

    private Animator _animator;

    private Collider2D _collider;

    private float _canFire = -1f;

    private float _fireRate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();

        _audioSourceExplosion = GameObject.Find("Explosion_Source").GetComponent<AudioSource>();

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
        Fire();
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

    private void Fire()
    {
        
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(4f, 7f);

            _canFire = Time.time + _fireRate;

            GameObject enemyLaser = Instantiate(_laser,
                transform.position,
                Quaternion.identity
                );

            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            foreach( Laser laser in lasers ){

                laser.AssignEnemy();
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            _player.OnHit();

            Explode();
        }

        Laser laser = other.GetComponent<Laser>();

        if (other.CompareTag("Laser") && !laser.IsEnemyLaser())
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

        _audioSourceExplosion.Play();

        _speed = 0;

        Destroy(this.gameObject, 2.5f);
    }
}
