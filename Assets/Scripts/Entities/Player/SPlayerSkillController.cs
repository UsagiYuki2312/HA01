using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class SPlayerSkillController : MonoBehaviour
{
    public SSkillJoytickPanel skillPanel;
    private Vector3 dirFirstSkill;
    private Vector3 dirSeconndSkill;
    private Vector3 dirThirdSkill;
    private float rotateAngleFirstSkill;
    private float rotateAngleSecondSkill;
    private float rotateAngleThirdSkill;
    public bool isRotateFirstSkill;
    public bool isRotateSecondSkill;
    public bool isRotateThirdSkill;
    public IEnumerator normalAttackCo;
    public IEnumerator firstSkillCo;
    public IEnumerator secondSkillCo;
    public IEnumerator thirdSkillCo;
    public GameObject spin;
    public SDirectionLine line;
    public SkillController skillController;
    public UnityAction<float> OnCharacterRecovery;
    public UnityAction<bool> OnCharacterUseSkill;
    public float timeCountPerAttack;
    public Animator animator;
    public PlayerProperties playerProperties;
    private void Start()
    {
        skillPanel = SGameInstance.Instance.skillJoytickPanel;
        skillController = new SkillController();
        skillController.Init();
        StartCoroutine(RecoveryRoutine());
    }

    public void CreatePanel(RectTransform createZone)
    {
        skillPanel = Instantiate(skillPanel, createZone);
        LoadFirstSkill();
    }

    void Update()
    {
        dirFirstSkill = skillPanel.joystickControllers[0].joystickSkill.Direction * 10;
        isRotateFirstSkill = RotateDirFirstSkill(dirFirstSkill);

        dirSeconndSkill = skillPanel.joystickControllers[1].joystickSkill.Direction * 10;
        isRotateSecondSkill = RotateDirSecondSkill(dirSeconndSkill);

        dirThirdSkill = skillPanel.joystickControllers[2].joystickSkill.Direction * 10;
        isRotateThirdSkill = RotateDirThirdSkill(dirThirdSkill);

        line.spriteLine[0].SetActive(dirFirstSkill != Vector3.zero);
        line.spriteLine[1].SetActive(dirSeconndSkill != Vector3.zero);
        line.spriteLine[2].SetActive(dirThirdSkill != Vector3.zero);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCoolDown(firstSkillCo, skillPanel.joystickControllers[0].OnResetCoolDown);
        }
        timeCountPerAttack += Time.deltaTime;

    }

    private bool RotateDirFirstSkill(Vector2 dir)
    {
        Vector3 newDir = dir * 8;
        if (dir.sqrMagnitude < 0.1f) return false;

        rotateAngleFirstSkill = Mathf.Atan2(newDir.x, newDir.y) * Mathf.Rad2Deg;
        //Debug.Log("rotateAngleFirstSkill: " + rotateAngleFirstSkill);
        if (rotateAngleFirstSkill < 0) rotateAngleFirstSkill += 360;
        line.transform.eulerAngles = Vector3.forward * -rotateAngleFirstSkill;
        return true;
    }

    private bool RotateDirSecondSkill(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.1f) return false;

        rotateAngleSecondSkill = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        if (rotateAngleSecondSkill < 0) rotateAngleSecondSkill += 360;
        line.transform.eulerAngles = Vector3.forward * -rotateAngleSecondSkill;
        return true;
    }

    private bool RotateDirThirdSkill(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.1f) return false;

        rotateAngleThirdSkill = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        if (rotateAngleThirdSkill < 0) rotateAngleThirdSkill += 360;
        line.transform.eulerAngles = Vector3.forward * -rotateAngleThirdSkill;
        return true;
    }

    public void SpawnBullet()
    {
        Instantiate(spin, line.gameObject.transform.position, line.gameObject.transform.rotation);
    }

    public void UseNormalAttack()
    {
        if (timeCountPerAttack > 0.5f)
        {
            SGameInstance.Instance.gameEvent.OnPlayerUseSkill?.Invoke();
            skillController.UseNormalAttack(transform.position, transform.rotation);
            AnimationAttack(OnPlayAttackAnim());
            timeCountPerAttack = 0;
        }
    }
    public void UseFirstSkill()
    {
        SGameInstance.Instance.gameEvent.OnPlayerUseSkill?.Invoke();
        skillController.UseFirstSkill(line.gameObject.transform.position, line.gameObject.transform.rotation);
        firstSkillCo = CoutCoolDown(skillPanel.joystickControllers[0].OnCoolDown, skillController.GetFirstSkillCoolDown());
        StartCoroutine(firstSkillCo);
    }
    public void UseSecondSkill()
    {
        SGameInstance.Instance.gameEvent.OnPlayerUseSkill?.Invoke();
        Vector3 dir = MiniumVector(dirSeconndSkill);
        skillController.UseSecondSkill(dir, line.gameObject.transform.rotation);
        secondSkillCo = (CoutCoolDown(skillPanel.joystickControllers[1].OnCoolDown, skillController.GetSecondSkillCoolDown()));

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        StartCoroutine(secondSkillCo);
        AnimationAttack(OnPlaySecondSkillAnim());
    }
    public void UseThirdSkill()
    {
        SGameInstance.Instance.gameEvent.OnPlayerUseSkill?.Invoke();
        AnimationAttack(OnPlayThirdSkillAnim());
        skillController.UseThirdSkill(transform.position + new Vector3(0, 1, 0), line.gameObject.transform.rotation);
        thirdSkillCo = (CoutCoolDown(skillPanel.joystickControllers[2].OnCoolDown, skillController.GetThirdSkillCoolDown()));
        StartCoroutine(thirdSkillCo);
    }

    public void LoadFirstSkill()
    {
       // skillPanel.joystickControllers[0].imageSkill.texture = skillController.skills[0].iconSkill;
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
            goToDirection = new Vector3(1, 1f * equation);
        }
        if (vectorEquation.x <= -1)
        {
            goToDirection = new Vector3(-1f, -1f * equation);
        }
        if (vectorEquation.y >= 1)
        {
            goToDirection = new Vector3(1f / equation, 1f);
        }
        if (vectorEquation.y <= -1)
        {
            goToDirection = new Vector3(-1f / equation, -1f);
        }
        return goToDirection;
    }
    IEnumerator CoutCoolDown(UnityAction<float> actionCoolDown, float cooldown)
    {
        actionCoolDown?.Invoke(cooldown);
        yield return new WaitForSeconds(0f);
    }
    void ResetCoolDown(IEnumerator skillCo, UnityAction actionCoolDown)
    {
        StopCoroutine(skillCo);
        actionCoolDown?.Invoke();
    }

    private IEnumerator RecoveryRoutine()
    {
        float recoveryAmount;
        while (true)
        {
            if (SGameInstance.Instance.player.playerProperties.health < SGameInstance.Instance.player.playerProperties.maxHealth)
            {
                recoveryAmount = 20f;
                Recover(recoveryAmount);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void Recover(float amount)
    {
        SGameInstance.Instance.player.playerProperties.health += amount;
        SGameInstance.Instance.player.playerProperties.health = Mathf.Clamp(SGameInstance.Instance.player.playerProperties.health, 0, SGameInstance.Instance.player.playerProperties.maxHealth);
        OnCharacterRecovery?.Invoke(SGameInstance.Instance.player.playerProperties.health);
    }

    private void AnimationAttack(IEnumerator skill)
    {

        StartCoroutine(skill);
    }
    IEnumerator OnPlayAttackAnim()
    {
        OnCharacterUseSkill?.Invoke(false);
        animator.Play("Attack");
        yield return new WaitForSeconds(1.2f);
        animator.Play("Idle");
        OnCharacterUseSkill?.Invoke(true);

    }

    IEnumerator OnPlayFistSkillAnim()
    {
        OnCharacterUseSkill?.Invoke(false);
        animator.Play("Attack");
        yield return new WaitForSeconds(1.2f);
        animator.Play("Idle");
        OnCharacterUseSkill?.Invoke(true);

    }

    IEnumerator OnPlaySecondSkillAnim()
    {
        OnCharacterUseSkill?.Invoke(false);
        animator.Play("Dash");
        yield return new WaitForSeconds(1.2f);
        animator.Play("Idle");
        OnCharacterUseSkill?.Invoke(true);

    }

    IEnumerator OnPlayThirdSkillAnim()
    {
        OnCharacterUseSkill?.Invoke(false);
        animator.Play("Ultimate");
        yield return new WaitForSeconds(1.8f);
        animator.Play("Idle");
        OnCharacterUseSkill?.Invoke(true);

    }
}
