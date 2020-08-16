using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = Unity.Mathematics.Random;

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

       [Serializable]
       private struct ShowAfterBreak
        {
            public bool UseCustomTransform;
            public Vector3 ShowPosition;
            public Quaternion ShowRotation;
            public Transform ShowObject;
        }

       [SerializeField]
        private ShowAfterBreak showAfterBreak;

        private void OnEnable()
        {
            _alertCollider2D = transform.Find("AlertRange").GetComponent<CircleCollider2D>();
            _alertCollider2D.radius = PlacementSet.GhostAlertRadius;

            _aiDestSetter = GetComponent<AIDestinationSetter>();
            _aiPath = GetComponent<AIPath>();

            _stuff = GetComponentsInChildren<Stuff>().Length > 0 ? GetComponentInChildren<Stuff>() :
                PlacementSet.StuffPrefab != null ? Instantiate(PlacementSet.StuffPrefab, transform).GetComponent<Stuff>() : null;
            _ghost = GetComponentsInChildren<Ghost>().Length > 0 ? GetComponentInChildren<Ghost>() :
                PlacementSet.GhostPrefab != null ? Instantiate(PlacementSet.GhostPrefab, transform).GetComponent<Ghost>() : null;
            
            SetDropAble(!PlacementSet.IsFloating);
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
                    if (_stuff != null)
                    {
                        _stuff.SetActive(true);
                        _stuff.transform.localPosition = Vector3.zero;
                    }

                    if (_ghost != null)
                    {
                        _ghost.SetActive(false);
                    }

                    _aiPath.maxSpeed = 0;
                    break;

                case ModeGhost:
                    if (_stuff != null)
                    {
                        _stuff.SetActive(false);
                    }

                    if (_ghost != null)
                    {
                        _ghost.SetActive(true);
                        _ghost.transform.localPosition = Vector3.zero;
                    }

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

        public void SetDropAble(bool flag)
        {
            GetComponent<Rigidbody2D>().gravityScale = flag ? 90 : 0;
        }

        public bool IsBreakAble()
        {
            return PlacementSet.BreakAble;
        }

        private Random _random = new Random(1);

        public void Break()
        {
            if (PlacementSet.BreakAble)
            {
                if (_stuff != null)
                {
                    var explodable = _stuff.gameObject.AddComponent<Explodable>();
                    var collider2d = _stuff.gameObject.AddComponent<BoxCollider2D>();
                    explodable.allowRuntimeFragmentation = true;
                    explodable.fragmentLayer = "Breaks";
                    explodable.extraPoints = 10;
                    explodable.orderInLayer = 3;
                    explodable.explode();
                    Sounds.Get().PlayDestructionSound();

                    var fragments = explodable.fragments;
                    foreach (var f in fragments)
                    {
                        Destroy(f.GetComponent<PolygonCollider2D>(), _random.NextFloat(1.0f, 2.5f));
                        Destroy(f, 3f);
                    }

                    if (showAfterBreak.ShowObject != null)
                    {
                        if (showAfterBreak.UseCustomTransform)
                        {
                            Instantiate(showAfterBreak.ShowObject, showAfterBreak.ShowPosition, showAfterBreak.ShowRotation);
                        }
                        else
                        {
                            Instantiate(showAfterBreak.ShowObject);
                        }
                    }
                    Destroy(gameObject, 2.0f);
                }
            }
        }
    }
}