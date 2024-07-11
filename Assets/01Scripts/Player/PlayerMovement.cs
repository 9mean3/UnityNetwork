using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam;
    public CinemachineVirtualCamera VirtualCamera => _vCam;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _lookSpeed;

    private PlayerInput _playerInput;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _playerInput.Move += MoveHandle;
        _playerInput.Look += LookHandle;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        _playerInput.Move -= MoveHandle;
        _playerInput.Look -= LookHandle;
    }

    private void MoveHandle(Vector2 dir)
    {
        Vector3 moveVec = dir * _moveSpeed * Time.deltaTime;
        _rigidbody.velocity = moveVec;
    }

    private void LookHandle(Vector2 dir)
    {
        Vector3 rotation = new Vector3(-dir.y, dir.x, 0) * _lookSpeed;
        _vCam.transform.eulerAngles += rotation;
    }
}
