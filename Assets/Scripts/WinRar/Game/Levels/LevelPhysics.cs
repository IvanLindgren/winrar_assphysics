using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinRar.Game.Levels
{
    public class LevelPhysics : LevelController
    {
        [SerializeField] private Trigger _levelFinishTrigger;
        [SerializeField] private Player _player;
        [SerializeField] private Animator _einsteinAnimator;
        [SerializeField] private Transform _einsteinTarget;
        [SerializeField] private Transform _cameraTarget;
        [Space]
        [SerializeField] private Animator _angryEinsteinAnimator;
        [SerializeField] private float _deltaPosX = -8;

        private void Start()
        {
            _levelFinishTrigger.OnTriggerEnter += LevelFinishTrigger_OnTriggerEnter;
            _player.OnDead += Player_OnDead;
            _angryEinsteinAnimator.gameObject.SetActive(false);
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

            SceneManager.LoadScene("IvanTestScene");
            // SceneManager.LoadScene("FinishComicsScene");
        }

        private IEnumerator PlayerDeadCoroutine()
        {
            _player.Stop();
            
            _angryEinsteinAnimator.gameObject.SetActive(true);
            var p = _angryEinsteinAnimator.transform.position;
            _angryEinsteinAnimator.transform.position = new Vector3(_player.transform.position.x + _deltaPosX, p.y, p.z);
            _angryEinsteinAnimator.Play("EinsteinAngryAnim");

            yield return new WaitForSeconds(_angryEinsteinAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length + 1f);

            _angryEinsteinAnimator.gameObject.SetActive(false);
            _player.Spawn(SpawnPoint);
        }

        private void LevelFinishTrigger_OnTriggerEnter(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                StartCoroutine(LevelFinishCoroutine());
        }

        private void Player_OnDead()
        {
            StartCoroutine(PlayerDeadCoroutine());
        }
    }
}