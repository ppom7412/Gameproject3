using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murderer : MonoBehaviour {
    [Header(" + Murderer Animations")]
    public AnimationClip attackAnim;
    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip chaseAnim;

    [Header(" + Murderer Stat")]
    [SerializeField]
    private float sensitiveArea;
    [SerializeField]
    private float attackArea;
    [SerializeField]
    private float attackDelay;

    public StateMachine<Murderer> murdererMachine;
    private Animator animator;
    private GameObject player;
    private Vector3 soundSpot;

    public void Start() {
        murdererMachine = new StateMachine<Murderer>(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        gameObject.tag = "Murderer";

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
    }

    ///
    /// - public 살인자 행동 함수들
    ///

    public void Waiting(){
        //Debug.Log(" < 기다린다 > ");
    }

    public void Attacking(){
        Debug.Log(" < 공격한다 > ");
    }

    public void Walking(){
        //gameObject.transform.position = gameObject.transform.position + (Vector3.up * 0.1f);
    }

    public void Chase(){
        // 지형이 생기면 길찾기로 변형될 것.
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, soundSpot, Time.deltaTime);
    }
}
