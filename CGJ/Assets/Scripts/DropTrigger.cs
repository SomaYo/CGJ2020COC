using System;
using Level;
using UnityEngine;

public class DropTrigger : MonoBehaviour
{
    public Placement Placement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Placement != null && Placement.PlacementSet.IsFloating)
            {
                Debug.Log(other.name);
                Debug.Log(Placement);
                Debug.Log(Placement.PlacementSet.IsFloating);
                Placement.SetDropAble(true);
                Placement.PlacementSet.IsFloating = false;
                Destroy(gameObject, .5f);
            }
        }
    }
}