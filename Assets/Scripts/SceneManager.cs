using System.Collections.Generic;
using UnityEngine;


namespace FishBattle
{
    public class SceneManager : MonoBehaviour
    {
        public Vector2 bounds = new Vector2(16, 10);

        BadFishController BadFishController;
        GoodFishController GoodFishController;

        List<IOnInit> inites = new List<IOnInit>();
        List<IOnUpdate> updates = new List<IOnUpdate>();

        private void Awake()
        {
            GoodFishController = new GoodFishController();
            BadFishController = new BadFishController();

            inites.Add(GoodFishController);
            inites.Add(BadFishController);

            updates.Add(GoodFishController);
            updates.Add(BadFishController);

        }
        private void Start()
        {
            for (int i = 0; i < inites.Count; i++)
            {
                inites[i].OnInit();
            }
        }

        private void Update()
        {
            for (int i = 0; i < updates.Count; i++)
            {
                updates[i].OnUpdate();
            }
        }


    }
}

