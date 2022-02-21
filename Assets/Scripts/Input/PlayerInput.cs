using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public bool IsPaused;

        private PlayerController _pc;

        void Start()
        {
            IsPaused = false;
            _pc = PlayerController.Instance;
        }

        public void OnTHRUST(InputValue value)
        {

            _pc.Thrust(!value.Get<float>().Equals(0));
        }

        public void OnROTATE(InputValue value)
        {
            float rotation = value.Get<float>();
            _pc.Rotate(rotation);
        }

        public void OnFIRE(InputValue value)
        {
            _pc.Fire();
        }

        public void OnPAUSE(InputValue value)
        {
            if (value.Get<bool>())
            {
                if (!IsPaused)
                {
                    //TODO: Add pause
                }
                else
                {
                    //TODO: Add unpause
                }
            }
        }
    }
}