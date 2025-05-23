using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu, _mapSelect, _gunSelect , _settingPanel , _shopPanel;
    [SerializeField] GameObject _loaderCanvas;
    [SerializeField] Slider _progressBar;
    [SerializeField] AsyncSceneLoader _sceneLoader;

    private string _sceneName;

    public void PlayBtn()
    {
        _mainMenu.SetActive(false);
        _mapSelect.SetActive(true);
    }
    public void SettingBtn()
    {
        _mainMenu.SetActive(false);
        //_settingPanel.SetActive(true);
    }

    public void ShopBtn()
    {
        _mainMenu.SetActive(false);
        _shopPanel.SetActive(true);
    }

    public void MapSelect(string sceneName)
    {
        _sceneName = sceneName;
        _mapSelect.SetActive(false);
        _gunSelect.SetActive(true);
    }

    public void GunSelect()
    {
        _gunSelect.SetActive(false);
        _sceneLoader.LoadSceneAsync(_sceneName);  

    }
    public void QuitBtn()
    {
        Application.Quit();
        print("Quit");
    }
}
