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

        private void Update()
        {
            var pos = transform.localPosition;
            var scale = transform.localScale;

            if (_inputSystem.Vertical > 0)
            {
                pos.y = _jumpedY;
                scale = new Vector3(1, 0.5f, 1);
            }
            else if (_inputSystem.Vertical < 0)
            {
                pos.y = _crouchedY;
                scale = new Vector3(1, 0.5f, 1);
            }
            else
            {
                pos.y = _normalY;
                scale = new Vector3(1, 1, 1);
            }

            transform.localPosition = pos;
            transform.localScale = scale;
        }

        private void OnTriggerEnter2D(Collider2D collision)
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
                _player.MoveToTopLayerTriggered();
            }
            else if (collision.gameObject.CompareTag("MoveToBottomLayer"))
            {
                _player.MoveToBottomLayerTriggered();
            }
        }
    }
}