using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Play : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 5;

    private Animator _animator;
    private Vector2 _playerMovement = Vector2.zero;
    private static readonly int Direction = Animator.StringToHash("direction");
    private LevelManager _levelManager;
    private List<ComputerBehaviour> _computers;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _levelManager = FindFirstObjectByType<LevelManager>();
        _computers = new List<ComputerBehaviour>(FindObjectsOfType<ComputerBehaviour>());
    }

    // Update is called once per frame
    void Update()
    {
        if (_levelManager.IsGameOver || _computers.Any(c => c.IsMinigameShowing))
        {
            _playerMovement = Vector2.zero;
        }
        else
        {
            _playerMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;    
        }

        int animationId = 0;
        if (_playerMovement.x > 0.1f)
        {
            animationId = 1;
        }

        if (_playerMovement.x < -0.1f)
        {
            animationId = 2;
        }

        if (_playerMovement.y > 0.1f)
        {
            animationId = 3;
        }

        if (_playerMovement.y < -0.1f)
        {
            animationId = 4;
        }
        
        _animator.SetInteger(Direction,animationId);
    }

    private void FixedUpdate()
    {
        rb.velocity = _playerMovement;
        
    }
}
