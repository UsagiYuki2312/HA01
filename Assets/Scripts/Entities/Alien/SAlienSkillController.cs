using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAlienSkillController : MonoBehaviour
{
    public SkillBossController skillController;
    private Vector3 directionSkill;
    public GameObject shootPoint;
    public GameObject dirSkill;
    public Rigidbody2D rigidbody;
    public Animator anim;
    public SMovement movement;
    public AlienProperties properties;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        skillController = new SkillBossController();
        skillController.Init();
        skillController.UpdatePowerSkill(1,300);
        skillController.UpdatePowerSkill(2,200);
        skillController.UpdatePowerSkill(3,20);
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
        int typeSkill = Random.Range(1, 4);
        switch (typeSkill)
        {


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

    Vector2 directionAim;
    Quaternion rotationAim;

    public void Update()
    {
        directionAim = SGameInstance.Instance.player.transform.position - transform.position;
        float angle = Mathf.Atan2(directionAim.y, directionAim.x) * Mathf.Rad2Deg;
        rotationAim = Quaternion.Euler(new Vector3(0, 0, angle));
        shootPoint.transform.rotation = rotationAim;

    }

    #region Skill for boss
    IEnumerator ShootBallPlayer()
    {
        movement.enabled = false;
        float wait = 0;
        while (wait <= 2)
        {
            dirSkill.gameObject.SetActive(true);
            dirSkill.transform.rotation = rotationAim;
            yield return new WaitForSeconds(0.05f);
            wait += 0.2f;
        }
        anim.Play("MagicAttack");
        yield return new WaitForSeconds(0.5f);
        dirSkill.gameObject.SetActive(false);
        skillController.UseHacCauItachiSkill(transform.position, dirSkill.transform.rotation);
        movement.enabled = true;
    }

    IEnumerator FirePlayer()
    {
        movement.enabled = false;
        float wait = 0;
        while (wait <= 2)
        {
            dirSkill.gameObject.SetActive(true);
            dirSkill.transform.rotation = rotationAim;
            yield return new WaitForSeconds(0.05f);
            wait += 0.2f;
        }
        dirSkill.gameObject.SetActive(false);
        anim.Play("FireSkill");
        yield return new WaitForSeconds(0.8f);
        skillController.UseHoaDonItachiSkill(transform.position, dirSkill.transform.rotation);
        movement.enabled = true;
    }

    IEnumerator AmaretasuPlayer()
    {
        movement.enabled = false;
        anim.Play("MagicAttack");
        yield return new WaitForSeconds(0.75f);
        dirSkill.transform.localScale = new Vector3(0, 0.5f, 0);
        skillController.UseAmaretasuItachiSkill(SGameInstance.Instance.player.transform.position, SGameInstance.Instance.player.transform.rotation);
        movement.enabled = true;
    }
    #endregion

}
