using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] public NextLevelManager _nextLevelManager;
    [SerializeField] public NextLevelManager _loseLevelManager;
    [SerializeField] public OptionManager _optionManager;


    void Awake()
    {
        buttonPopUp(_menuManager.buttons);
        foreach (var button in _menuManager.buttons)
        {
            TMP_Text btText = button.GetComponentInChildren<TMP_Text>();
            Button _button = button.GetComponent<Button>();
            switch (btText.text)
            {
                case "Level Select":
                    _button.onClick.AddListener(() => StartCoroutine(LevelSelectPopUP()));
                    break;
                case "Option":
                    _button.onClick.AddListener(() => StartCoroutine(OptionPopUp()));
                    break;
            }
        }
    }
      private void OnEnable()
    {
        EventManager.Instance.OnMapIndexChange += (mapIndex) => nextLevel(mapIndex);
        EventManager.Instance.OnLoseMapIndexChange += (mapIndex) => loseLevel(mapIndex);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnMapIndexChange -= (mapIndex) => nextLevel(mapIndex);
         EventManager.Instance.OnLoseMapIndexChange -= (mapIndex) => loseLevel(mapIndex);
    }
    public void loseLevel(int mapIndex)
    {
      _loseLevelManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_loseLevelManager.nextLevelsLists));
        _levelManager.levels[mapIndex-1].SetActive(false);
        _levelManager.levels[mapIndex-1].SetActive(true);
        _levelManager.levels[mapIndex-1].SetActive(false);
        RemoveButtonListeners(_loseLevelManager);
         foreach (var button in _loseLevelManager.nextLevelsLists)
        {
            Button _button = button.GetComponent<Button>();
            switch (button.name)
            {
                case "Menu":
                    _button.onClick.AddListener(() => StartCoroutine(LevelSelectPopUP2(_loseLevelManager)));
                    break;
                case "PlayAgain":
                    _button.onClick.AddListener(() => NextLevel(mapIndex - 1,_loseLevelManager));
                    break;
            }
        }
    }
    public void nextLevel(int mapIndex)
    {
        _nextLevelManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_nextLevelManager.nextLevelsLists));
        _levelManager.levels[mapIndex-1].SetActive(false);
        _levelManager.levels[mapIndex-1].SetActive(true);
        _levelManager.levels[mapIndex-1].SetActive(false);
        RemoveButtonListeners(_nextLevelManager);
        foreach (var button in _nextLevelManager.nextLevelsLists)
        {
            Button _button = button.GetComponent<Button>();
            switch (button.name)
            {
                case "Menu":
                    _button.onClick.AddListener(() => StartCoroutine(LevelSelectPopUP2(_nextLevelManager)));
                    break;
                case "PlayAgain":
                    _button.onClick.AddListener(() => NextLevel(mapIndex - 1,_nextLevelManager));
                    break;
                case "NextLevel":
                    _button.onClick.AddListener(() => NextLevel(mapIndex,_nextLevelManager));
                    break;
            }
        }
        
    }
    private void RemoveButtonListeners( NextLevelManager _nextLevelManager)
{
    foreach (var button in _nextLevelManager.nextLevelsLists)
    {
        Button _button = button.GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
    }
}

    
    void NextLevel(int mapIndex, NextLevelManager _nextLevelManager )
    {
        if (mapIndex == _levelManager.levels.Count)
        {
            StartCoroutine(LevelSelectPopUP2( _nextLevelManager));
        }
        else
        {
            _nextLevelManager.gameObject.SetActive(false);
            _levelManager.levels[mapIndex].SetActive(true);
        }
    }
    IEnumerator OptionPopUp()
    {
          StartCoroutine(buttonPopOut(_menuManager.buttons));
        yield return new WaitForSeconds(1f);
        _menuManager.gameObject.SetActive(false);
        _optionManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_optionManager.buttons));
    }
    public  IEnumerator OptionPopOut()
    {
          StartCoroutine(buttonPopOut(_optionManager.buttons));
        yield return new WaitForSeconds(1f);
        _optionManager.gameObject.SetActive(false);
        _menuManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_menuManager.buttons));
    }
    IEnumerator LevelSelectPopUP2(NextLevelManager _nextLevelManager)
    {   
        StartCoroutine(buttonPopOut(_nextLevelManager.nextLevelsLists));
        yield return new WaitForSeconds(1f);
        _menuManager.gameObject.SetActive(false);
        _levelManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_levelManager.canvasAllButtons));
    }
    IEnumerator LevelSelectPopUP()
    {
        StartCoroutine(buttonPopOut(_menuManager.buttons));
        yield return new WaitForSeconds(1f);
        _menuManager.gameObject.SetActive(false);
        _levelManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_levelManager.canvasAllButtons));
    }
    public IEnumerator levelSelectBack()
    {
        StartCoroutine(buttonPopOut(_levelManager.canvasAllButtons));
        yield return new WaitForSeconds(1f);
        _levelManager.gameObject.SetActive(false);
        _menuManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_menuManager.buttons));
    }

    public IEnumerator buttonPopUp(List<GameObject> buttonList)
    {
        foreach (var button in buttonList)
        {
            button.transform.localScale = Vector3.zero;
            Button _button = button.GetComponent<Button>();
            _button.enabled = false;
        }
        foreach (var button in buttonList)
        {
            Button _button = button.GetComponent<Button>();
            button.transform.DOScale(1, 1).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.1f);
            _button.enabled = true;
        }
    }
    public IEnumerator buttonPopOut(List<GameObject> buttonList)
    {
        foreach (var button in buttonList)
        {
            Button _button = button.GetComponent<Button>();
            _button.enabled = false;
            button.transform.DOScale(0f, 1).SetEase(Ease.OutSine);
            yield return new WaitForSeconds(0f);
        }
    }
}
