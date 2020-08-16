using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Canvas _canvas;

    private RectTransform _menuPanel;
    private RectTransform _levelPanel;
    private RectTransform _scorePanel;
    private Button _startGameButton;
    private Text _healthText;
    private Text _levelStateText;
    private Button _restartGameButton;
    public GameObject ingameMenu;

    private void OnEnable()
    {
        _canvas = transform.Find("Canvas").GetComponent<Canvas>();
        _menuPanel = _canvas.transform.Find("Menu").GetComponent<RectTransform>();
        _startGameButton = _menuPanel.Find("StartGameButton").GetComponent<Button>();

        _levelPanel = _canvas.transform.Find("Level").GetComponent<RectTransform>();
        _healthText = _levelPanel.Find("HealthText").GetComponent<Text>();
        _levelStateText = _levelPanel.Find("LevelStateText").GetComponent<Text>();

        _scorePanel = _canvas.transform.Find("Score").GetComponent<RectTransform>();
        _restartGameButton = _scorePanel.Find("RestartGameButton").GetComponent<Button>();

        _levelPanel.gameObject.SetActive(false);
        _scorePanel.gameObject.SetActive(false);
    }

    public void SetGameFSM(FSMLite gameFSM)
    {
        gameFSM.GetState(Game.GameStateMenu).OnStateInEvent.AddListener(() => { _menuPanel.gameObject.SetActive(true); });
        gameFSM.GetState(Game.GameStateMenu).OnStateOutEvent.AddListener(() => { _menuPanel.gameObject.SetActive(false); });

        gameFSM.GetState(Game.GameStateLevel).OnStateInEvent.AddListener(() => { _levelPanel.gameObject.SetActive(true); });
        gameFSM.GetState(Game.GameStateLevel).OnStateOutEvent.AddListener(() => { _levelPanel.gameObject.SetActive(false); });

        gameFSM.GetState(Game.GameStateScore).OnStateInEvent.AddListener(() => { _scorePanel.gameObject.SetActive(true); });
        gameFSM.GetState(Game.GameStateScore).OnStateOutEvent.AddListener(() => { _scorePanel.gameObject.SetActive(false); });

        _startGameButton.onClick.AddListener(() =>
        {
            Game.Get().GameFSM.SetState(Game.GameStateLevel);
            Sounds.Get().PlayStartButtonSound();
        });
        _restartGameButton.onClick.AddListener(() =>
        {
            Game.Get().GameFSM.SetState(Game.GameStateLevel);
        });
    }
    
    public void SetLevelFSM(FSMLite levelFSM)
    {
        levelFSM.GetState(Game.LevelStateClose).OnStateInEvent.AddListener(OnLevelStateCloseIn);
        levelFSM.GetState(Game.LevelStateOpen).OnStateInEvent.AddListener(OnLevelStateOpenIn);
    }
    
    public void UnSetLevelFSM(FSMLite levelFSM)
    {
        levelFSM.GetState(Game.LevelStateClose).OnStateInEvent.RemoveListener(OnLevelStateCloseIn);
        levelFSM.GetState(Game.LevelStateOpen).OnStateInEvent.RemoveListener(OnLevelStateOpenIn);
    }


    void OnLevelStateCloseIn()
    {
        _levelStateText.text = "Eye Closed!"; 
    }
    void OnLevelStateOpenIn()
    {
        _levelStateText.text = "Eye Opened!"; 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ingameMenu.activeSelf) OnPause();
            else OnResume();
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }

    public void OnRestart()
    {
        //重新加载此关
        Game.Get().CurrentLevelIndex--;
        Game.Get().StartNextLevel();
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }
    public void OnreturnTotitle()
    {
        //回标题
        Game.Get().GameFSM.SetState(Game.GameStateMenu);
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }
}