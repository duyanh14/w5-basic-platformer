using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class Enemy : MonoBehaviour
    {
        public Slider health;
        public LayerMask playerLayer;
        public Transform attackPoint;

        private Animator animator;
        private bool attackAnimationPlay = false;
        
        private Vector3 moveTarget;
        private bool isDefaultPosition;

        public GameObject potionPrefab;

        void Start()
        {
            animator = GetComponent<Animator>();
            setMoveTarget();
        }

        void Update()
        {
            move();
            attackPlayer();
        }

        private  void attackPlayer()
        {
            if (attackAnimationPlay)
            {
                return;
            }

            Collider2D player = Physics2D.OverlapCircle(attackPoint.position, 1f, playerLayer);
            if (player == null)
            {
                return;
            }

            attackAnimationPlay = true;
            animator.SetTrigger("Attack_3");
            player.GetComponent<Player>().hit(2f);
        }

        public void attackAnimation()
        {
            attackAnimationPlay = false;
        }

        public void hit(float value)
        {
            health.value -= value;
            if (this.health.value <= 0)
            {
                Instantiate(potionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        
        void move()
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTarget, 1 * Time.deltaTime );
        
            if (Vector3.Distance(transform.localPosition, moveTarget) < 0.001f)
            {
                setMoveTarget();
            }
        }
    
        void setMoveTarget()
        {
            if (isDefaultPosition)
            {
                moveTarget.x -= 10;
                isDefaultPosition = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }       
            else
            {
                moveTarget.x += 10;
                isDefaultPosition = true;
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
        }
    }
}