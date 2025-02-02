using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IrfanQavi.Enemies.States {

    public class EnemyWait : EnemyBaseState {

        public EnemyWait(Enemy enemy, EnemyStatesFactory enemyStatesFactory) : base(enemy, enemyStatesFactory) {}

        public override void EnterState() {

            // Calculate the wait time and then call the wait coroutine
            float waitingTime = Random.Range(Enemy.MinWaitTime, Enemy.MaxWaitTime);
            Enemy.StartCoroutine(Wait(waitingTime));
            
        }

        private IEnumerator Wait(float time) {

            // Wait for the desired amount of time and then switch the state
            yield return new WaitForSeconds(time);
            SwitchStates(EnemyStatesFactory.Patrol());

        }

        #region Unused Functions
        public override void UpdateState() {}
        #endregion

    }

}
