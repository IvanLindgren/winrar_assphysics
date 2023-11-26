using UnityEngine;

namespace WinRar.Game
{
    public class InputSystem : MonoBehaviour
    {
        public float Vertical { get; private set; }
        public float Horizontal { get; private set; }

        void Update()
        {
            Vertical = Input.GetAxis("Vertical");
            Horizontal = Input.GetAxis("Horizontal");
        }
    }
}

