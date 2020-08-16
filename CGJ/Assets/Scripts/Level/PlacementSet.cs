using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "placement_set", menuName = "ScriptableObjects/PlacementScripableObject", order = 1)]
    public class PlacementSet : ScriptableObject
    {
        public string Name;

        public bool HasGhost;
        public bool BreakAble;
        public float GhostMoveSpeed; // 怪物移动速度
        public float GhostAlertRadius; //怪物警戒半径

        public Stuff StuffPrefab;
        public Ghost GhostPrefab;
    }
}