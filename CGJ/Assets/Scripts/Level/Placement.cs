﻿using System;
using Pathfinding;
using UnityEngine;

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
            _mode = mode;
            switch (_mode)
            {
                case ModeStuff:
                    _stuff.SetActive(true);
                    _ghost.SetActive(false);
                    _aiPath.maxSpeed = 0;
                    break;

                case ModeGhost:
                    _stuff.SetActive(false);
                    _ghost.SetActive(true);
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
    }
}