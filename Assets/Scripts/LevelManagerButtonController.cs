using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerButtonController : MonoBehaviour
{
    public int levelId;
    public bool isLocked;
    RawImage buttonImage;
    public Texture lockImage;
    public Texture UnlockImage;
    void Start()
    {
        buttonImage=GetComponentInChildren<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsLocked();
    }

   

    public void CheckIsLocked()
    {
        if (isLocked==true) 
        {
            buttonImage.texture = lockImage;
            GetComponent<Button>().interactable = false;
            
        }
        else
        {
            buttonImage.texture=UnlockImage;
            GetComponent<Button>().interactable = true;
        }
    }

    public void OpenScene()
    {
        SceneManager.LoadScene($"level{levelId}");
        Debug.Log("opend ");
    }
}
