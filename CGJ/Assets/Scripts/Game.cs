using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game _Instance;

    private void OnEnable()
    {
        _Instance = this;

        UI = GameObject.Find("UIManager").GetComponent<UIManager>();

        GameFSM = new FSMLite();
        GameFSM.RegisterState(new FSMLite.State {Name = GameStateMenu});
        GameFSM.RegisterState(new FSMLite.State {Name = GameStateLevel});
        GameFSM.RegisterState(new FSMLite.State {Name = GameStateScore});
        
        Debug.DrawRay(new Vector3(-2,-2,-2), Vector3.up, Color.green, 3);
    }

    public static Game Get()
    {
        return _Instance;
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(-1,-1,-1), Vector3.up*10);
    }

    public List<Level.Level> LevelPrefabList;

    [HideInInspector]
    public UIManager UI;
    [HideInInspector]
    public Level.Level Level;

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
        GameFSM.GetState(GameStateLevel).OnStateInEvent.AddListener(() =>
        {
            LevelFsm = new FSMLite();
            LevelFsm.RegisterState(new FSMLite.State() {Name = LevelStateOpen});
            LevelFsm.RegisterState(new FSMLite.State() {Name = LevelStateClose});
            UI.SetLevelFSM(LevelFsm);

            if (LevelPrefabList.Count > 0)
            {
                var levelPrefab = LevelPrefabList.First();
                var levelGo = Instantiate(levelPrefab, transform);
                if (!(levelGo is null))
                {
                    Level = levelGo.GetComponent<Level.Level>();
                }
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
}