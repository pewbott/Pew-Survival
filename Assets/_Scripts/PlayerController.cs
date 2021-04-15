using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ControlType { PC, Android }
    [Header("Platform")]
    public ControlType _controlType;

    [Header("Movement")]
    public float _moveSpeed;
    public float _speedMultiplyVariable;

    float _horizontalAxis;
    float _verticalAxis;
    
    public float _rotateSpeed;

    private Vector3 _moveDirection;

    Transform _playerTransform;
    Rigidbody _playerRb;
    public float _rbDrag;

    public Joystick _joystick;

    private GameObject _joystickObject;

    public Transform _visiblePlayerTransform;

    public bool isMoving;

    private Animator _playerAnimator;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerRb.freezeRotation = true;
        _playerTransform = GetComponent<Transform>();
        _joystickObject = _joystick.gameObject;
        _playerAnimator = _visiblePlayerTransform.GetComponent<Animator>();
    }

    private void Update()
    {
        InputSwitcher();
        ControlDrag();
        AnimatorController();
    }

    void PCInputs()
    {
        _horizontalAxis = Input.GetAxisRaw("Horizontal");
        _verticalAxis = Input.GetAxisRaw("Vertical");

        _moveDirection = _playerTransform.forward * _verticalAxis + _playerTransform.right * _horizontalAxis;
    }

    void AndroidInputs()
    {
        _horizontalAxis = _joystick.Horizontal;
        _verticalAxis = _joystick.Vertical;

        _moveDirection = _playerTransform.forward * _verticalAxis + _playerTransform.right * _horizontalAxis;
    }

    void ControlDrag()
    {
        _playerRb.drag = _rbDrag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        if (_verticalAxis != 0 || _horizontalAxis != 0)
            isMoving = true;
        else
            isMoving = false;
      
        _playerRb.AddForce(_moveDirection.normalized * _moveSpeed * _speedMultiplyVariable, ForceMode.Acceleration);
    }

    void InputSwitcher()
    {
        if (_controlType == ControlType.PC)
        {
            _joystickObject.SetActive(false);
            PCInputs();
        }
        else if (_controlType == ControlType.Android)
        {
            _joystickObject.SetActive(true);
            AndroidInputs();
        }
    }

    void RotatePlayer()
    {
        if (_playerRb.velocity != Vector3.zero && isMoving)
        {
            Vector3 _dir = _playerTransform.position + _playerRb.velocity;

            _visiblePlayerTransform.LookAt(_dir);
        }
    }

    void AnimatorController()
    {
        if(isMoving)       
            _playerAnimator.SetBool("isRuning", true);
        
        else
            _playerAnimator.SetBool("isRuning", false);


    }
}
