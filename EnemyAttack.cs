using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IrfanQavi.Enemies.States {

    public class EnemyAttack : EnemyBaseState {

        public EnemyAttack(Enemy enemy, EnemyStatesFactory enemyStatesFactory) : base(enemy, enemyStatesFactory) {}

        public override void EnterState() {

            // Stop the previous coroutine if there
            if (Enemy.LastAttackRoutine != null) { Enemy.StopCoroutine(Enemy.LastAttackRoutine); }

            // Play the coroutine
            Enemy.LastAttackRoutine = Enemy.StartCoroutine(AttackAndWait());

        }

        public override void UpdateState() {
            
            // Check if enemy is outside of range
            float remainingDistance = Vector3.Distance(Enemy.transform.position, Enemy.Player.position);
            if (remainingDistance >= (Enemy.Agent.stoppingDistance + Enemy.ExtraAttackStoppingDistance)) {

                // Enemy is away so chase him back
                SwitchStates(EnemyStatesFactory.Chase());
                Enemy.Agent.ResetPath();
                Enemy.CanAttack = true;

            }

            else {

                // Attack again if he is in range
                Enemy.LastAttackRoutine = Enemy.StartCoroutine(AttackAndWait());

            }
            
        }

        private IEnumerator AttackAndWait() {

            // Attack and then wait for the delay
            Attack();

            yield return new WaitForSeconds(Enemy.AttackDelay);
            Enemy.LastAttackRoutine = null;

        }

        private void Attack() {
            
            // Look at the player before attacking but ignore the y position
            Vector3 lookTarget = new(Enemy.Player.position.x, Enemy.transform.position.y, Enemy.Player.position.z);
            Enemy.transform.LookAt(lookTarget);

            Debug.Log("Attacking...");
            
        }

    }

}
