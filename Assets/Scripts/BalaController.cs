using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public float Velocity = 10;
    private PlayerController _playerController;
    private Rigidbody2D _rb;
    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
    }

    void Update()
    {
        _rb.velocity = new Vector2(Velocity, _rb.velocity.y);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        Debug.Log(tag);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        Debug.Log(tag);
        if (tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
