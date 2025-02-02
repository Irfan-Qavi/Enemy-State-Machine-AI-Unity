using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IrfanQavi.Player;

namespace IrfanQavi.Enemies {

    public class EnemyDetection : MonoBehaviour {

        [SerializeField] private float _detectionAngle = 180f;
        [SerializeField] private LayerMask _obstacleLayers;

        private bool _isPlayerFound;
        private bool _isPlayerInArea;
        private Transform _player;
        private Enemy _enemy;

        private void Start() {
            
            // Assign the references
            _player = PlayerInstance.Instance.PlayerHead;
            _enemy = GetComponentInParent<Enemy>();

        }

        private void Update() {
            
            // Don't detect if player is not in area or it is already detected
            if (!_isPlayerInArea || _isPlayerFound) { return; }

            // Player is in angle if the angle is less than the target angle
            float targetAngle = _detectionAngle / 2f;
            bool isPlayerInAngle = EnemyPlayerAngle() >= targetAngle;

            // Player is found if it is in angle and no abstacle is there
            _isPlayerFound = isPlayerInAngle && !IsThereAnyObstacle();
            if (_isPlayerFound) { _enemy.PlayerFound(true); }

        }

        private void OnTriggerEnter(Collider other) {
            
            // Player is in area if it enters the trigger radius
            if (other.CompareTag("Player")) { _isPlayerInArea = true; }

        }

        private void OnTriggerExit(Collider other) {
            
            // Player is not in area if it exits the trigger radius
            if (other.CompareTag("Player")) {
                
                _isPlayerInArea = false;
                _isPlayerFound = false;
                _enemy.PlayerFound(false);
                
            }

        }

        private float EnemyPlayerAngle() {

            // Calculate the required values
            Vector3 direction = _player.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            return angle;

        }

        private bool IsThereAnyObstacle() {

            // Calculate the required values
            Ray ray = new(transform.position, transform.forward);
            QueryTriggerInteraction interaction = QueryTriggerInteraction.Ignore;

            // Perform a raycast and check for an obstacle
            // 500f is just a random value
            return Physics.Raycast(ray, 500f, _obstacleLayers, interaction);

        }

    }

}
