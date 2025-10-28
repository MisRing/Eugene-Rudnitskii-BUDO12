using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerConditions : MonoBehaviour
    {
        private float _interrupted;
        private float _invulnerable;
        
        public bool IsInterrupted => _interrupted > 0;
        public  bool IsInvulnerable => _invulnerable > 0;
        
        public event Action OnInterrupted;
        public event Action OnHighFall;

        private void Update()
        {
            _interrupted -= Time.deltaTime;
            _invulnerable -= Time.deltaTime;
        }
        
        public void Interrupt(float time)
        {
            _interrupted = time;
            
            OnInterrupted?.Invoke();
        }

        public void Invulnerable(float time) => _invulnerable = time;
        public void InvokeOnHighFall() => OnHighFall?.Invoke();
    }
}
