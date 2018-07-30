using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float projectileSpeed = 10f;
    // [SerializeField] public float damageCaused { get; set; }


    float damageCaused;

    public void SetDamage(float damage)
    {
        damageCaused = damage;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
       

        Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));
        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);
         }
          Destroy(gameObject,0.01f);
  }
}
