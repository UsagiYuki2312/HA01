using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNormalAttackObject : MonoBehaviour
{
    public SDamageTrigger damageTrigger;
    private SAlienSensor alienSensor;
    // private Vector3 startPosition;
    public Vector3 target;
    private Vector3 targetPosition;
    private Vector3 direction;
    public float speed;
    public float duration;

    protected virtual void Awake()
    {
        speed = 10;
        alienSensor = SGameInstance.Instance.player.alienSensor;
        damageTrigger = GetComponent<SDamageTrigger>();
        targetPosition = Vector3.zero;
        //damageTrigger.OnAlienTouched = OnAlienTouched;
    }


    protected void Update()
    {
        // target = alienSensor.closestAliens.transform.position;
        // direction = target - transform.position;
        // transform.Translate(direction * speed * Time.deltaTime, Space.World);
        //   //TriggerAutoHide();
        if (gameObject.active == true)
        {
            StartCoroutine(OnFinishAttack());
        }
    }

    protected virtual void OnAlienTouched()
    {
        gameObject.SetActive(false);
    }
    public void TriggerAutoHide()
    {
        float distance = Vector3.Distance(gameObject.transform.position, target);
        if (distance < 0.5f)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator OnFinishAttack()
    {
        yield return new WaitForSeconds(duration);
        FinishAttack();
    }

    protected virtual void FinishAttack()
    {
        gameObject.SetActive(false);
    }
}
