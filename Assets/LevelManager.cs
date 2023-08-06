using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels;
    public List<GameObject> levelButtons;
    public List<GameObject> canvasAllButtons;
    [SerializeField] UIManager uiManager;
    int index=0;
    void Start()
    {
        foreach(var button in levelButtons)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            Button thisButton = button.GetComponent<Button>();
            index +=1;
            buttonText.text = index.ToString();
            thisButton.onClick.AddListener(() => AddLevel(buttonText));
        }
        Button backButton = GameObject.Find("LevelBack").GetComponent<Button>();
        backButton.onClick.AddListener(()=>StartCoroutine(uiManager.levelSelectBack()));
        
    }
    void AddLevel(TMP_Text text)
    {
        levels[int.Parse(text.text)-1].SetActive(true);
        this.gameObject.SetActive(false);
    }

}
