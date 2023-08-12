using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCbase : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;
    public float speed = 2f;
    public float rotSpeed = 1f;
    public float accuracy = 3f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        opponent = NPC.GetComponent<TankAI>().getPlayer();
    }
}
