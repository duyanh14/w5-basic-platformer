using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;

        public Slider health;
        public LayerMask enemyLayer;
        public Transform attackPoint;
        
        private bool attackAnimationPlay = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            move();
        }

        private void attackEnemy()
        {
            if (attackAnimationPlay)
            {
                return;
            }
            
            attackAnimationPlay = true;
            animator.SetTrigger("Attack_2");
            
            Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.position, 2f, enemyLayer);
            foreach (Collider2D p in player)
            {
                p.GetComponent<Enemy>().hit(30f);
            }
        }
        
        public void attackAnimation()
        {
            attackAnimationPlay = false;
        }
        
        private void move()
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("Run");
                transform.Translate(Vector3.left * 5f * Time.deltaTime);
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(
                        transform.localScale.x * -1,
                        transform.localScale.y,
                        transform.localScale.z
                    );
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("Run");
                transform.Translate(Vector3.right * 5f * Time.deltaTime);
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(
                        transform.localScale.x * -1,
                        transform.localScale.y,
                        transform.localScale.z
                    );
                }
            } 
            else if (Input.GetMouseButtonDown(0))
            {
                attackEnemy();
            }
        }
        
        public void hit(float value)
        {
            health.value -= value;
        }
        
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag != "Potion")
            {
                return;
            }
            
            Destroy(col.gameObject);

            this.health.value = 100;
        }
    }
}