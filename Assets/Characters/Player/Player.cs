using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float damagePerHit = 45;
    [SerializeField] int enemyLayer = 10;  // TODO magic number!!
    [SerializeField] float minTimeBetweenHits = 0.5f;
    [SerializeField] float maxAttackRange = 2f;

    GameObject currentTarget;
    CameraRaycaster cameraRaycaster;
 
    float lastHitTime = 0f;
    float currentHealthPoints; 
    void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        currentHealthPoints = maxHealthPoints;
 
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
         if (layerHit == enemyLayer)
        {
            var enemy = raycastHit.collider.gameObject;
            
            // Check Enemy in range
            if ((enemy.transform.position - transform.position).magnitude > maxAttackRange)
            {
                return;
            }

            currentTarget = enemy;
            if (Time.time - lastHitTime > minTimeBetweenHits)
            {
                var enemyComponent = enemy.GetComponent<Enemy>();
                enemyComponent.TakeDamage(damagePerHit);
                lastHitTime = Time.time;
            }
        }
    }
}
