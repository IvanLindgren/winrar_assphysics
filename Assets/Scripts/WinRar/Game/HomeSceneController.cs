using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinRar.Game
{
    public class HomeSceneController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Trigger _finishTrigger;

        private void Start()
        {
            _player.OnDead += Player_OnDead;
            _finishTrigger.OnTriggerEnter += FinishTrigger_Enter;
        }

        private void Player_OnDead()
        {
            SceneManager.LoadScene("BadComics");
        }

        private void FinishTrigger_Enter(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                SceneManager.LoadScene("FinishComicsScene");
        }
    }
}