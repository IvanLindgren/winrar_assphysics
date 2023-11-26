using UnityEngine;
using TMPro;


namespace WinRar.Game.Levels
{
    public class LevelEncyclopedia : LevelController
    {
        [SerializeField] private PlayerEncyclopedia _playerEncyclopedia;
        [SerializeField] private TextMeshProUGUI instructionText;

        void Update()
        {
            UpdateInstruction();
        }

        private void UpdateInstruction()
        {
            instructionText.text = "Жми " + _playerEncyclopedia.nextKey;
        }
    }
}
