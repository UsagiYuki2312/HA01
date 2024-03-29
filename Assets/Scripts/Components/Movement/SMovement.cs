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
    public Animator anim;
    public SAlienSkillController alienSkillController;


    void Start()
    {
        if (isMovable)
        {
            if (typeMove == 0)
            {
                StartCoroutine(MoveForwardToPlayerForBoss());
            }
            if (typeMove == 1)
            {
                StartCoroutine(MoveForwardToPlayer());
            }
            if (typeMove == 2)
            {
                StartCoroutine(MoveForwardToPlayer());
                StartCoroutine(AttackPlayer());
            }
            if (typeMove == 3)
            {
                StartCoroutine(MoveAroundPlayerForRange());
            }

        }
    }

    protected virtual void Update()
    {

    }


    public void OnSlowdownTriggered()
    {
        // defaultSpeed = characterProperties.speed;
        characterProperties.speed *= 0.7f; // reduce 30% speed
    }

    public void OnSpeedUpTriggered()
    {
        defaultSpeed = characterProperties.speed;
        //characterProperties.speed = 20f;
    }

    public void OnStopFinished()
    {
        characterProperties.speed = defaultSpeed;
    }

    public void OnStopMovement()
    {
        defaultSpeed = characterProperties.speed;
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
                Vector3 dirMoveToPlayer = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position);
                if (dirMoveToPlayer.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1) * 3;
                }
                if (dirMoveToPlayer.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1) * 3;
                }
                rb.velocity = dirMoveToPlayer * characterProperties.speed;

            }
            else
            {
                Vector2 targetPosition = (Vector2)(SGameInstance.Instance.player.transform.position) + Random.insideUnitCircle * 3f;
                while (Vector2.Distance(transform.position, targetPosition) > 0.5f && Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) < 5f)
                {
                    FlipLocalScale(((Vector3)(targetPosition) - transform.position));
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
            Vector3 dirMove = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position);
            if (dirMove.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1) * 3;
            }
            if (dirMove.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1) * 3;
            }

            if (Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) < 1f)
            {
                rb.velocity = -dirMove * characterProperties.speed;
            }
            if (Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) > 3f)
            {
                rb.velocity = dirMove * characterProperties.speed;
            }



            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }

    }

    private IEnumerator MoveAroundPlayerForRange()
    {
        while (true)
        {
            rb.velocity = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position) * characterProperties.speed;

            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1) * 3;
            }
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1) * 3;
            }


            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    anim.Play("Attack");
                    alienSkillController.UseRangeSkill();
                    yield return new WaitForSeconds(1f);
                    anim.Play("Run");
                    break;
                }
            }


            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }
    }

    private IEnumerator MoveOut(Vector3 positionMove)
    {
        while (Vector2.Distance(transform.position, positionMove) <= 0.3f)
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

    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    anim.Play("Attack");
                    yield return new WaitForSeconds(1f);
                    anim.Play("Run");
                    yield return new WaitForSeconds(2f);
                    break;
                }
            }

            yield return null;
        }
    }

    private IEnumerator ShootPlayer()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 6);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    //OnStopMovement();
                    anim.Play("Attack");
                    //alienSkillController.UseRangeSkill(transform.position, transform.rotation);
                    yield return new WaitForSeconds(1f);
                    anim.Play("Run");
                    //OnStopFinished();
                    break;
                }
            }

            yield return null;
        }
    }

    public void FlipLocalScale(Vector3 dir)
    {
        if (dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1) * 3;
        }
        if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1) * 3;
        }
    }

    private IEnumerator MoveForwardToPlayerForBoss()
    {
        while (true)
        {
            Vector3 dirMove = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position);
            if (dirMove.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1) * 3;
            }
            if (dirMove.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1) * 3;
            }

            if (Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) < 1f)
            {
                rb.velocity = -dirMove * characterProperties.speed;
            }
            if (Vector2.Distance(transform.position, SGameInstance.Instance.player.transform.position) > 3f)
            {
                rb.velocity = dirMove * characterProperties.speed;
            }



            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }

    }
}

