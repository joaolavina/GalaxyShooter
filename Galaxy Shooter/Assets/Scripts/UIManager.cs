using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public Image mainMenu;
    public int score;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void UptadeLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UptadeScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void showTitleScreen()
    {
        mainMenu.enabled = true;
    }
    public void hideTitleScreen()
    {
        mainMenu.enabled = false;
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
