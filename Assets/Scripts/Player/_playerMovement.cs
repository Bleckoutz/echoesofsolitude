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
    [SerializeField] private bool _pulou;
    [SerializeField] private LayerMask _Chao;
    public Transform _verificaChao;
    public float _raioVerificacao = 0.2f;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimenta o jogador em duas direções distintas sem afetar a sprite na vertical.
        movimentoX = Input.GetAxis("Horizontal");
        movimentoY = _estaNoChao ? Input.GetAxis("Vertical") : 0f;

        if (_estaNoChao && Input.GetKeyDown(KeyCode.Space))
        {
            _pulou = true;
            Jump();
        }
    }

    private void FixedUpdate()
    {
        bool estavaNoChao = _estaNoChao;
        VerificaSeChao();
        

        _rb.velocity = new Vector2(movimentoX * _velocidadeMovimento, !_pulou ? movimentoY * _velocidadeMovimento : _rb.velocity.y);
        _spriteJogador.flipX = (_rb.velocity.x != 0 && _rb.velocity.x < 0) ? true : false;
    }
    void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.W)) {
            Debug.Log("Entrou");
            _rb.AddForce(Vector2.up * _forcaPulo, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("ANTES: " + _estaNoChao);
        //_estaNoChao = IsInLayerMask(collision.gameObject, _Chao) ? Physics2D.OverlapCircle(_verificaChao.position, _raioVerificacao, _Chao) : false;
        //Debug.Log("Depois: " + _estaNoChao);
    }

    private void VerificaSeChao()
    {
        _estaNoChao = Physics2D.OverlapCircle(_verificaChao.position, _raioVerificacao, _Chao);
    }

    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return((1 << obj.layer) & mask) != 0;
    }
}

    
