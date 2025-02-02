using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IrfanQavi.Enemies.States;

namespace IrfanQavi.Enemies {

    public class EnemyStatesFactory {

        private Enemy _enemy;

        public EnemyStatesFactory(Enemy enemy) { _enemy = enemy; }

        public EnemyWait Wait() { return new EnemyWait(_enemy, this); }
        public EnemyPatrol Patrol() { return new EnemyPatrol(_enemy, this); }
        public EnemyChase Chase() { return new EnemyChase(_enemy, this); }
        public EnemyAttack Attack() { return new EnemyAttack(_enemy, this); }

    }

}
