using UnityEngine;
using System.Collections;

public class MonsterWalk : MonoBehaviour
{
    public Transform entryPoint;
    public Transform targetPoint;
    public float speed = 3f;
    public float pauseBeforeRequest = 0.4f;
    public MonsterRequest monsterRequest;
    public Animator animator;


    public IEnumerator WalkIn()
    {
        // ensure that my monster is at the entry point before starting walk cycle
        if(entryPoint)
        {
            transform.position = entryPoint.position;
        }

        // disable root motion so movement is fully controlled by script
        if (animator)
        {
            animator.applyRootMotion = false;
            animator.SetBool("IsWalking", true);
        }

        //continue stepping toward the target until "close enough"
        // the small threshold prevents jittering once within a fraction of a unit
        while(Vector2.Distance(transform.position, targetPoint.position) > 0.02f)
        {
            // ensure that my sprite is facing the correct direction when walking
            Facing(targetPoint.position.x);
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPoint.position;
        if(animator)
        {
            // go back to idle state
            animator.SetBool("IsWalking", false);
        }
    }

    public IEnumerator WalkOut()
    {
        if(animator)
        {
            animator.SetBool("IsWalking", true);
        }
        while(Vector2.Distance(transform.position, entryPoint.position) > 0.02f)
        {
            Facing(entryPoint.position.x);  
            transform.position = Vector2.MoveTowards(transform.position, entryPoint.position, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = entryPoint.position;
        if (animator)
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void Facing(float targetX)
    {
        // flip the sprite horizontally by adjusting localScale.x
        // keeps absolute scale constant to advoid distortions
        float dir = Mathf.Sign(targetX - transform.position.x);
        var s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (dir == 0 ? 1f : dir);
        transform.localScale = s;
    }
}
