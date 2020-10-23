using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private GameObject _explosion;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _rotationSpeed = 20f;
        
    }

    // Update is called once per frame
    void Update()
    {

        Rotate();
        
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            Explode();
        }

    }

    private void Explode()
    {
        
        GameObject explosion = Instantiate(_explosion,
            this.transform.position,
            Quaternion.identity
            );

        Destroy(explosion, 3);

        _gameManager.StartGame();

        Destroy(this.gameObject, 0.15f);

    }
}
