using System;
using UnityEngine;

namespace WinRar.Game.Levels
{
    public class LevelController : MonoBehaviour
    {
        public Action OnSuccess;
        public Action OnFailure;

        [SerializeField] private Transform _spawnPoint;

        public Vector3 SpawnPoint => _spawnPoint.position;
    }
}