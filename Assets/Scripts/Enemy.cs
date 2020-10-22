using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();

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

            Vector3 randomPos = new Vector3(randomX, 7, 0);

            transform.position = randomPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            _player.OnHit();

            Destroy(this.gameObject);

        }

        if (other.CompareTag("Laser"))
        {

            _player.EnemyKilled();

            Destroy(other.gameObject);

            Destroy(this.gameObject);
        }
    }
}
