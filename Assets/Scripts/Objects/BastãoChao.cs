using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BastãoChao : MonoBehaviour
{
    [SerializeField] private _playerAttack _playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("_jogador"))
        {
           if(_playerAttack !=null)
                _playerAttack.enabled = true; // vai ativar o script
                                          
           


            gameObject.SetActive(false);// vai desativar a espada
        }
    }
}
