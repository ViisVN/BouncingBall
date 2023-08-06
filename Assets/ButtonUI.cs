using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonUI : MonoBehaviour
{
    private void Start() 
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(1.8f,0.5f,0),1).SetEase(Ease.OutBounce);
    }
}
