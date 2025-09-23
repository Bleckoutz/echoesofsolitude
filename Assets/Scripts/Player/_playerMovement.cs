using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class _playerMovement : MonoBehaviour
{
    [Header("Movimento do player")]
    [Tooltip("Controla o movimento horizontal e pulo do jogador")]
    [SerializeField] private float _velocidadeMovimento = 5f;
    [SerializeField] private float _forcaPulo = 10f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _movimentoHorizontal;
    [SerializeField] private float _movimentoVertical;
    [SerializeField] private bool _estaNoChao;
    [SerializeField] private SpriteRenderer _spriteJogador;
    [SerializeField] public Animator _animator;
    [SerializeField] private string _Chao;
    [SerializeField] public bool _isGround;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteJogador = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        _movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        _movimentoVertical = Input.GetAxisRaw("Vertical");
        if (_estaNoChao && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movimentoHorizontal * _velocidadeMovimento, _movimentoVertical * _velocidadeMovimento);
        if (_velocidadeMovimento > 0) _spriteJogador.flipX = false;
        else if (_velocidadeMovimento < 0) _spriteJogador.flipX = true;
    }
    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _forcaPulo);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_Chao))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    _isGround = true;
                    break;
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_Chao))
        {
            bool noChao = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    noChao = true;
                    break;
                }
            }
            _isGround = noChao;
        }
    }
        private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_Chao))
            _isGround = false;
    }

}

    
