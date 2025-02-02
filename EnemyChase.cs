using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IrfanQavi.Enemies.States {

    public class EnemyChase : EnemyBaseState {

        public EnemyChase(Enemy enemy, EnemyStatesFactory enemyStatesFactory) : base(enemy, enemyStatesFactory) {}

        public override void EnterState() {

            // Reset the path and set the speed
            Enemy.Agent.ResetPath();
            Enemy.Agent.speed = Enemy.ChaseSpeed;

        }

        public override void UpdateState() {

            // Don't go ahead if we cannot attack
            if (!Enemy.CanAttack) { return; }
            
            // Check if we can attack
            if (Enemy.CanAttack) {

                float remainingDistance = Vector3.Distance(Enemy.transform.position, Enemy.PlayerGround.position);
                if (remainingDistance <= (Enemy.Agent.stoppingDistance + Enemy.ExtraAttackStoppingDistance)) {

                    // Switch the state and reset everything
                    SwitchStates(EnemyStatesFactory.Attack());
                    Enemy.CurrentWayPoint = null;
                    Enemy.Agent.ResetPath();
                    Enemy.CanAttack = false;

                }

            }

            // Continuously chase the player
            GoTo(Enemy.PlayerGround);
            
        }

        private void GoTo(Transform point) { Enemy.Agent.SetDestination(point.position); }

    }

}
