using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IrfanQavi.Enemies.States {

    public class EnemyPatrol : EnemyBaseState {

        public EnemyPatrol(Enemy enemy, EnemyStatesFactory enemyStatesFactory) : base(enemy, enemyStatesFactory) {}

        public override void EnterState() {

            // Set the speed and way point then finally go to the way point
            Enemy.Agent.speed = Enemy.PatrolSpeed;
            SetWayPoint();
            GoTo(Enemy.CurrentWayPoint);
            
        }

        public override void UpdateState() {

            // Check if we have reached the way point
            if (Enemy.Agent.hasPath && Enemy.CurrentWayPoint != null) {

                float remainingDistance = Vector3.Distance(Enemy.transform.position, Enemy.CurrentWayPoint.position);
                if (remainingDistance <= (Enemy.Agent.stoppingDistance + Enemy.ExtraStoppingDistance)) {

                    // Reset everything and switch the state
                    Enemy.CurrentWayPoint = null;
                    Enemy.Agent.ResetPath();
                    SwitchStates(EnemyStatesFactory.Wait());

                }

            }

        }


        private void SetWayPoint() {

            // Choose a way point to go to
            int index = Random.Range(0, Enemy.WayPoints.Length);
            Transform wayPoint = Enemy.WayPoints[index];

            // Change the way point if it's the same
            if (wayPoint == Enemy.CurrentWayPoint) {

                if (index == 0) { index++; }
                else if (index == Enemy.WayPoints.Length - 1) { index--; }
                else { index++; }

            }

            // Set the way point
            wayPoint = Enemy.WayPoints[index];
            Enemy.CurrentWayPoint = wayPoint;

        }

        private void GoTo(Transform location) { Enemy.Agent.SetDestination(location.position); }

    }

}
