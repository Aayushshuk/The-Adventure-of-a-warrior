using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform Enemy;

    [Header("Movmeent Paramter")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool MovingLeft;

    [Header("Idle Paramter")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Animator Parameter")]
    [SerializeField] private Animator anim;
    private void Update()
    {
        if (MovingLeft)
        {
            if (Enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
            
        }
        else
        {
            if (Enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
            
        }
       
    }
    private void DirectionChange()
    {
        anim.SetBool("moving",false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            MovingLeft = !MovingLeft;
        }
      
    }
    private void Awake()
    {
        initScale = transform.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        Enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * _direction * speed,
            Enemy.position.y, Enemy.position.z);
    }
}
