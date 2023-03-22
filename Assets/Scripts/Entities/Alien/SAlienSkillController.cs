using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAlienSkillController : MonoBehaviour
{
    public SkillBossController skillController;
    private Vector3 directionSkill;
    public GameObject shootPoint;
    public GameObject dirSkill;
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        skillController = new SkillBossController();
        skillController.Init();
    }

    public void UseMeleeSkilll(Vector3 position, Quaternion rotation)
    {
        skillController.UseMeleeSkilll(position, rotation);
    }
    public void UseRangeSkill()
    {
        skillController.UseRangeSkill(shootPoint.transform.position, shootPoint.transform.rotation);
    }

    public void UseSkillBoss()
    {
        int typeSkill = Random.Range(0, 3);
        switch (typeSkill)
        {

            case 0:
                StartCoroutine(AimPlayer());
                break;

            case 1:
                StartCoroutine(FirePlayer());
                break;

            case 2:
                StartCoroutine(ShootBallPlayer());
                break;

            case 3:
                StartCoroutine(AmaretasuPlayer());
                break;
            default: break;
        }
    }


    public void Update()
    {
        Vector2 direction = SGameInstance.Instance.player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        shootPoint.transform.rotation = rotation;

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        //     bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 10;
        // }
    }

    #region Skill for boss
    IEnumerator AimPlayer()
    {
        Vector3 direction = Vector3.zero;
        float wait = 0;
        while (wait <= 2)
        {
            if (dirSkill.transform.localScale.y < 3)
            {
                dirSkill.transform.localScale += new Vector3(0, 0.2f, 0);
            }
            direction = SGameInstance.Instance.player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            dirSkill.transform.rotation = rotation;

            yield return new WaitForSeconds(0.2f);
            wait += 0.2f;
        }
        yield return new WaitForSeconds(1f);
        dirSkill.transform.localScale = new Vector3(0.5f, 0, 0);
        rigidbody.velocity = (direction * 3);

    }

    IEnumerator ShootBallPlayer()
    {
        Vector3 direction = Vector3.zero;
        float wait = 0;
        while (wait <= 2)
        {
            if (dirSkill.transform.localScale.x < 3)
            {
                dirSkill.transform.localScale += new Vector3(0.2f, 0, 0);
            }
            direction = SGameInstance.Instance.player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            dirSkill.transform.rotation = rotation;

            yield return new WaitForSeconds(0.2f);
            wait += 0.2f;
        }
        yield return new WaitForSeconds(1f);
        dirSkill.transform.localScale = new Vector3(0, 0.5f, 0);
        skillController.UseHacCauItachiSkill(transform.position, dirSkill.transform.rotation);

    }

    IEnumerator FirePlayer()
    {
        Vector3 direction = Vector3.zero;
        float wait = 0;
        while (wait <= 2)
        {
            if (dirSkill.transform.localScale.x < 1)
            {
                dirSkill.transform.localScale += new Vector3(0.2f, 0, 0);
            }
            direction = SGameInstance.Instance.player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            dirSkill.transform.rotation = rotation;

            yield return new WaitForSeconds(0.2f);
            wait += 0.2f;
        }
        yield return new WaitForSeconds(1f);
        dirSkill.transform.localScale = new Vector3(0, 0.5f, 0);
        skillController.UseHoaDonItachiSkill(transform.position, dirSkill.transform.rotation);

    }

    IEnumerator AmaretasuPlayer()
    {
        yield return new WaitForSeconds(1f);
        dirSkill.transform.localScale = new Vector3(0, 0.5f, 0);
        skillController.UseAmaretasuItachiSkill(SGameInstance.Instance.player.transform.position, SGameInstance.Instance.player.transform.rotation);

    }
    #endregion

}
