using System.Linq;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        [SerializeField] private GameOverUI gameOverUi;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private InputActionAsset inputActionAsset;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            gameOverUi = Resources.FindObjectsOfTypeAll<GameOverUI>().First();
            gameOverUi.gameObject.SetActive(false);

            if (retryButton != null)
            {
                retryButton.onClick.AddListener(RetryLevel);
            }
            if (quitButton != null)
            {
                quitButton.onClick.AddListener(Quit);
            }
            
            
#if UNITY_EDITOR
            string assetPathId = AssetDatabase.FindAssets($"t:{typeof(InputActionAsset)}").First();
            string assetPath = AssetDatabase.GUIDToAssetPath(assetPathId);
            inputActionAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(assetPath);
#endif
        }

        public void NextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public void RetryLevel()
        {
            inputActionAsset.Disable();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            gameOverUi.gameObject.SetActive(true);
            inputActionAsset.Disable();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
