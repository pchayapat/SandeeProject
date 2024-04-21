using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangeChecker : MonoBehaviour
{   
    public Transform targetObject;
    public float outOfRangeDistance = 7f;
    public float outOfRangeTime = 3f;
    public float moveSpeed = 5f;
    public float stopDistance = 3f;

    private float timeOutOfRange = 0f;
    private bool isOutOfRange = false;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);
        if (distance > outOfRangeDistance) // Change 10f to the desired range
        {
            timeOutOfRange += Time.deltaTime;
            if (!isOutOfRange && timeOutOfRange >= outOfRangeTime)
            {
                Debug.Log("Out");
                isOutOfRange = true;
                StartCoroutine(MoveToTarget());
            }
        }
        else
        {
            timeOutOfRange = 0f;
            isOutOfRange = false;
        }

        if (distance <= stopDistance)
        {
            StopMoving();
        }
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, targetObject.position) > stopDistance)
        {
            animator.SetBool("isMoving",true);
            transform.LookAt(targetObject.position);
            transform.position = Vector3.MoveTowards(transform.position, targetObject.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void StopMoving()
    {
        animator.SetBool("isMoving",false);
        StopCoroutine(MoveToTarget());
    }
}
