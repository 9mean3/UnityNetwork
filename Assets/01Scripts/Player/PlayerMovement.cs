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

    private Vector2 _moveVector;
    private Vector2 _lookVector;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();

        //
        _playerInput.Move += MoveHandle;
        _playerInput.Look += LookHandle;
        //
    }

    private void Update()
    {
        Vector3 lookVec = new Vector3(-_lookVector.y, _lookVector.x, 0);
        lookVec *= _lookSpeed * Time.deltaTime;
        _vCam.transform.localEulerAngles += lookVec;
    }

    private void FixedUpdate()
    {
        Vector3 moveVec = _moveVector * _moveSpeed * Time.deltaTime;
        _rigidbody.velocity = moveVec;
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
        _moveVector = dir;
    }

    private void LookHandle(Vector2 dir)
    {
        _lookVector = dir;
    }
}
