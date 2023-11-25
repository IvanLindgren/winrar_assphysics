using System;
using UnityEngine;

namespace WinRar.Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _moveDelta = 0.3f;

        private void Update()
        {
            var pos = transform.position;

            pos.x = _player.CameraArm.x + _player.SpeedBoostersCount * _moveDelta;

            transform.position = pos;
        }
    }
}