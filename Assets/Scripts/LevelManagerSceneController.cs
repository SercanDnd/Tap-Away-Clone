using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManagerSceneController : MonoBehaviour
{
    public int lastLevel;
    public List<Button> buttons=new List<Button>();
    void Start()
    {
        GetLastLevel();
        GameObject[] _buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (var button in _buttons)
        {
            if (!buttons.Contains(button.GetComponent<Button>()))
            {
                buttons.Add(button.GetComponent<Button>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lastLevel == 0)
        {
            lastLevel = 1;
        }
        else
        {
            foreach (var button in buttons)
            {
                if (button.GetComponent<LevelManagerButtonController>().levelId > lastLevel)
                {
                    button.GetComponent<LevelManagerButtonController>().isLocked = true;
                }
                else
                {
                    button.GetComponent<LevelManagerButtonController>().isLocked = false;
                }
            }
        }
        
      
    }

    public void GetLastLevel()
    {
       lastLevel=PlayerPrefs.GetInt("Level");
    }
}
