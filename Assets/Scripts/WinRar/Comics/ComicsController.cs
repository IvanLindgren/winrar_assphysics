using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinRar.Comics
{
    public class ComicsController : MonoBehaviour
    {
        public void StartScene(string sceneAsset)
        {
            SceneManager.LoadScene(sceneAsset);
        }
    }
}
