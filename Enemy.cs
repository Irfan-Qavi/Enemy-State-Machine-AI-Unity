using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using IrfanQavi.Player;

namespace IrfanQavi.Enemies {

    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour {

        [Header("Waiting")]
        public float MinWaitTime = 1f;
        public float MaxWaitTime = 5f;

        [Header("Patrolling")]
        public Transform[] WayPoints;
        public float PatrolSpeed = 5f;
        public float ExtraStoppingDistance = 1.5f;

        [Header("Chasing")]
        public float ChaseSpeed = 6.5f;

        [Header("Attacking")]
        public float AttackDelay = 1f;
        public float ExtraAttackStoppingDistance = 2f;

        private EnemyBaseState _currentState;
        public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

        private EnemyStatesFactory _enemyStatesFactory;

        public Transform Player { get; private set; }
        public Transform PlayerGround { get; private set; }
        public bool IsPlayerFound { get; private set; }

        [HideInInspector] public NavMeshAgent Agent;
        [HideInInspector] public Transform CurrentWayPoint;
        [HideInInspector] public bool CanAttack;
        [HideInInspector] public Coroutine LastAttackRoutine;

        private void Start() {

            // Assign the references
            Player = PlayerInstance.Instance.PlayerArmature;
            PlayerGround = PlayerInstance.Instance.GroundStick;
            Agent = GetComponent<NavMeshAgent>();
            _enemyStatesFactory = new EnemyStatesFactory(this);

            // By default, state will be Wait
            _currentState = _enemyStatesFactory.Wait();
            _currentState.EnterState();

        }

        private void Update() {

            // Update the current state
            _currentState.UpdateState();

        }

        public void PlayerFound(bool found) {

            IsPlayerFound = found;

            // If Player is found, chase him else wait
            if (found) { _currentState.SwitchStates(_enemyStatesFactory.Chase()); }
            else { _currentState.SwitchStates(_enemyStatesFactory.Wait()); }

        }

    }

}
