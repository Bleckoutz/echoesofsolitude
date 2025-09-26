using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class _playerMovement : MonoBehaviour
{
    [Header("Movimento do player")]
    [Tooltip("Velocidade de movimento do jogador")][SerializeField] private float _velocidadeMovimento = 2f;
    [Tooltip("Força do pulo do jogador")][SerializeField] private float _forcaPulo = 10f;
    [Tooltip("Corpo rigido do jogador (global para interação com outros scripts)")][SerializeField] private Rigidbody2D _rb;

    [Header("Validação de estado (chão)")]
    [SerializeField] private bool _estaNoChao;
    [SerializeField] private bool _pulou;
    [SerializeField] private LayerMask _Chao;
    public Transform _verificaChao;
    public float _raioVerificacao = 0.2f;

    [Header("Limite de movimentação vertical com W")]
    [SerializeField] private float _alturaMaximaMovimento = 5f;
    private float _alturaInicial;

    // Váriaveis internas
    private float movimentoX, movimentoY;
    private SpriteRenderer _spriteJogador;
    //[SerializeField] private Animator _animacao;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteJogador = GetComponent<SpriteRenderer>();
        //_animacao = GetComponent<Animator>();
    }

    private void Start()
    {
        _alturaInicial = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        movimentoX = Input.GetAxis("Horizontal");

        if (_estaNoChao)
        {
            float _alturaAtual = transform.position.y;
            bool _podeSubir = _alturaAtual < _alturaInicial + _alturaMaximaMovimento;

            movimentoY = _podeSubir ? Input.GetAxis("Vertical") : 0f;

            if (Input.GetKeyDown(KeyCode.Space) && movimentoY == 0)
                _pulou = true;

            if (Input.GetButtonDown("Vertical") && Input.GetKeyDown(KeyCode.W))
                _rb.gravityScale = 0f;
            else if(Input.GetButtonDown("Vertical") && Input.GetKeyDown(KeyCode.S))
                _rb.gravityScale = 1f;

        }
        else
        {
            movimentoY = 0f;
        }
    }

    private void FixedUpdate()
    {
        VerificaSeChao();

        if (_estaNoChao)
        {
            if (!_pulou)
            {
                _rb.velocity = new Vector2(movimentoX * _velocidadeMovimento, movimentoY * _velocidadeMovimento);
            }
        }
        else
        {
            _rb.velocity = new Vector2(movimentoX * _velocidadeMovimento, _rb.velocity.y);
        }

        if (_pulou)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * _forcaPulo, ForceMode2D.Impulse);
            _pulou = false;
        }

        FlipSprite();
    }

    private void FlipSprite()
    {
        _spriteJogador.flipX = movimentoX < 0 ? true : false;
    }

    private void VerificaSeChao()
    {
        _estaNoChao = Physics2D.OverlapCircle(_verificaChao.position, _raioVerificacao, _Chao);
        
    }
}


