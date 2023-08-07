using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<Sprite> soundSpites;
    bool IsMute = false;
    public UIManager uiManager;
    private void Start() 
    {
       foreach(var button in buttons)
       {
        Button _button = button.GetComponent<Button>();
        Debug.Log(button.name);
         switch(button.name)
         {
            case "Sound": _button.onClick.AddListener(()=> SoundControl(button));
            break;
            case "Back": _button.onClick.AddListener(() => StartCoroutine(uiManager.OptionPopOut()));
            break;
         }
       }
    }
    public void SoundControl(GameObject button)
    {
       if(!IsMute)
       {
         button.GetComponent<Image>().sprite =soundSpites[1];
         IsMute=true;
       }
       else if(IsMute)
       {
        button.GetComponent<Image>().sprite = soundSpites[0];
        IsMute = false;
       }
    }
}
