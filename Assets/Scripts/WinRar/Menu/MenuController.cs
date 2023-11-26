using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinRar.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _tutorButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _exitButton;
        [Space]
        [SerializeField] private string _startGameSceneName;
        [Space]
        [SerializeField] private GameObject _tutorImage;
        [SerializeField] private Button _tutorExitButton;
        [Space]
        [SerializeField] private GameObject _creditsImage;
        [SerializeField] private Button _creditsExitButton;

        private void Start()
        {
            _tutorImage.SetActive(false);
            _creditsImage.SetActive(false);

            _startButton.onClick.AddListener(StartButton_OnClick);
            _tutorButton.onClick.AddListener(TutorButton_OnClick);
            _creditsButton.onClick.AddListener(CreditsButton_OnClick);
            _exitButton.onClick.AddListener(ExitButton_OnClick);
            _tutorExitButton.onClick.AddListener(TutorExitButton_OnClick);
            _creditsExitButton.onClick.AddListener(CreditsExitButton_OnClick);
        }

        private void StartButton_OnClick() => SceneManager.LoadScene(_startGameSceneName);
        private void TutorButton_OnClick() => _tutorImage.SetActive(true);
        private void CreditsButton_OnClick() => _creditsImage.SetActive(true);
        private void ExitButton_OnClick() => Application.Quit();
        private void TutorExitButton_OnClick() => _tutorImage.SetActive(false);
        private void CreditsExitButton_OnClick() => _creditsImage.SetActive(false);
    }
}