using System.Collections.Generic;
using UnityEngine;

namespace FishBattle
{
    public class BadFishController : IOnInit, IOnUpdate
    {
        public GoodFish[] GoodFish;
        public BadFish[] BadFish;

        public List<GoodFish> GFList = new List<GoodFish>();
        public List<BadFish> BFList = new List<BadFish>();

        public void OnInit()
        {
            GoodFish = Object.FindObjectsOfType<GoodFish>();
            BadFish = Object.FindObjectsOfType<BadFish>();

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
        private void Die(BaseFish BaseFish)
        {
            for (int i = 0; i < GFList.Count; i++)
            {
                if (GFList[i] == BaseFish)
                {
                    GFList.Remove(GFList[i]);
                }
            }
            for (int i = 0; i < BFList.Count; i++)
            {
                if (BFList[i] == BaseFish)
                {
                    BFList.Remove(BFList[i]);
                }
            }
        }

        public void OnUpdate()
        {
            var td = Time.deltaTime;

            for (int i = 0; i < BFList.Count; i++)
            {
                var t = GetGoodFish(BFList[i].transform.position).transform;
                var v = (t.position - BFList[i].transform.position).normalized;
                //Debug.DrawLine(BFList[i].transform.position, t.position, Color.red);
                BFList[i].Rigidbody.velocity += (Vector2)v * BFList[i].Speed * td;
                BFList[i].Rigidbody.velocity *= Mathf.Pow(0.1f, td);

                if (v.x != 0)
                {
                    BFList[i].transform.localScale = new Vector2(-Mathf.Sign(v.x), BFList[i].transform.localScale.y);
                }


            }


        }

        public GoodFish GetGoodFish(Vector2 pos)
        {

            float dist = float.MaxValue;
            GoodFish gf = null;

            for (int i = 0; i < GFList.Count; i++)
            {
                var bestdist = ((Vector2)GFList[i].transform.position - pos).magnitude;
                if (bestdist < dist)
                {
                    dist = bestdist;
                    gf = GFList[i];
                }
            }
            return gf;
        }



    }
}
