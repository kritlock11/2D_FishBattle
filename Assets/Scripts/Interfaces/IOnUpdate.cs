namespace FishBattle
{
    public interface IOnUpdate
    {
        void OnUpdate();
    }
}
//void CreateScene()
//{
//    var BG = new GameObject { name = "BG" };
//    var BGimgHolder = new GameObject { name = "BGimgHolder" }.AddComponent<SpriteRenderer>();
//    BGimgHolder.transform.SetParent(BG.transform);
//    BGimgHolder.sprite = Resources.Load<Sprite>("BG");
//}
//void CreateGoodFish()
//{
//        var GF = new GameObject { name = "GoodFish" };


//        var GFimgHolder = new GameObject { name = "GoodFishHolder" }.AddComponent<SpriteRenderer>();
//        GFimgHolder.gameObject.AddComponent<CircleCollider2D>()
//                   .gameObject.AddComponent<Rigidbody2D>()
//                   .gameObject.AddComponent<GoodFish>()
//                   .transform.SetParent(GF.transform);
//        GFimgHolder.GetComponent<Rigidbody2D>().gravityScale = 0;
//        GFimgHolder.sortingOrder = 1;
//        GFimgHolder.sprite = Resources.Load<Sprite>("GoodFish");
//}