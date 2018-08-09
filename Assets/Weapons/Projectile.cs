using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Weapons
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float projectileSpeed = 10f;
        [SerializeField] GameObject shooter; // Only so can inspect when paused

        const float DESTROY_DELAY = 0.01f;

        float damageCaused;
        public void SetShooter(GameObject shooter)
        {
            this.shooter = shooter;
        }

        public void SetDamage(float damage)
        {
            damageCaused = damage;
        }

        public float GetDefaultLaunchSpeed()
        {
            return projectileSpeed;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != shooter.layer)
            {
                DamageDamageblObjects(collision);
            }
        }

        private void DamageDamageblObjects(Collision collision)
        {
            Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));
            if (damageableComponent)
            {
                (damageableComponent as IDamageable).TakeDamage(damageCaused);
            }
            Destroy(gameObject, DESTROY_DELAY);
        }
    }
}
