using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class TextAnimation : MonoBehaviour
{
    public GameObject animatedText;
    void Start()
    {
        AnimateSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateSize()
    {
        animatedText.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f).SetLoops(100,LoopType.Yoyo);
    }
}
