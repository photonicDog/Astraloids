using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Thrust Settings")]
        public float MaxSpeed;
        public float Acceleration;

        [Header("Rotation Settings")]
        public float RotateSpeed;
        public float DecelerationTime;
        [Range(0f, 1f)]
        public float GravityScaleModifier;
        [Range(0f, 1f)]
        public float AirControlModifier;
        [Range(0f, 1f)]
        public float TurnSlowDuringThrustModifier;

        [Header("Collision Settings")]
        [Range(0f, 1f)]
        public float Bounciness;

        private float _currentRotation;
        private Vector2 _currentVelocity;
        private bool _thrusting;

        public static PlayerController Instance
        {
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
            float rotateSpeedThisFrame = RotateSpeed;

            //Check if we are thrusting
            if (_thrusting)
            {
                Vector2 rotation = MathHelper.DegreesToVector2(transform.rotation.eulerAngles.z);
                Vector2 controlledVelocity = (_currentVelocity.magnitude + Acceleration) * rotation; //immediate turn
                Vector2 dragVelocity = _currentVelocity + (rotation * Acceleration); //dragged turn
                _currentVelocity = (controlledVelocity - dragVelocity) * Mathf.Pow(AirControlModifier, 2) + dragVelocity;
                rotateSpeedThisFrame *= Mathf.Min(TurnSlowDuringThrustModifier / (_currentVelocity.magnitude / (float)MaxSpeed), 1);
            }
            else
            {
                //Apply air resistance
                _currentVelocity -= _currentVelocity * (MaxSpeed * Time.deltaTime) / DecelerationTime;
                //Apply gravity
                _currentVelocity.y -= GravityScaleModifier * 9.8f;
            }

            //Check if we are rotating
            if (!_currentRotation.Equals(0))
            {
                transform.Rotate(0, 0, _currentRotation * rotateSpeedThisFrame * Time.deltaTime);
            }

            //Cap velocity
            _currentVelocity = Vector2.ClampMagnitude(_currentVelocity, MaxSpeed);

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
}