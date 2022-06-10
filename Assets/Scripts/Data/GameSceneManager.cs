using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

 public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private AnalyticManager _analytic;
    [SerializeField] private Data _data;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Player _player;
    [SerializeField] private Resources _resources;

    private int _nextLevelIndex;

    private void Awake()
    {
        _data.Load();
        _data.SetLevelIndex(SceneManager.GetActiveScene().buildIndex);
        _playerTransform.position = new Vector3(47.5f, 0f, -25f);
        _player.Replenish(_data.GetCurrentSoft());
        _resources.ActivateServiceZones(_data.GetOpennedServiceZones());
        _resources.DeactivateMoneyDropZones(_data.GetClosedMoneyDropZones());
        _data.Save();
    }

    private void Start()
    {
        _analytic.SendEventOnLevelStart(_data.GetDisplayedLevelNumber());
    }

    private void OnApplicationQuit()
    {
        _analytic.SendEventOnGameExit(_data.GetRegistrationDate(), _data.GetSessionCount(), _data.GetNumberDaysAfterRegistration(), _data.GetCurrentSoft());
        _data.SetCurrentSoft(_player.GetWalletCash());
        _data.SetOppenedServiceZones(_resources.GetActiveServiceZones());
        _data.SetClosedMoneyDropZones(_resources.GetInactiveDropZones());
        _data.Save();
    }

    public void LevelFail()
    {
        _analytic.SendEventOnFail(_data.GetDisplayedLevelNumber());
    }

    public void LoadNextScene()
    {
        _analytic.SendEventOnLevelComplete(_data.GetDisplayedLevelNumber());
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            _nextLevelIndex = 1;
        else
            _nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        _data.SetLevelIndex(_nextLevelIndex);
        _data.AddDisplayedLevelNumber();
        _data.Save();
        SceneManager.LoadScene(_nextLevelIndex);
    }

    public void ReloadScene()
    {
        _analytic.SendEventOnLevelRestart(_data.GetDisplayedLevelNumber());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
