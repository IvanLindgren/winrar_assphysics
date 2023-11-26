using System;
using UnityEngine;

namespace WinRar.Game
{
    public class Trigger : MonoBehaviour
    {
        public Action<Collider2D> OnTriggerEnter;
        public Action<Collider2D> OnTriggerExit;
        
        private void OnTriggerEnter2D(Collider2D col) => OnTriggerEnter?.Invoke(col);
        private void OnTriggerExit2D(Collider2D col) => OnTriggerExit?.Invoke(col);
    }
}