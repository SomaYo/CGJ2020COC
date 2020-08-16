using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class Level : MonoBehaviour
    {
        [HideInInspector]
        public Player Player;

        private GameObject _platform;
        private GameObject _placements;
        private List<Placement> _placementList;

        private GameObject _lightPlace;
        private GameObject _darkPlace;
        
        private void OnEnable()
        {
            _lightPlace = transform.Find("PlaceLight").gameObject;
            _darkPlace = transform.Find("PlaceDark").gameObject;
            _placements = transform.Find("Placements").gameObject;
            _platform = transform.Find("Platforms").gameObject;
            Player = GetComponentInChildren<Player>();
            Game.Get().SetCameraFollowPlayer(Player.transform);
        }

        // Start is called before the first frame update
        void Start()
        {
            _placementList = new List<Placement>(_placements.GetComponentsInChildren<Placement>());
            foreach (var p in _placementList)
            {
                p.SetPlayer(Player);
                p.SetMode(Placement.ModeStuff);
            }

            Game.Get().LevelFsm.GetState(Game.LevelStateOpen).OnStateInEvent.AddListener(() =>
            {
                foreach (var placement in _placementList)
                {
                    placement.SetMode(Placement.ModeStuff);
                    // show light place
                    _lightPlace.SetActive(true);
                    _darkPlace.SetActive(false);
                }
            });
            Game.Get().LevelFsm.GetState(Game.LevelStateClose).OnStateInEvent.AddListener(() =>
            {
                foreach (var placement in _placementList)
                {
                    placement.SetMode(Placement.ModeGhost);
                    // show dark place
                    _lightPlace.SetActive(false);
                    _darkPlace.SetActive(true);
                }
            });

            Game.Get().LevelFsm.Start(Game.LevelStateOpen);
        }

        // Update is called once per frame
        void Update()
        {
            // TODO: remove test code
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                if (Game.Get().LevelFsm.GetState().Name.Equals(Game.LevelStateOpen))
                {
                    Player.Get().SwitchPlayerShow(isDark:true);
                   Game.Get().LevelFsm.SetState(Game.LevelStateClose);
                }
                else
                {
                    Player.Get().SwitchPlayerShow(isDark: false);
                    Game.Get().LevelFsm.SetState(Game.LevelStateOpen);
                }
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Game.Get().GameFSM.SetState(Game.GameStateScore);
            }
        }
    }
}