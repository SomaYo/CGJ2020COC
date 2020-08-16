using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Game : MonoBehaviour
{
    private static Game _Instance;

    private void OnEnable()
    {
        _Instance = this;
        Application.targetFrameRate = 60;

        DistortionMask = GameObject.Find("Distortion").gameObject;
        UI = GameObject.Find("UIManager").GetComponent<UIManager>();
        
        GameFSM = new FSMLite();
        GameFSM.RegisterState(new FSMLite.State {Name = GameStateMenu});
        GameFSM.RegisterState(new FSMLite.State {Name = GameStateLevel});
        GameFSM.RegisterState(new FSMLite.State {Name = GameStateScore});
    }

    public static Game Get()
    {
        return _Instance;
    }

    public List<Level.Level> LevelPrefabList;

    [HideInInspector] public UIManager UI;
    [HideInInspector] public Level.Level Level;
    public GameObject[] collisions = new GameObject[10];

    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    private GameObject DistortionMask;
    
    public FSMLite GameFSM;
    public const string GameStateMenu = "GameMenu";
    public const string GameStateLevel = "GameLevel";
    public const string GameStateScore = "GameScore";

    public FSMLite LevelFsm;
    public const string LevelStateOpen = "EyeOpen";
    public const string LevelStateClose = "EyeClose";

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevelIndex = 3;
        Debug.Log("level : " + CurrentLevelIndex);
        GameFSM.GetState(GameStateLevel).OnStateInEvent.AddListener(() =>
        {
            LevelFsm = new FSMLite();
            LevelFsm.RegisterState(new FSMLite.State() {Name = LevelStateOpen});
            LevelFsm.RegisterState(new FSMLite.State() {Name = LevelStateClose});
            LevelFsm.GetState(LevelStateOpen).OnStateInEvent.AddListener(() =>
            {
                DistortionMask.SetActive(false);
            });
            LevelFsm.GetState(LevelStateClose).OnStateInEvent.AddListener(() =>
            {
                DistortionMask.SetActive(true);
            });
            
            UI.SetLevelFSM(LevelFsm);
            LevelFsm.Start(LevelStateOpen);

            if (LevelPrefabList.Count > CurrentLevelIndex)
            {
                var levelPrefab = LevelPrefabList[CurrentLevelIndex];
                var levelGo = Instantiate(levelPrefab, transform);
                if (!(levelGo is null))
                {
                    Level = levelGo.GetComponent<Level.Level>();
                }
                switchCameraLimit();
            }
        });
        GameFSM.GetState(GameStateLevel).OnStateOutEvent.AddListener(() =>
        {
            UI.UnSetLevelFSM(LevelFsm);
            LevelFsm.Clear();
            LevelFsm = null;
            Destroy(Level.gameObject);
        });

        UI.SetGameFSM(GameFSM);
        
        GameFSM.Start(GameStateMenu);
    }

    private void switchCameraLimit()
    {
        foreach (var col in collisions)
        {
            if (col != null)
            {
                col.SetActive(false);
            }
        }
        collisions[CurrentLevelIndex].SetActive(true);
        virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = collisions[CurrentLevelIndex].GetComponent<PolygonCollider2D>();
    }

    [HideInInspector] public int CurrentLevelIndex;

    public void SetCameraFollowPlayer(Transform trans)
    {
        virtualCamera.Follow = trans;
    }

    public bool StartNextLevel()
    {
        CurrentLevelIndex++;

        if (LevelPrefabList.Count <= CurrentLevelIndex)
        {
            CurrentLevelIndex--;
            return false;
        }
        else
        {
            GameFSM.SetState(GameStateScore);
            GameFSM.SetState(GameStateLevel);
            return true;
        }
    }

    public int getlevelnow()
    {
        return CurrentLevelIndex;
    }
}