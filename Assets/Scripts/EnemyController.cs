using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float Velocity = 10;
    private Rigidbody2D _rb;
    private int enemigos_faltantes = 5;
    private SpriteRenderer _sr;
    private int cont1 = 0;
    private int cont2 = 0;


    private PlayerController _playerController;
    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Enemigos.text = "Enemigos faltantes: " + enemigos_faltantes;
        if (_sr.flipX == true)
        {
            _rb.velocity = new Vector2(Velocity * -1, _rb.velocity.y);
        }
        if (_sr.flipX == false)
        {
            _rb.velocity = new Vector2(Velocity * 1, _rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;

        if (tag == "Limite2")
        {
            _sr.flipX = true;
        }
        if (tag == "Limite")
        {
            _sr.flipX = false;
        }
        if (tag == "Bala")
        {
            cont1++;
            if (cont1 == 3)
            {
                Destroy(this.gameObject);
                _playerController.EnemigosDest(1);
            }
        }
        if (tag == "Bala2")
        {
            cont2 = cont2 +2;
            if (cont2 >=3)
            {
                Destroy(this.gameObject);
                _playerController.EnemigosDest(1);
            }
        }
    }
}
