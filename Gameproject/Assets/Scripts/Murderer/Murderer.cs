using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Murderer : MonoBehaviour {
    [SerializeField]
    private float sensitiveArea;
    [SerializeField]
    private float attackArea;
    [SerializeField]
    private float attackDelay;

    public GameObject[] wayPoints;
    public StateMachine<Murderer> murdererMachine;
    public Animator animator;
    public NavMeshAgent agent;

    private GameObject player;
    private Vector3 soundSpot;
    private int currWaypoint;

    public void Start() {
        murdererMachine = new StateMachine<Murderer>(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        gameObject.tag = "Murderer";
        currWaypoint = -1;

        if (player == null)
            ErrorAdmin.ErrorMessegeFromObject("Don't Found Object With Tag is Player", "Start()", gameObject);

        // StateMachine initialization
        murdererMachine.SetGlobalState(new GlobalState());
        murdererMachine.SetPreviousState(new State<Murderer>());
        murdererMachine.SetCurrentState(new Wait());
    }

    void Update() {
        murdererMachine.Update();
    }

    public void SetSoundSpot(Vector3 _spot) {
        soundSpot = _spot;
    }

    public void ResetSoundSpot()
    {
        soundSpot = Vector3.zero;
    }

    public float GetAttackDelay() {
        return attackDelay;
    }

    public bool CheckAttackArea() {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distance < attackArea)
            return true;

        return false;
    }

    public bool CheckSensitiveArea() {
        if (soundSpot == Vector3.zero) return false;

        float distance = Vector3.Distance(soundSpot, gameObject.transform.position);

        if (distance < sensitiveArea)
            return true;

        return false;
    }

    public bool CheckMinDistanse() {
        if (soundSpot == Vector3.zero) return false;

        float distance = Vector3.Distance(soundSpot, gameObject.transform.position);

        if (distance < 0.5f)
            return true;

        return false;
    }

    public void UpdateSpot(Vector3 spot) {
        soundSpot = spot;
        agent.SetDestination(soundSpot);
    }

    public void FoundWayPoint()
    {
        if (wayPoints == null)
        {
            ErrorAdmin.WarningMessegeFromObject("Don't setting wayPoints", "FoundWayPoint()", gameObject);
            return;
        }

        if (currWaypoint == -1)
        {
            float mindistance = 1000.0f;
            float tempdistance = 0;
            int savepoint = 0;
            for (int i =0; i < wayPoints.Length; ++i){
                tempdistance = Vector3.Distance(transform.position, wayPoints[i].transform.position);
                if (tempdistance < mindistance){
                    savepoint = i;
                    mindistance = tempdistance;
                }
            }
            currWaypoint = savepoint;
            agent.SetDestination(wayPoints[currWaypoint].transform.position);
        }
    }

    public void Walking()
    {
        if (wayPoints == null)
        {
            ErrorAdmin.WarningMessegeFromObject("Don't setting wayPoints", "FoundWayPoint()", gameObject);
            return;
        }

        //참이면 다음 웨이포인트로
        if (CheckToCloseWayPoint())
        {
            Debug.Log("Arrive Waypoint["+currWaypoint+"]");
            currWaypoint++;
            if (currWaypoint >= wayPoints.Length)
                currWaypoint = 0;

            agent.SetDestination(wayPoints[currWaypoint].transform.position);
        }
    }

    private bool CheckToCloseWayPoint()
    {
        if (wayPoints == null)
        {
            ErrorAdmin.WarningMessegeFromObject("Don't setting wayPoints", "FoundWayPoint()", gameObject);
            return false;
        }

        if ((Vector3.Distance(transform.position, wayPoints[currWaypoint].transform.position) < 0.15))
            return true;

        return false;
    }

    //크게 경로를 이탈하는 경우가 있을 때 항상 써주자.
    public void ResetWaypoint()
    {
        currWaypoint = -1;
    }

    public void Attacking(){
        Debug.Log(" < 공격한다 > ");
        gameObject.transform.LookAt(player.transform);
    }

}
