using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float JumpForce = 10;
    public float Velocity = 10;
    public GameObject BulletPrefab;
    public GameObject BulletPrefab2;


    private bool vivo = true;
    private int vidas = 3;
    private int enemigos_falt = 5;
    private float tiempo_pres = 0f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;

    public Text Vidas_texto;
    public Text Enemigos_faltantes;


    private static readonly string ANIMATOR_STATE = "Estado";
    private static readonly int ANIMATOR_IDLE = 0;
    private static readonly int ANIMATOR_RUN = 1;
    private static readonly int ANIMATOR_JUMP = 2;
    private static readonly int ANIMATION_SLIDE = 3;
    private static readonly int ANIMATION_SHOOT = 4;
    private static readonly int ANIMATION_RUN_SHOOT = 5;
    private static readonly int ANIMATION_DEAD = 6;

    private static readonly int Right = 1;
    private static readonly int Left = -1;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Vidas_texto.text = "Vidas: " + vidas;
        Enemigos_faltantes.text = "Enemigos " + enemigos_falt;


        _rb.velocity = new Vector3(0, _rb.velocity.y);
        if (_rb.velocity == new Vector2(0,0) && vivo == true)
        {
            ChangeAnimation(ANIMATOR_IDLE);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Desplazarse(Right);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Desplazarse(Left);

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            ChangeAnimation(ANIMATOR_JUMP);

        }
        if (Input.GetKey(KeyCode.C))
        {
            Deslizarse();
        }
        balas();
        /*if (Input.GetKeyUp(KeyCode.X) && Input.GetKey(KeyCode.LeftArrow))
        {
            DispararCorriendo();
        }
        if (Input.GetKeyUp(KeyCode.X) && Input.GetKey(KeyCode.RightArrow))
        {
            DispararCorriendo();
        }*/
            if (vivo == false)
        {
            Muerte();
        }
    }
    private void Deslizarse()
    {
        ChangeAnimation(ANIMATION_SLIDE);
    }

    private void Desplazarse(int position)
    {
        _rb.velocity = new Vector2(Velocity * position, _rb.velocity.y);
        _sr.flipX = position == Left;
        ChangeAnimation(ANIMATOR_RUN);
    }
    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE, animation);
    }
    private void Disparar()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var bulletGo = Instantiate(BulletPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bulletGo.GetComponent<BalaController>();
        controller.SetPlayerController(this);
        ChangeAnimation(ANIMATION_SHOOT);

        if (_sr.flipX)
        {
            controller.Velocity = controller.Velocity * -1;
        }
    }
    private void Disparar2()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var bulletGo = Instantiate(BulletPrefab2, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bulletGo.GetComponent<BalaController>();
        ChangeAnimation(ANIMATION_SHOOT);

        if (_sr.flipX)
        {
            controller.Velocity = controller.Velocity * -1;
        }
    }
    private void DispararCorriendo()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var bulletGo = Instantiate(BulletPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bulletGo.GetComponent<BalaController>();
        controller.SetPlayerController(this);

        if (_sr.flipX)
        {
            controller.Velocity = controller.Velocity * -1;
        }

    }
    private void Muerte()
    {
        _rb.velocity = new Vector2(0,0);
        ChangeAnimation(ANIMATION_DEAD);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            vidas -= 1;
            if(vidas == 0)
            {
                vivo = false;
                SceneManager.LoadScene("SampleScene");
            }
               
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Recogible")
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Scene2");
        }
    }
    public void EnemigosDest(int bajas)
    {
        enemigos_falt -= bajas;
    }
    private void balas()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            tiempo_pres = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            tiempo_pres = Time.time - tiempo_pres;

            if (tiempo_pres >= 0f)
            {
                ChangeAnimation(ANIMATION_SHOOT);
                Disparar();
            }
            if (tiempo_pres >= 2f)
            {
                ChangeAnimation(ANIMATION_SHOOT);
                Disparar2();
            }
        }
    }

}
