using UnityEngine;

namespace WinRar.Game
{
    public class InputSystem : MonoBehaviour
    {
        public float Vertical { get; private set; }
        public float Horizontal { get; private set; }

        public KeyCode KeyPressed { get; private set; } //для уровня энциклопедия
        void Update()
        {
            Vertical = Input.GetAxis("Vertical");
            Horizontal = Input.GetAxis("Horizontal");

            if (Input.anyKeyDown)
            {
                KeyPressed = FetchKey();
            }
        }

        KeyCode FetchKey()
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                    return keyCode;
            }
            return KeyCode.None;
        }
    }
}

