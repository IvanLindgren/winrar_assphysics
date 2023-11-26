using System;
using UnityEngine;

namespace WinRar.Game
{
    public class PlayerHitController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private float _jumpedY;
        [SerializeField] private float _crouchedY;
        [SerializeField] private float _normalY;
        [Space]
        [SerializeField] private Trigger _topTrigger;
        [SerializeField] private Trigger _bottomTrigger;
        [Space]
        [SerializeField] private Animator _animator;


        private void Start()
        {
            _topTrigger.OnTriggerEnter += OnCustomTriggerEnter;
            _bottomTrigger.OnTriggerEnter += OnCustomTriggerEnter;
            _topTrigger.OnTriggerExit += OnCustomTriggerExit;
            _bottomTrigger.OnTriggerExit += OnCustomTriggerExit;
        }

        private void Update()
        {
            // animation
            if (_inputSystem.Vertical > 0)
            {
                _animator.Play("PlayerCrouchAnim");
            }
            else if (_inputSystem.Vertical < 0)
            {
                _animator.Play("PlayerJumpAnim");
            }
            else
            {
                _animator.Play("PlayerRunAnim");
            }

            // movement
            var pos = transform.localPosition;
            var scale = transform.localScale;
            if (_inputSystem.Vertical > 0 && !_player.CanMoveToTopLayer)
            {
                pos.y = _jumpedY;
                _topTrigger.gameObject.SetActive(true);
                _bottomTrigger.gameObject.SetActive(false);
            }
            else if (_inputSystem.Vertical < 0 && !_player.CanMoveToBottomLayer)
            {
                pos.y = _crouchedY;
                _topTrigger.gameObject.SetActive(false);
                _bottomTrigger.gameObject.SetActive(true);
            }
            else
            {
                pos.y = _normalY;
                _topTrigger.gameObject.SetActive(true);
                _bottomTrigger.gameObject.SetActive(true);
            }
            transform.localPosition = pos;
            transform.localScale = scale;
        }

        private void OnCustomTriggerEnter(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                _player.ObstacleTriggered();
            }
            else if (collision.gameObject.CompareTag("BoosterUp"))
            {
                _player.BoosterUpTriggered();
            }
            else if (collision.gameObject.CompareTag("BoosterDown"))
            {
                _player.BoosterDownTriggered();
            }
            else if (collision.gameObject.CompareTag("MoveToTopLayer"))
            {
                _player.CanMoveToTopLayer = true;
            }
            else if (collision.gameObject.CompareTag("MoveToBottomLayer"))
            {
                _player.CanMoveToBottomLayer = true;
            }
        }

        private void OnCustomTriggerExit(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("MoveToTopLayer"))
            {
                _player.CanMoveToTopLayer = false;
            }
            else if (collision.gameObject.CompareTag("MoveToBottomLayer"))
            {
                _player.CanMoveToBottomLayer = false;
            }
        }
    }
}