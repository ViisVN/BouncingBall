using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapControl : MonoBehaviour
{
    public int MapNumber;
    public int basedCount,count;
    public TMP_Text text;
    SquareControl ballBounce;
    public bool Ispass = false;
    public bool NotPass = false;
    public Transform ballPos;
    public GameObject ball;
    public UIManager uIManager;
    private void OnEnable() 
    {
        count = basedCount;
        ball.SetActive(true);
        ballBounce = ball.GetComponent<SquareControl>();
        ball.GetComponent<SpriteRenderer>().enabled = true;
        textControl(count, text);
        ballBounce.gameObject.transform.position = ballPos.position;
        Time.timeScale=1f;
    }
    private void OnDisable()

    {    
        NotPass = false;
         Ispass = false;
         count = basedCount;
         ball.SetActive(false);
         ballBounce.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public void minusCount(int _count)
    {
        if(NotPass)
        {
            Time.timeScale = 0f;
            EventManager.Instance.InvokeMapIndexChangeLose(MapNumber);
        }
        if (count > 0)
        {
            count -= _count;
            textControl(count, text);
        }
        else if (count == 0)
        {
            if(Ispass)
            {
                Debug.Log("Pass");
                Time.timeScale = 0f;
                EventManager.Instance.InvokeMapIndexChange(MapNumber);
            }
            else
            {
                Debug.Log("False");
                Time.timeScale = 0f;
                EventManager.Instance.InvokeMapIndexChangeLose(MapNumber);
            }
        }
        if (Ispass&&count>0)
        {
            Debug.Log("False");
            Time.timeScale = 0f;
            EventManager.Instance.InvokeMapIndexChangeLose(MapNumber);
        }
    }
    void textControl(int count, TMP_Text text)
    {
        text.text = count.ToString();
        text.color = Color.red;
        if (count == 0)
        {
            text.color = Color.green;
        }
    }
}

