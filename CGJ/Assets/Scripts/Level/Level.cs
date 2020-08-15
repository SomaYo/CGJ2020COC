using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class Level : MonoBehaviour
    {
        [HideInInspector]
        public Player Player;

        private GameObject Platform;
        private GameObject Placements;
        private List<Placement> PlacementList;

        private void OnEnable()
        {
            Placements = transform.Find("Placements").gameObject;
            Platform = transform.Find("Platforms").gameObject;
            Player = GetComponentInChildren<Player>();
        }

        // Start is called before the first frame update
        void Start()
        {
            PlacementList = new List<Placement>(Placements.GetComponentsInChildren<Placement>());

            Game.Get().LevelFsm.GetState(Game.LevelStateOpen).OnStateInEvent.AddListener(() =>
            {
                foreach (var placement in PlacementList)
                {
                    placement.SetMode(Placement.ModeGhost);
                }
            });
            Game.Get().LevelFsm.GetState(Game.LevelStateClose).OnStateInEvent.AddListener(() =>
            {
                foreach (var placement in PlacementList)
                {
                    placement.SetMode(Placement.ModeStuff);
                }
            });

            Game.Get().LevelFsm.Start(Game.LevelStateOpen);
        }

        // Update is called once per frame
        void Update()
        {
            // TODO: remove test code
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (Game.Get().LevelFsm.GetState().Name.Equals(Game.LevelStateOpen))
                {
                    Game.Get().LevelFsm.SetState(Game.LevelStateClose);
                }
                else
                {
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