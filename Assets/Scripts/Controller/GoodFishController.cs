using System.Collections.Generic;
using UnityEngine;

namespace FishBattle
{
    public class GoodFishController : IOnInit, IOnUpdate
    {
        public GoodFish[] GoodFish;
        public BadFish[] BadFish;
        public BaseFish[] BaseFish;


        protected SceneManager SceneManager;

        float timer = 0;

        public List<GoodFish> GFList = new List<GoodFish>();
        public List<BadFish> BFList = new List<BadFish>();


        public void OnInit()
        {
            GoodFish = Object.FindObjectsOfType<GoodFish>();
            BadFish = Object.FindObjectsOfType<BadFish>();

            SceneManager = Object.FindObjectOfType<SceneManager>();

            for (int i = 0; i < GoodFish.Length; i++)
            {
                GFList.Add(GoodFish[i]);
                GoodFish[i].OnDie += Die;
            }

            for (int i = 0; i < BadFish.Length; i++)
            {
                BFList.Add(BadFish[i]);
                BadFish[i].OnDie += Die;

            }
        }

        void SetDir(BaseFish fish)
        {
            var v = new Vector2(Random.value - 0.5f, Random.value - 0.5f);
            fish.CurrMoveDirection = v;
        }

        public void OnUpdate()
        {
            for (int i = 0; i < GFList.Count; i++)
            {
                if (timer <= 0)
                {
                    SetDir(GFList[i]);
                    timer = Random.Range(2, 5);
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }


            Move();
        }
        public BadFish GetBadFish(Vector2 pos)
        {

            if (BFList.Count != 0)
            {
                float dist = float.MaxValue;
                BadFish gf = null;

                for (int i = 0; i < BFList.Count; i++)
                {
                    var bestdist = ((Vector2)BFList[i].transform.position - pos).magnitude;
                    if (bestdist < dist)
                    {
                        dist = bestdist;
                        gf = BFList[i];
                    }
                }
                return gf;
            }
            return null;
        }

        public void Move()
        {
            var td = Time.deltaTime;
            for (int i = 0; i < GFList.Count; i++)
            {

                var gf = GFList[i].transform.position;



                //Debug.DrawLine(gf, bf, Color.blue);
                //Debug.Log($"{m.magnitude}");

                var x = GFList[i].Transform.position.x;
                var y = GFList[i].Transform.position.y;
                var bx = SceneManager.bounds.x;
                var by = SceneManager.bounds.y;
                var s = GFList[i].Speed;




                if (x < -bx * 0.5f + 0.5f)
                {
                    GFList[i].Rigidbody.velocity += Vector2.right * s * td;
                }
                if (x > bx * 0.5f - 0.5f)
                {
                    GFList[i].Rigidbody.velocity += Vector2.left * s * td;
                }
                if (y < -by * 0.5f + 0.5f)
                {
                    GFList[i].Rigidbody.velocity += Vector2.up * s * td;
                }
                if (y > by * 0.5f - 0.5f)
                {
                    GFList[i].Rigidbody.velocity += Vector2.down * s * td;
                }

                GFList[i].Rigidbody.velocity += GFList[i].CurrMoveDirection.normalized * GFList[i].Speed * td;
                GFList[i].Rigidbody.velocity *= Mathf.Pow(0.5f, td);

                if (GFList[i].Rigidbody.velocity.x != 0)
                {
                    GFList[i].transform.localScale = new Vector2(-Mathf.Sign(GFList[i].Rigidbody.velocity.x), GFList[i].transform.localScale.y);
                }

                if (GetBadFish(GFList[i].Transform.position) != null)
                {
                    var bf = GetBadFish(GFList[i].Transform.position).transform.position;

                    var m = -(bf - gf);
                    if (m.magnitude < 2f)
                    {
                        GFList[i].Rigidbody.velocity += (Vector2)m.normalized * GFList[i].Speed * td;
                        Physics2D.Raycast(gf, Vector3.Cross(gf, m), 10);
                        //Debug.DrawLine(gf, Vector3.Cross(gf, m), Color.yellow);

                        GFList[i].Rigidbody.velocity *= Mathf.Pow(1.25f, td);
                        SetDir(GFList[i]);
                    }
                }
            }
        }

        private void Die(BaseFish BaseFish)
        {
            for (int i = 0; i < GFList.Count; i++)
            {
                if (GFList[i] == BaseFish)
                {
                    GFList[i].OnDie -= Die;
                    GFList.Remove(GFList[i]);
                }
            }
            for (int i = 0; i < BFList.Count; i++)
            {
                if (BFList[i] == BaseFish)
                {
                    BFList[i].OnDie -= Die;
                    BFList.Remove(BFList[i]);
                }
            }
        }
    }
}


