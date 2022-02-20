using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed;
    public float AccelerationConstant;
    public float RotateSpeedConstant;
    public float ResistToStopTime;
    public float GravityScale;
    public float TurnCoefficient;

    private float _currentRotation;
    private Vector2 _currentVelocity;
    private bool _thrusting;

    public static PlayerController Instance {
        get
        {
            return _instance;
        }
    }

    private static PlayerController _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _currentVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player position
        Vector3 playerPos = transform.position;

        //Check if we are rotating
        if (!_currentRotation.Equals(0))
        {
            transform.Rotate(0, 0, _currentRotation * RotateSpeedConstant * Time.deltaTime);
        }

        //Check if we are thrusting
        if (_thrusting)
        {
            Vector2 rotation = MathHelper.DegreesToVector2(transform.rotation.eulerAngles.z);
            Vector2 controlledVelocity = (_currentVelocity.magnitude + AccelerationConstant) * rotation; //immediate turn
            Vector2 dragVelocity = _currentVelocity + (rotation * AccelerationConstant); //dragged turn
            _currentVelocity = (controlledVelocity - dragVelocity) * Mathf.Pow(TurnCoefficient, 2) + dragVelocity;
        }

        else
        {
            _currentVelocity -= _currentVelocity * (MaxSpeed * Time.deltaTime) / ResistToStopTime;
            //Apply gravity
            _currentVelocity.y -= GravityScale * 9.8f;
        }

        //Cap velocity
        _currentVelocity.x = Mathf.Clamp(_currentVelocity.x, -MaxSpeed, MaxSpeed);
        _currentVelocity.y = Mathf.Clamp(_currentVelocity.y, -MaxSpeed, MaxSpeed);

        //Apply velocity
        playerPos += (Vector3)_currentVelocity * Time.deltaTime;
        playerPos.z = 0;

        //Set new position
        transform.position = playerPos;
    }

    public void Thrust(bool thrusting)
    {
        _thrusting = thrusting;
    }

    public void Rotate(float rotationDirection)
    {
        _currentRotation = rotationDirection;
    }

    public void Fire()
    {

    }
}
