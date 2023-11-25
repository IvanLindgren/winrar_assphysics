using System;
using UnityEngine;
using WinRar.Game.Levels;

namespace WinRar.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LevelPhysics _physics;
        [SerializeField] private LevelEncyclopedia _encyc;
        [SerializeField] private LevelTale _tale;
        [SerializeField] private Player _player;

        private int _currentLevel;
        private LevelController[] _levels;

        private void Start()
        {
            _currentLevel = 0;

            _levels = new LevelController[] { _physics, _encyc, _tale };

            foreach (LevelController level in _levels)
            {
                level.OnSuccess += Level_OnSuccess;
                level.OnFailure += Level_OnFailure;
            }

            _player.OnDead += Player_OnDead;

            _player.Spawn(_levels[_currentLevel].SpawnPoint);
        }

        private void ToNextLevel()
        {
            _currentLevel++;
            if (_levels.Length <= _currentLevel)
            {
                _player.Stop();
            }
            else
            {
                _player.Spawn(_levels[_currentLevel].SpawnPoint);
            }
        }

        private void RestartLevel()
        {
            _player.Spawn(_levels[_currentLevel].SpawnPoint);
        }

        private void Level_OnSuccess() => ToNextLevel();
        private void Level_OnFailure() => RestartLevel();
        private void Player_OnDead() => RestartLevel();
    }
}