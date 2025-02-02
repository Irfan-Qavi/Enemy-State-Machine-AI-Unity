using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IrfanQavi.Enemies {

    public abstract class EnemyBaseState {

        public EnemyBaseState(Enemy enemy, EnemyStatesFactory enemyStatesFactory) {

            Enemy = enemy;
            EnemyStatesFactory = enemyStatesFactory;

        }

        public Enemy Enemy;
        public EnemyStatesFactory EnemyStatesFactory;

        public abstract void EnterState();
        public abstract void UpdateState();

        public void SwitchStates(EnemyBaseState newState) {

            // Enter the new state
            Enemy.CurrentState = newState;
            Enemy.CurrentState.EnterState();

        }

    }

}
