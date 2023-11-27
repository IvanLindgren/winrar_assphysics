using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WinRar.Game
{
    public class PlayerEncyclopedia : MonoBehaviour
    {
        public Action OnDead;

        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private float speed;
        [SerializeField] private Animator _animator;
        [SerializeField] private Trigger _topTrigger;
        [SerializeField] private Trigger _bottomTrigger;

        private KeyCode[] _comb;
        private Queue<KeyCode> _keyQueue;
        private Queue<float> _timeStamps;

        private bool _isLeftLeg;

        public KeyCode NextKey { get; private set; }


        void Start()
        {
            _comb = new KeyCode[] { KeyCode.A, KeyCode.D };
            _keyQueue = new Queue<KeyCode>();
            _timeStamps = new Queue<float>();
            NextKey = _comb[0];

            _topTrigger.OnTriggerEnter += Trigger_OnTriggerEnter;
            _bottomTrigger.OnTriggerEnter += Trigger_OnTriggerEnter;

            _animator.Play("PlayerStopAnim");
        }

        void Update()
        {
            if (_inputSystem.KeyPressed != KeyCode.None)
            {
                if (_inputSystem.KeyPressed == NextKey) // Если игрок нажал правильную клавишу
                {
                    _keyQueue.Enqueue(_inputSystem.KeyPressed);
                    _timeStamps.Enqueue(Time.time);
                    while (_keyQueue.Count > _comb.Length)
                    {
                        _keyQueue.Dequeue();
                        _timeStamps.Dequeue();
                    }
                    if (IsMatch())
                    {
                        float timeDifference = _timeStamps.Max() - _timeStamps.Min();
                        float adjustedSpeed = speed / timeDifference; // Чем быстрее нажимаются клавиши, тем больше скорость
                        transform.Translate((Vector2.left * adjustedSpeed * Time.deltaTime), Space.World);

                        _isLeftLeg = !_isLeftLeg;

                        _keyQueue.Clear();
                        _timeStamps.Clear();
                    }
                    // Обновляем следующую клавишу
                    NextKey = _comb[_keyQueue.Count % _comb.Length];
                }
            }

            if (_inputSystem.Vertical > 0)
            {
                _topTrigger.gameObject.SetActive(true);
                _bottomTrigger.gameObject.SetActive(false);
                _animator.Play("PlayerCrouchAnim");
            }
            else if (_inputSystem.Vertical < 0)
            {
                _topTrigger.gameObject.SetActive(false);
                _bottomTrigger.gameObject.SetActive(true);
                _animator.Play("PlayerJumpAnim");
            }
            else
            {
                _topTrigger.gameObject.SetActive(true);
                _bottomTrigger.gameObject.SetActive(true);
                _animator.Play(_isLeftLeg ? "PlayerWalkAnim" : "PlayerWalkAnim2");
            }
        }

        private bool IsMatch()
        {
            if (_keyQueue.Count != _comb.Length)
                return false;

            return _keyQueue.ToArray().SequenceEqual(_comb);
        }

        private void Trigger_OnTriggerEnter(Collider2D col)
        {
            if (col.gameObject.CompareTag("Obstacle"))
            {
                OnDead?.Invoke();
            }
        }
    }
}