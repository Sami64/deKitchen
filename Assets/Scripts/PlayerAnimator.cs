using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    
    private Animator _animator;
    [SerializeField] private Player _player;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(IS_WALKING, _player.IsWalking());
    }

    void Update()
    {
        _animator.SetBool(IS_WALKING, _player.IsWalking());
    }
}
