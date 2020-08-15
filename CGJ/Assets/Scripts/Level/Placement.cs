using UnityEngine;

namespace Level
{
    public class Placement : MonoBehaviour
    {
        public PlacementSet PlacementSet;
    
        private Stuff _stuff;
        private Ghost _ghost;

        private void OnEnable()
        {
            _stuff = Instantiate(PlacementSet.StuffPrefab, transform).GetComponent<Stuff>();
            _ghost = Instantiate(PlacementSet.GhostPrefab, transform).GetComponent<Ghost>();
        }

        void Start()
        {
        }

        private int _mode;
        public const int ModeStuff = 0x01;
        public const int ModeGhost = 0x10;

        public void SetMode(int mode)
        {
            switch (mode)
            {
                case ModeStuff:
                    _stuff.SetActive(true);
                    _ghost.SetActive(false);
                    break;

                case ModeGhost:
                    _stuff.SetActive(false);
                    _ghost.SetActive(true);
                    break;
            }
        }
    }
}