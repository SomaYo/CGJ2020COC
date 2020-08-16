using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class EndZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Game.Get().StartNextLevel();
            }
        }
    }
}