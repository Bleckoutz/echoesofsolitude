using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _playerAttack : MonoBehaviour
{
    _playerMovement _PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rig = _PlayerMovement.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
