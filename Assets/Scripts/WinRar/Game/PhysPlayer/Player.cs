using System;
using System.Collections;
using UnityEngine;

namespace WinRar.Game
{
    public class Player : MonoBehaviour
    {
        public Action OnDead;

        [SerializeField] private CameraController _camera;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _topLayerY;
        [SerializeField] private float _bottomLayerY;
        [SerializeField] private Transform _cameraArm;
        [SerializeField] private float _speedBoost = 5.0f;

        private bool _isStopped = false;

        public int SpeedBoostersCount { get; private set; }
        public Vector3 CameraArm => _cameraArm.position;
        public bool CanMoveToTopLayer { get; set; }
        public bool CanMoveToBottomLayer { get; set; }

        void Update()
        {
            if (_isStopped)
                return;

            HorizontalMove();

            if (CanMoveToTopLayer)
                MoveToTopLayerTriggered();
            if (CanMoveToBottomLayer)
                MoveToBottomLayerTriggered();
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

        public void PlayFinishAnim(Action callback, Action invokeEinstein, Vector3 target, Vector3 cameraTarget)
        {
            // if on top layer - move to bottom layer
            // var pos = transform.position;
            // pos.y = _bottomLayerY;
            // transform.position = pos;

            // come close to Einstein

            // watch Einstein anim
            invokeEinstein?.Invoke();

            // unfocus camera and go forward
            Stop();
            _camera.IsChasing = false;
            _camera.transform.position = cameraTarget;
            StartCoroutine(MoveToCoroutine(callback, target));
        }

        private void HorizontalMove()
        {
            float currentSpeed = _speed + SpeedBoostersCount * _speedBoost;
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        }

        public void ObstacleTriggered()
        {
            Stop();
            OnDead?.Invoke();
        }
        public void BoosterUpTriggered()
        {
            _speed += _speedBoost;
            SpeedBoostersCount++;
        }
        public void BoosterDownTriggered()
        {
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

        private IEnumerator MoveToCoroutine(Action callback, Vector3 target)
        {
            Vector3 pos = transform.position;
            float timer = 5f;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                transform.position = Vector3.Lerp(pos, target, 1 - timer / 5);
                yield return null;
            }
            transform.position = target;
            
            yield return new WaitForSeconds(2);

            callback?.Invoke();
        }
    }
}