using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject target;
    private int damage;
    private Transform targetTransform;
    private Transform thisTransform;
    private float acceptableDistance = 0.3f;
    [SerializeField] private float moveSpeed;
    private Vector3 adjustmentPosition = new Vector3(0, 2, 0);
    [SerializeField] private GameObject VFX;
    private bool arrowHitting;
    void Start()
    {
        thisTransform = transform;
    }

    public void SetTarget(GameObject TARGET, int DAMAGE)
    {
        damage = DAMAGE;
        target = TARGET;
        targetTransform = target.transform;
    }
    
    void Update()
    {
        if (Vector3.SqrMagnitude(targetTransform.position + adjustmentPosition - thisTransform.position) < acceptableDistance && arrowHitting == false)
            StartCoroutine(ArrowHit());
        else
            MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        thisTransform.position += moveSpeed * Time.deltaTime * (targetTransform.position + adjustmentPosition - thisTransform.position);
    }

    private IEnumerator ArrowHit()
    {
        arrowHitting = true;
        target.GetComponent<Orc>().TakeDamage(damage);
        Instantiate(VFX, thisTransform);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
