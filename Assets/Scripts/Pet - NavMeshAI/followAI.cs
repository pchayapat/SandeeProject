using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;
    Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dest = player.position;
        ai.destination = dest;
        if(!ai.pathPending)
        {
            if(ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
            }
        }
        else
        {
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");
        }
    }
}
