using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{


    public static GameManager Instance;

    public delegate void MinusLeftMove();
    public MinusLeftMove minusLeftMove;
    public delegate void LeftCubesRemover(GameObject cube);
    public LeftCubesRemover leftCubesRemover;

    public int levelMoves;
    public int leftMoves;
    public int challangeMoves;
    bool isGameOver;
    bool isGameComplate;
    public int level;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI leftText;
    public List<GameObject> lastCubes;
    public GameObject gameoverPanel;
    public GameObject nextLevelPanel;
    void Start()
    {
        

    if(Instance== null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        SetLevelMoves();
        minusLeftMove += MinusMove;
        leftCubesRemover += RemoveLeftCube;
    }

    // Update is called once per frame
    void Update()
    {
        SetGameOver();
        SetNextLevel();
        SetLevelText();
        SetLevelMovesText();
        LastCubes();
        if (isGameOver == true)
        {
            GameOverScreen();
        }
        else
        {
            gameoverPanel.SetActive(false);
        }

        if(isGameComplate==true)
        {
            NextLevel();
        }
        else
        {
            nextLevelPanel.SetActive(false);
        }
    }

    public void NextLevel()
    {
        nextLevelPanel.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            if (SceneManager.sceneCount< level-1)
            {
                SceneManager.LoadScene($"LevelManagerScene");
            }
            else
            {
                SceneManager.LoadScene($"level{level + 1}");
            }
            
        }
    }

    public void SetGameOver()
    {
        if (leftMoves <= 0)
        {
            if (lastCubes.Count > 0)
                //For Waiting
                //Evet evet gidip hamle sayýsýný düþürmek için týklamadan deðil collider çarpýþmasýndan da alabilirim
                //ve ortada bekleme gibi bir sorunum kalmaz ama uðraþmak istememek baþka tabi
                Invoke("SetGameOverBoolTrue", 1f);
           
        }
        else
        {
            isGameOver = false;
        }
    }
    public void SetGameOverBoolTrue()
    {
        isGameOver = true;
    }
    public void SetNextLevel()
    {
        if (lastCubes.Count == 0)
        {
            if (isGameOver == false)
            {
                isGameComplate = true;
            }
        }
        else
        {
            isGameComplate = false;
        }
    }

    public void SetLevelMoves()
    {
        leftMoves = levelMoves;
    }

    public void SetLevelText()
    {
        levelText.text = level.ToString();
    }

    public void SetLevelMovesText()
    {
        leftText.text = leftMoves.ToString();
    }

    public void MinusMove()
    {
        leftMoves -= 1;
    }

    public void GameOverScreen()
    {
        gameoverPanel.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            Retry();
        }
    }

    public void LastCubes()
    {
        //That codes effect optimization badly
        //Sadece gece ve yorgun olduðum için siz yapmayýn böyle küçük ama mide bulandýrýcý þeyler :)

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");

        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i].gameObject.activeInHierarchy == true)
            {
                if (!lastCubes.Contains(cubes[i]))
                {
                    lastCubes.Add(cubes[i]);
                }
            }
        }
    }

    public void RemoveLeftCube(GameObject cube)
    {
        lastCubes.Remove(cube);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", level);
        
        
    }

    public void Retry()
    {
        SceneManager.LoadScene($"level{level}");
    }

    public void OpenLevelManagerScene()
    {
        SceneManager.LoadScene("LevelManagerScene");
    }
}
