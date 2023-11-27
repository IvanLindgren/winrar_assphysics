using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


namespace WinRar.Game.Levels
{
    public class LevelEncyclopedia : LevelController
    {
        [SerializeField] private PlayerEncyclopedia _playerEncyclopedia;
        [SerializeField] private Animator _animPlayerAnimator;
        [SerializeField] private GameObject _animPlayerContainer;
        [SerializeField] private Animator _darvinAnimator;
        [SerializeField] private Animator _loseDoorAnimator;
        [SerializeField] private TextMeshProUGUI instructionText;
        [SerializeField] private Animator _winDoorAnimator;
        [SerializeField] private Trigger _winTrigger;

        private void Start()
        {
            _playerEncyclopedia.OnDead += PlayerEncyclopedia_OnDead;
            _playerEncyclopedia.transform.position = SpawnPoint;
            _loseDoorAnimator.gameObject.SetActive(false);
            _winDoorAnimator.gameObject.SetActive(false);

            _playerEncyclopedia.gameObject.SetActive(false);
            _animPlayerContainer.gameObject.SetActive(true);

            StartCoroutine(PlayStartAnim());

            _winTrigger.OnTriggerEnter += WinTrigger_OnEnter;
        }

        void Update()
        {
            UpdateInstruction();
        }

        private void UpdateInstruction()
        {
            instructionText.text = "Жми " + _playerEncyclopedia.NextKey;
        }

        private IEnumerator PlayStartAnim()
        {
            _darvinAnimator.Play("DarvinOpenMouthAnim");
            _animPlayerAnimator.Play("Start");

            yield return new WaitForSeconds(_animPlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            _playerEncyclopedia.gameObject.SetActive(true);
            _animPlayerContainer.gameObject.SetActive(false);
        }

        private IEnumerator DeadCoroutine()
        {
            _playerEncyclopedia.gameObject.SetActive(false);
            _loseDoorAnimator.gameObject.SetActive(true);

            _loseDoorAnimator.transform.position = _playerEncyclopedia.transform.position;

            _loseDoorAnimator.Play("LoseDoorAnim");
            yield return new WaitForSeconds(_loseDoorAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            _playerEncyclopedia.transform.position = SpawnPoint;
            _loseDoorAnimator.gameObject.SetActive(false);

            _animPlayerContainer.gameObject.SetActive(true);
            _darvinAnimator.Play("DarvinShokAnim");
            _animPlayerAnimator.Play("RestartAnim");

            float t1 = _darvinAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            float t2 = _animPlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            yield return new WaitForSeconds(Math.Max(t1, t2));

            _animPlayerContainer.gameObject.SetActive(false);
            _playerEncyclopedia.gameObject.SetActive(true);
        }

        private IEnumerator WinCoroutine()
        {
            _playerEncyclopedia.gameObject.SetActive(false);
            _winDoorAnimator.gameObject.SetActive(true);
            
            _winDoorAnimator.Play("WinDoorAnim");
            yield return new WaitForSeconds(_winDoorAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            SceneManager.LoadScene("3Game_Scene3");
        }

        private void PlayerEncyclopedia_OnDead()
        {
            StartCoroutine(DeadCoroutine());
        }

        private void WinTrigger_OnEnter(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                StartCoroutine(WinCoroutine());
            }
        }
    }
}