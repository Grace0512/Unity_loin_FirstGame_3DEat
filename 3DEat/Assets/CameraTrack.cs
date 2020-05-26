using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    #region 欄位與屬性
    /// <summary>
    ///  玩家變形元件
    /// </summary>
    private Transform player;

    [Header("追蹤速度"),Range(0.1f,50.5f)]
    public float speed = 1.5f;
    #endregion

    #region 方法
    /// <summary>
    /// 追蹤玩家
    /// </summary>
    private void Track()
    {
        // 攝影機與妹紙小明Y軸距離 4-2=2
        // 攝影機與妹紙小明的Z軸距離 -2.5-1 =-3.5

        Vector3 posTrack = player.position;
        posTrack.y += 2.0f;
        posTrack.z += -3.5f;

        // 取得攝影機座標
        Vector3 posCam = transform.position;

        // 攝影機的座標 = 三維向量.差值(A點,B點,百分比)
        posCam = Vector3.Lerp(posCam, posTrack, 0.5f * Time.deltaTime * speed);

        transform.position = posCam;

    
    }

    #endregion


    #region 事件
    #region 測試Lerp
    /*
     public float A = 0;
     public float B = 100;

     public Vector2 v2A = Vector2.zero;
     public Vector2 v2B = Vector2.one * 1000;
     private void Update()
     {
     //Lerp 差值
         A = Mathf.Lerp(A, B, 0.5f);

         v2A = Vector2.Lerp(v2A, v2B, 0.5f);

     }*/
    #endregion

    private void Start()
    {
        // 妹紙物件 = 遊戲物件.尋找("物件名稱").變形
        player = GameObject.Find("妹紙").transform;
    }

    // 延遲更新：會在Update執行後再執行
    // 建議：需要追蹤座標要寫在此事件內
    private void LateUpdate()
    {
        Track();
        
    }

    #endregion

}
