using System;
using UnityEngine;

namespace WinRar.Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _moveDelta = 0.3f;

        public bool IsChasing { get; set; } = true;

        private void Update()
        {
            if (!IsChasing)
                return;

            var pos = transform.position;

            // pos.x = _player.CameraArm.x + _player.SpeedBoostersCount * _moveDelta;
            pos.x = _player.CameraArm.x;

            transform.position = pos;
        }
    }
}