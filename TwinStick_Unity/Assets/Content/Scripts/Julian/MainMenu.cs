using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject exitWindow = null;
    [SerializeField] private CanvasGroup loadingScreenGroup = null;
    [SerializeField] private CanvasGroup loadingIconGroup = null;

    private GameObject[] menuScreens = null;

    private void Start()
    {
        loadingScreenGroup.alpha = 0;

        exitWindow.SetActive(false);

        Cursor.visible = true;
        Time.timeScale = 1;
    }


    /// <summary>
    /// Starts The Game
    /// </summary>
    public void LoadScene(int _level)
    {
        StartCoroutine(LoadSceneCoroutine(_level));
    }

    private IEnumerator LoadSceneCoroutine(int _level)
    {
        GameManager.instance.statsController.AddTimesPlayed();
        AsyncOperation _loadOperation = SceneManager.LoadSceneAsync(_level);
        _loadOperation.allowSceneActivation = false;

        float _timer = 0;

        while (_timer < 1f && !_loadOperation.isDone)
        {
            _timer += Time.deltaTime * 10;

            loadingScreenGroup.alpha = _timer;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        _timer = 1;

        while (_timer > 0)
        {
            _timer -= Time.deltaTime * 5;

            loadingIconGroup.alpha = _timer;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        _loadOperation.allowSceneActivation = true;

    }


    /// <summary>
    /// Opens the exit screen
    /// </summary>
    public void OpenExitScreen()
    {
        exitWindow.SetActive(true);
    }

    /// <summary>
    /// Closes Exit Screen
    /// </summary>
    public void CloseExitScreen()
    {
        exitWindow.SetActive(false);
    }


    /// <summary>
    /// Closes the application
    /// </summary>
    public void ExitApplication()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}



