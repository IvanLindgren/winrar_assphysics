using System;
using UnityEngine;

namespace WinRar.Game
{
    public class Player : MonoBehaviour
    {
        public Action OnDead;

        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _topLayerY;
        [SerializeField] private float _bottomLayerY;
        [SerializeField] private Transform _cameraArm;
        [SerializeField] private float _speedBoost = 5.0f;

        private bool _isStopped = false;

        public int SpeedBoostersCount { get; private set; }
        public Vector3 CameraArm => _cameraArm.position;

        void Update()
        {
            if (_isStopped)
                return;

            HorizontalMove();
        }

        public void Spawn(Vector3 spawnPoint)
        {
            transform.position = spawnPoint;
            SpeedBoostersCount = 0;
            _isStopped = false;
        }

        public void Stop()
        {
            _isStopped = true;
        }

        private void HorizontalMove()
        {
            float currentSpeed = _speed + SpeedBoostersCount * _speedBoost;
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        }

        public void ObstacleTriggered()
        {
            _isStopped = true;
            OnDead?.Invoke();
        }
        public void BoosterUpTriggered() {
            _speed += _speedBoost;
            SpeedBoostersCount++; 
        }
        public void BoosterDownTriggered() {
            _speed -= _speedBoost;
            SpeedBoostersCount--; 
        }
        public void MoveToTopLayerTriggered()
        {
            if (_inputSystem.Vertical > 0)
            {
                var pos = transform.position;
                pos.y = _topLayerY;
                transform.position = pos;
            }
        }
        public void MoveToBottomLayerTriggered()
        {
            if (_inputSystem.Vertical < 0)
            {
                var pos = transform.position;
                pos.y = _bottomLayerY;
                transform.position = pos;
            }
        }
    }
}