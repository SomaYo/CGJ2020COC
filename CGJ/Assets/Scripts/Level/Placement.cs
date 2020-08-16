using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level
{
    public class Placement : MonoBehaviour
    {
        public PlacementSet PlacementSet;

        private CircleCollider2D _alertCollider2D;
        
        private AIDestinationSetter _aiDestSetter;
        private AIPath _aiPath;
        
        private Stuff _stuff;
        private Ghost _ghost;

        private void OnEnable()
        {
            _alertCollider2D = transform.Find("AlertRange").GetComponent<CircleCollider2D>();
            _alertCollider2D.radius = PlacementSet.GhostAlertRadius;
            
            _aiDestSetter = GetComponent<AIDestinationSetter>();
            _aiPath = GetComponent<AIPath>();

            _stuff = GetComponentsInChildren<Stuff>().Length > 0 ? GetComponentInChildren<Stuff>() : Instantiate(PlacementSet.StuffPrefab, transform).GetComponent<Stuff>();
            _ghost = GetComponentsInChildren<Ghost>().Length > 0 ? GetComponentInChildren<Ghost>() : Instantiate(PlacementSet.GhostPrefab, transform).GetComponent<Ghost>();
        }

        void Start()
        {
        }

        private int _mode;
        public const int ModeStuff = 0x01;
        public const int ModeGhost = 0x10;

        public void SetMode(int mode)
        {
            _mode = mode;
            switch (_mode)
            {
                case ModeStuff:
                    _stuff.SetActive(true);
                    _stuff.transform.localPosition = Vector3.zero;
                    _ghost.SetActive(false);
                    _aiPath.maxSpeed = 0;
                    break;

                case ModeGhost:
                    _stuff.SetActive(false);
                    _ghost.SetActive(true);
                    _ghost.transform.localPosition = Vector3.zero;
                    if (_isPlayerInRange)
                    {
                        _aiPath.maxSpeed = PlacementSet.GhostMoveSpeed;
                    }
                    break;
            }
        }

        private Player _player;
        public void SetPlayer(Player player)
        {
            _aiDestSetter.target = player.transform;
        }

        private bool _isPlayerInRange;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = true;
                if (_mode == ModeGhost)
                {
                    _aiPath.maxSpeed = PlacementSet.GhostMoveSpeed;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                _aiPath.maxSpeed = 0;
            }
        }

        public bool IsBreakAble()
        {
            return PlacementSet.BreakAble;
        }
        
        public void Break()
        {
            if (PlacementSet.BreakAble)
            {
                var explodable = _stuff.gameObject.AddComponent<Explodable>();
                var collider2d = _stuff.gameObject.AddComponent<BoxCollider2D>();
                explodable.allowRuntimeFragmentation = true;
                explodable.extraPoints = 10;
                explodable.orderInLayer = 3;
                explodable.explode();
            
                Destroy(gameObject, 2.0f);
            }
        }
    }
}