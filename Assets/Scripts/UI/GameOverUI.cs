using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteInEditMode]
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button quitButton;
        [SerializeField] private Button retryButton;

        private void Awake()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            foreach (Button button in buttons)
            {
                if (button.name.Equals("Quit"))
                {
                    quitButton = button;
                } else if (button.name.Equals("Retry"))
                {
                    retryButton = button;
                }
            }

            if (quitButton)
            {
                quitButton.onClick.AddListener(HandleQuit);
            }
            if (retryButton)
            {
                retryButton.onClick.AddListener(HandleRetry);
            }
        }

        private void HandleQuit()
        {
            GameManager.instance.Quit();
        }

        private void HandleRetry()
        {
            GameManager.instance.RetryLevel();
        }
    }
}
