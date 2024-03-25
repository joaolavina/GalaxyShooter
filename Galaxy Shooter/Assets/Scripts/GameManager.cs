using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool gameOver = true;
    
    [SerializeField]
    private GameObject _player;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = true;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_player, new Vector3(0,0,0), Quaternion.identity);
                // Instantiate(_spawnManager,)
                gameOver = false;
                _uiManager.hideTitleScreen();
            }
        }    
    }
}
