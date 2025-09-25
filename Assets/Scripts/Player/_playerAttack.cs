using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _playerAttack : MonoBehaviour
{
    [SerializeField] private Animator _atackanima�ao;
    [SerializeField] private GameObject _hit;
    [SerializeField] private float _tempodohit = 0.5f;
    [SerializeField] private bool  _estaAtacando = false;


    _playerMovement _PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !_estaAtacando) //quando aperta q ele ataca
        {
            Atacar();   //chamando o m�todo
            Debug.Log("atacou.....");
        }
            
    }
    private void Atacar()
    {
        StartCoroutine(AtaqueCoroutine()); // chama o metodo

    }
    private IEnumerator AtaqueCoroutine()
    {
        _estaAtacando = true;

        _atackanima�ao.SetTrigger("Attack"); // toca a anima��o
        _hit.SetActive(true); // ativa hitbox

        yield return new WaitForSeconds(_tempodohit); // espera X segundos

        _hit.SetActive(false); // desliga hitbox
        _estaAtacando = false;
    }

}
