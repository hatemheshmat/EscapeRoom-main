using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MainMENUManager : MonoBehaviour

{
    [Header("Main MenuObjects")]
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private GameObject[] _objectToHide;
    [SerializeField] Image LoadingBAR;

    [Header("Scenes to Load")]
    [SerializeField] private SceneField _persistentGmaePlay;
    [SerializeField] private SceneField _levelScene;

    private List<AsyncOperation> _SceneToLoad = new List<AsyncOperation>();



    private void Awake()
    {
        _loadingBarObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void StartGame()
    {
        _loadingBarObject.SetActive(true);
        HideMenu();
       _SceneToLoad.Add(SceneManager.LoadSceneAsync(_persistentGmaePlay));
       _SceneToLoad.Add(SceneManager.LoadSceneAsync(_levelScene, LoadSceneMode.Additive));
        StartCoroutine(loadingBar());

    }

    private void HideMenu()
    {
        for (int i = 0; i < _objectToHide.Length; i++)
        {
            _objectToHide[i].SetActive(false);
        }
    }

    private IEnumerator loadingBar()
    {
        float loadingProgress = 0f;
        for (int i =0; i < _SceneToLoad.Count; i++)
        {
            while (!_SceneToLoad[i].isDone)
            {
                loadingProgress += _SceneToLoad[i].progress;
                LoadingBAR.fillAmount = loadingProgress / _SceneToLoad.Count;
                yield return null;

            }
        }

    }
    
}
