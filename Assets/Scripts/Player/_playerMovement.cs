using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class _playerMovement : MonoBehaviour
{
    [Header("Movimento do player")]
    [Tooltip("Velocidade de movimento do jogador")][SerializeField] private float _velocidadeMovimento = 5f;
    [Tooltip("Força do pulo do jogador")][SerializeField] private float _forcaPulo = 10f;
    [Tooltip("Corpo rigido do jogador (global para interação com outros scripts)")][SerializeField] private Rigidbody2D _rb;

    [Header("Validação de estado (chão)")]
    [SerializeField] private bool _estaNoChao;
    [SerializeField] private LayerMask _Chao;

    // Váriaveis internas
    private Vector2 _movimentoXY;
    private SpriteRenderer _spriteJogador;
    [SerializeField] private Animator _animacao;
    private LayerMask _Chao;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteJogador = GetComponent<SpriteRenderer>();
        _animacao = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimenta o jogador em duas direções distintas sem afetar a sprite na vertical.
        _movimentoXY = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_estaNoChao && Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movimentoXY * _velocidadeMovimento;
        _spriteJogador.flipX = (_movimentoXY.x != 0 && _movimentoXY.x < 0) ? true : false;
    }
    void Jump()
    {
        _rb.AddForce(Vector2.up * _forcaPulo, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_Chao))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    _estaNoChao = true;
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
            _estaNoChao = noChao;
        }
    }
        private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_Chao))
            _estaNoChao = false;
    }

}

    
