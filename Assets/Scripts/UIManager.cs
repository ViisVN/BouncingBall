using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ButtonLists _menuManager;
    [SerializeField] private ButtonLists _levelManager;
    [SerializeField] public ButtonLists _nextLevelManager;
    [SerializeField] public ButtonLists _loseLevelManager;
    [SerializeField] public ButtonLists _optionManager;
    [Header("LevelControl")]
    public List<GameObject> levels;
    public List<GameObject> levelButtons;
    int levelIndex = 0;
    [Header("OptionManager")]
    public List<Sprite> soundSpites;
    bool IsMute;

    public GameObject levelbackButton;


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
                    _button.onClick.AddListener(() => StartCoroutine(PopUpThenPopOut(_menuManager, _levelManager)));
                    break;
                case "Option":
                    _button.onClick.AddListener(() => StartCoroutine(PopUpThenPopOut(_menuManager, _optionManager)));
                    break;
            }
        }

        foreach (var button in _optionManager.buttons)
        {
            Button _button = button.GetComponent<Button>();
            Debug.Log(button.name);
            switch (button.name)
            {
                case "Sound":
                    _button.onClick.AddListener(() => SoundControl(button));
                    break;
                case "Back":
                    _button.onClick.AddListener(() => StartCoroutine(PopUpThenPopOut(_optionManager, _menuManager)));
                    break;
            }
        }
    }
    void Start()
    {
        foreach (var button in levelButtons)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            Button thisButton = button.GetComponent<Button>();
            levelIndex += 1;
            buttonText.text = levelIndex.ToString();
            if (buttonText != null)
            {
                thisButton.onClick.AddListener(() => AddLevel(buttonText, _levelManager));
            }
            Button backButton = levelbackButton.GetComponent<Button>();
            backButton.onClick.AddListener(() => StartCoroutine(PopUpThenPopOut(_levelManager, _menuManager)));
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
        StartCoroutine(buttonPopUp(_loseLevelManager.buttons));
        levels[mapIndex - 1].SetActive(false);
        levels[mapIndex - 1].SetActive(true);
        levels[mapIndex - 1].SetActive(false);
        RemoveButtonListeners(_loseLevelManager);
        foreach (var button in _loseLevelManager.buttons)
        {
            Button _button = button.GetComponent<Button>();
            switch (button.name)
            {
                case "Menu":
                    _button.onClick.AddListener(() => StartCoroutine(PopUpThenPopOut(_loseLevelManager, _menuManager)));
                    break;
                case "PlayAgain":
                    _button.onClick.AddListener(() => NextLevel(mapIndex - 1, _loseLevelManager, _menuManager));
                    break;
            }
        }
    }
    public void nextLevel(int mapIndex)
    {
        _nextLevelManager.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(_nextLevelManager.buttons));
        levels[mapIndex - 1].SetActive(false);
        levels[mapIndex - 1].SetActive(true);
        levels[mapIndex - 1].SetActive(false);
        RemoveButtonListeners(_nextLevelManager);
        foreach (var button in _nextLevelManager.buttons)
        {
            Button _button = button.GetComponent<Button>();
            switch (button.name)
            {
                case "Menu":
                    _button.onClick.AddListener(() => StartCoroutine(PopUpThenPopOut(_nextLevelManager, _menuManager)));
                    break;
                case "PlayAgain":
                    _button.onClick.AddListener(() => NextLevel(mapIndex - 1, _nextLevelManager, _levelManager));
                    break;
                case "NextLevel":
                    _button.onClick.AddListener(() => NextLevel(mapIndex, _nextLevelManager, _levelManager));
                    break;
            }
        }

    }
    private void RemoveButtonListeners(ButtonLists _nextLevelManager)
    {
        foreach (var button in _nextLevelManager.buttons)
        {
            Button _button = button.GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
        }
    }
    public void SoundControl(GameObject button)
    {
        if (!IsMute)
        {
            button.GetComponent<Image>().sprite = soundSpites[1];
            IsMute = true;
        }
        else if (IsMute)
        {
            button.GetComponent<Image>().sprite = soundSpites[0];
            IsMute = false;
        }
    }


    void NextLevel(int mapIndex, ButtonLists _nextLevelManager, ButtonLists _menuManager)
    {
        if (mapIndex == levels.Count)
        {
            StartCoroutine(PopUpThenPopOut(_nextLevelManager, _menuManager));
        }
        else
        {
            _nextLevelManager.gameObject.SetActive(false);
            levels[mapIndex].SetActive(true);
        }
    }
    public IEnumerator PopUpThenPopOut(ButtonLists listPopOut, ButtonLists listPopUp)
    {
        StartCoroutine(buttonPopOut(listPopOut.buttons));
        yield return new WaitForSeconds(1f);
        listPopOut.gameObject.SetActive(false);
        listPopUp.gameObject.SetActive(true);
        StartCoroutine(buttonPopUp(listPopUp.buttons));
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
    void AddLevel(TMP_Text text, ButtonLists _levelManager)
    {
        levels[int.Parse(text.text) - 1].SetActive(true);
        _levelManager.gameObject.SetActive(false);
    }
}
