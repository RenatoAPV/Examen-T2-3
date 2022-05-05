using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala2Controller : MonoBehaviour
{
    public float Velocity = 10;
    private PlayerController _playerController;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(Velocity, _rb.velocity.y);
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
