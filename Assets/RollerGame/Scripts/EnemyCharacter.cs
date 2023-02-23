using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]


public class EnemyCharacter : MonoBehaviour{

    [SerializeField] Animator animator;

    private NavMeshAgent navMeshAgent;

    private Camera mainCamera;

    private Transform target;

    private void Start(){

        target = GameObject.FindGameObjectWithTag("Player").transform;

        mainCamera = Camera.main;

        navMeshAgent= GetComponent<NavMeshAgent>();

		GetComponent<Health>().onDeath += OnDeath;

	}

	void OnDeath(){

		StartCoroutine(Death());

	}

	IEnumerator Death(){

		animator.SetTrigger("Death");

		yield return new WaitForSeconds(4.0f);

		Destroy(gameObject);

	}

	void Update(){

        navMeshAgent.SetDestination(target.position);

        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

    }

}
