using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FishBattle
{
    public abstract class BaseFish : BaseSceneObj
    {
        [SerializeField]
        private float _hp;
        [SerializeField]
        private float _maxHp = 3;
        [SerializeField]
        private float _speed = 2;
        [SerializeField]
        private Vector2 _currMoveDirection;

        protected SceneManager SceneManager;

        public event Action<BaseFish> OnDie;
        public event Action OnDamaged;

        [System.Serializable]
        public class Bounce
        {
            public int layer;
            public float bounceforce;
            public float _dmg = 1;

        }
        [SerializeField]
        private List<Bounce> bounces;

        public GameObject sliderPrefab;

        public float Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                OnDamaged?.Invoke();
                if (_hp <= 0)
                {
                    _hp = 0;
                    OnDie?.Invoke(this);
                }
            }
        }

        public float MaxHp { get => _maxHp; set => _maxHp = value; }
        public Vector2 CurrMoveDirection { get => _currMoveDirection; set => _currMoveDirection = value; }
        public float Speed { get => _speed; set => _speed = value; }


        protected override void Awake()
        {
            base.Awake();
            SceneManager = FindObjectOfType<SceneManager>();
            _hp = _maxHp;
            OnDie += Die;
            OnDamaged += Damaged;
            sliderPrefab.GetComponent<Slider>().maxValue = MaxHp;
            sliderPrefab.GetComponent<Slider>().value = MaxHp;
        }

        private void Damaged()
        {
            sliderPrefab.GetComponent<Slider>().value = Hp;
        }

        public void SetDir(Vector2 dir)
        {
            CurrMoveDirection = dir;
            if (dir.x != 0)
            {
                transform.localScale = new Vector2(-Mathf.Sign(dir.x), transform.localScale.y);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (var item in bounces)
            {
                if (collision.gameObject.layer == item.layer)
                {
                    Rigidbody.velocity += collision.contacts[0].normal * item.bounceforce;
                    Hp -= item._dmg;
                }
            }
        }

        private void Die(BaseFish baseFish)
        {
            OnDie -= Die;
            OnDamaged -= Damaged;

            Destroy(gameObject); 
        }
    }
}
