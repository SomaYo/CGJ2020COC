using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "placement_set", menuName = "ScriptableObjects/PlacementScripableObject", order = 1)]
    public class PlacementSet : ScriptableObject
    {
        public string Name;

        public bool HasGhost;
        public float GhostMoveSpeed; // 怪物移动速度
        public float GhostAlertRadius; //怪物警戒半径
        public int GhostDamage; //怪物伤害

        public Stuff StuffPrefab;
        public Ghost GhostPrefab;
    }
}