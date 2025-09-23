using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        _movimentoVertical = Input.GetAxisRaw("Vertical");
        _rb.velocity = new Vector2(_movimentoHorizontal*_velocidadeMovimento, _movimentoVertical*_velocidadeMovimento);
        
    }
}
