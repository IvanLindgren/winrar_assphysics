using System;
using System.Collections;
using UnityEngine;

namespace WinRar.Game.Levels
{
    public class LevelPhysics : LevelController
    {
        [SerializeField] private Trigger[] _toTopLayerTriggers;
        [SerializeField] private Trigger[] _toBottomLayerTriggers;
        [SerializeField] private Trigger _levelFinishTrigger;
        [SerializeField] private Player _player;
        [SerializeField] private Animator _einsteinAnimator;
        [SerializeField] private Transform _einsteinTarget;
        [SerializeField] private Transform _cameraTarget;

        private void Start()
        {
            foreach (Trigger trigger in _toTopLayerTriggers)
            {
                trigger.OnTriggerEnter += ToTopLayerTrigger_OnTriggerEnter;
                trigger.OnTriggerExit += ToTopLayerTrigger_OnTriggerExit;
            }
            foreach (Trigger trigger in _toBottomLayerTriggers)
            {
                trigger.OnTriggerEnter += ToBottomLayerTrigger_OnTriggerEnter;
                trigger.OnTriggerExit += ToBottomLayerTrigger_OnTriggerExit;
            }

            _levelFinishTrigger.OnTriggerEnter += LevelFinishTrigger_OnTriggerEnter;
        }

        private IEnumerator LevelFinishCoroutine()
        {
            bool isAnimsFinished = false;
            _player.PlayFinishAnim(
                () => isAnimsFinished = true,
                () => _einsteinAnimator.Play("EinsteinOpenMouthAnim"),
                _einsteinTarget.position,
                _cameraTarget.position);

            while (!isAnimsFinished)
                yield return null;

            OnSuccess?.Invoke();
        }

        private void ToTopLayerTrigger_OnTriggerEnter(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                _player.CanMoveToTopLayer = true;
        }
        private void ToTopLayerTrigger_OnTriggerExit(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                _player.CanMoveToTopLayer = false;
        }
        private void ToBottomLayerTrigger_OnTriggerEnter(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                _player.CanMoveToBottomLayer = true;
        }
        private void ToBottomLayerTrigger_OnTriggerExit(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                _player.CanMoveToBottomLayer = false;
        }
        private void LevelFinishTrigger_OnTriggerEnter(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                StartCoroutine(LevelFinishCoroutine());
        }
    }
}