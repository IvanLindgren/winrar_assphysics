using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WinRar.Game
{
    public class PlayerEncyclopedia : MonoBehaviour
    {
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private float speed;
        public KeyCode[] comb;
        public Queue<KeyCode> keyQueue;
        public Queue<float> timeStamps;
        public KeyCode nextKey; 

        // Start is called before the first frame update
        void Start()
        {
            comb = new KeyCode[] { KeyCode.A, KeyCode.D };
            keyQueue = new Queue<KeyCode>();
            timeStamps = new Queue<float>();
            nextKey = comb[0]; 
        }

        // Update is called once per frame
        void Update()
        {
            if (_inputSystem.KeyPressed != KeyCode.None)
            {
                if (_inputSystem.KeyPressed == nextKey) // Если игрок нажал правильную клавишу
                {
                    keyQueue.Enqueue(_inputSystem.KeyPressed);
                    timeStamps.Enqueue(Time.time);
                    while (keyQueue.Count > comb.Length)
                    {
                        keyQueue.Dequeue();
                        timeStamps.Dequeue();
                    }
                    if (IsMatch())
                    {
                        float timeDifference = timeStamps.Max() - timeStamps.Min();
                        float adjustedSpeed = speed / timeDifference; // Чем быстрее нажимаются клавиши, тем больше скорость
                        transform.Translate((Vector2.left * adjustedSpeed * Time.deltaTime), Space.World);
                        keyQueue.Clear();
                        timeStamps.Clear();
                    }
                    // Обновляем следующую клавишу
                    nextKey = comb[keyQueue.Count % comb.Length];
                }
            }
        }

        private bool IsMatch()
        {
            if (keyQueue.Count != comb.Length)
                return false;

            return keyQueue.ToArray().SequenceEqual(comb);
        }
    }
}
