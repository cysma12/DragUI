using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeManager : MonoBehaviour
{

    public static SceneChangeManager Instance;

    public RawImage BlackBackgroundImage;

    private float _activeTime = 0.5f;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else Destroy(this.gameObject);      
    }

    #region ChangeScenes

    public void TempChangeScene(string scenes)
    {
        //SceneManager.LoadScene(scenes);

        ActiveBackground(true);
        DOVirtual.DelayedCall(_activeTime, delegate () {
            SceneManager.LoadScene(scenes);
        });
    }

    public void ChangeScene(SCENE_SORT scenes)
    {
        string tempSceneName = scenes.ToString();
        SceneManager.LoadScene(tempSceneName);        
    }


    private void ActiveSceneChange(string scenes)
    {
        ActiveBackground(true);
        DOVirtual.DelayedCall(_activeTime,delegate() {
            SceneManager.LoadScene(scenes);
        });
       
    }
    #endregion


    private void ActiveBackground(bool isActive)
    {
        if (isActive)
        {
            BlackBackgroundImage.gameObject.SetActive(true);
            BlackBackgroundImage.DOColor(new Color(0,0,0,1), _activeTime);
        }
        else 
        {           
            BlackBackgroundImage.DOColor(new Color(0,0,0,0), _activeTime).OnComplete(delegate() {
                BlackBackgroundImage.gameObject.SetActive(false);
            });
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ActiveBackground(false);
    }

    private void Init()
    {
        BlackBackgroundImage.gameObject.SetActive(true);
        BlackBackgroundImage.color = new Color(0,0,0,1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


}
