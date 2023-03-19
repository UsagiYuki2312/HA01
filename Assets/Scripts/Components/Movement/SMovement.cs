using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMovement : MonoBehaviourCore
{
    //[HideInInspector] 
    public CharacterProperties characterProperties;
    public static bool isMovable = true;
    public float defaultSpeed;
    public Rigidbody2D rb;
    public int typeMove;

    void Start()
    {
        if (isMovable)
        {
            if (typeMove == 1)
            {
                StartCoroutine(MoveForwardToPlayer());
            }
            if (typeMove == 2)
            {
                //StartCoroutine(MoveAroundPlayer());
                StartCoroutine(MoveAroundPlayerForRange());
                //StartCoroutine(AttackPlayer());
            }
            //if()

        }
    }

    protected virtual void Update()
    {
        // if (isMovable)
        // {
        //     if (typeMove == 1)
        //     {
        //         rb.velocity = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position) * characterProperties.speed;
        //     }
        //     if (typeMove == 2)
        //     {
        //         Debug.Log("Melee Walking");
        //     }
        // MoveToPlayer(SGameInstance.Instance.player.transform.position - transform.position);

    }


    public void OnSlowdownTriggered()
    {
        // defaultSpeed = characterProperties.speed;
        characterProperties.speed *= 0.7f; // reduce 30% speed
    }

    public void OnSpeedUpTriggered()
    {
        // defaultSpeed = characterProperties.speed;
        characterProperties.speed = 20f;
    }

    public void OnSlowdownFinished()
    {
        characterProperties.speed = defaultSpeed;
    }

    public void OnStopMovement()
    {
        characterProperties.speed = 0;
    }

    public void MoveToPlayer(Vector3 dir)
    {

        transform.Translate(MiniumVector(dir) * 5 * Time.deltaTime, Space.World);

    }

    public Vector3 ConvertVector(Vector3 dir)
    {
        float directionX = 0;
        float directionY = 0;
        if (dir.x == 0 && dir.y != 0)
        {
            directionY = dir.y / Mathf.Abs(dir.y);
            return new Vector3(directionX, directionY, 0);
        }
        if (dir.y == 0 && dir.x != 0)
        {
            directionX = dir.x / Mathf.Abs(dir.x);
            return new Vector3(directionX, directionY, 0);
        }
        if (dir.x != 0 && dir.y != 0)
        {
            if (dir.x < 0 && dir.y < 0)
            {
                directionY = ((dir.y / dir.x) * -1);
                directionX = -1;
            }
            if (dir.x > 0 && dir.y > 0)
            {
                directionY = (dir.y / dir.x);
                directionX = 1;
            }
            if (dir.x < 0 && dir.y > 0)
            {
                directionY = ((dir.y / dir.x) * -1);
                directionX = -1;
            }
            if (dir.x > 0 && dir.y < 0)
            {
                directionY = (dir.y / dir.x);
                directionX = 1;
            }
            return new Vector3(directionX, directionY, 0);
        }
        return Vector3.zero;
    }
    public Vector3 MiniumVector(Vector3 dir)
    {
        Vector3 vectorEquation = ConvertVector(dir);
        float equation = dir.y / dir.x;
        Vector3 goToDirection = new Vector3(0, 0, 0);
        if (vectorEquation.x >= 1)
        {
            goToDirection = new Vector3(0.5f, 0.5f * equation);
        }
        if (vectorEquation.x <= -1)
        {
            goToDirection = new Vector3(-0.5f, -0.5f * equation);
        }
        if (vectorEquation.y >= 1)
        {
            goToDirection = new Vector3(0.5f / equation, 0.5f);
        }
        if (vectorEquation.y <= -1)
        {
            goToDirection = new Vector3(-0.5f / equation, -0.5f);
        }
        //Debug.Log("ConvertVector: " + goToDirection);
        return goToDirection;
    }

    private IEnumerator MoveAroundPlayer()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) > 5f)
            {
                rb.velocity = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position) * characterProperties.speed;
            }
            else
            {
                Vector2 targetPosition = (Vector2)(SGameInstance.Instance.player.transform.position) + Random.insideUnitCircle * 3f;
                while (Vector2.Distance(transform.position, targetPosition) > 0.5f && Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) < 5f)
                {
                    rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, characterProperties.speed * Time.deltaTime * 5));
                    yield return null;
                }
            }


            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }
    }

    private IEnumerator MoveForwardToPlayer()
    {
        while (true)
        {
            rb.velocity = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position) * characterProperties.speed;
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }

    }

    private IEnumerator MoveAroundPlayerForRange()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) > 10f)
            {
                rb.velocity = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position) * characterProperties.speed;
            }
            if (Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) < 4f)
            {
                rb.velocity = MiniumVector(transform.position - SGameInstance.Instance.player.transform.position) * characterProperties.speed * 0.7f;
                Vector2 targetPosition = (Vector2)(SGameInstance.Instance.player.transform.position) + Random.insideUnitCircle * 3f;
            }


            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }
    }

    private IEnumerator MoveOut(Vector3 positionMove)
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, positionMove) <= 0.3f)
                rb.velocity = MiniumVector(transform.position - positionMove) * characterProperties.speed;
            yield return new WaitForSeconds(1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Alien")
        {
            StartCoroutine(MoveOut(collision.gameObject.transform.position));
        }
    }

    // private IEnumerator AttackPlayer()
    // {
    //     while (true)
    //     {
    //         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
    //         foreach (Collider2D collider in colliders)
    //         {
    //             if (collider.CompareTag("Player"))
    //             {
    //                 Debug.Log("Enemy attacking player");
    //                 yield return new WaitForSeconds(attackCooldown);
    //                 break;
    //             }
    //         }

    //         yield return null;
    //     }
    // }
}


// public class EnemyController : MonoBehaviour
// {
//     public float moveSpeed = 3f;
//     public float attackCooldown = 2f;
//     public float attackRange = 1f;

//     private Transform player;
//     private Rigidbody2D rb;

//     private void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//         rb = GetComponent<Rigidbody2D>();

//         StartCoroutine(MoveAroundPlayer());
//         StartCoroutine(AttackPlayer());
//     }

// }