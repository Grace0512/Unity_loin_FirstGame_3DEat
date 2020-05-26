using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 欄位與屬性
    [Header("道具")]
    public GameObject[] porps;
       
    [Header("文字介面：道具總數")]
    public Text textCount;

    [Header("文字介面：時間")]
    public Text textTime;

    [Header("文字介面：標題")]
    public Text textTitle;

    [Header("結束畫面")]
    public CanvasGroup final;
    
    /// <summary>
    /// 道具總數
    /// </summary>
    private int countTotal;
 
    /// <summary>
    /// 取得道具數量
    /// </summary>
    private int countProp;

    private float gameTime = 30;
    #endregion

    #region 方法
    /// <summary>
    /// 生成道具
    /// </summary>
    /// <param name="prop">想要生成的道具</param>
    /// <param name="count">想要生成的數量+隨機值 +-5 </param>
    private int CreatProp(GameObject prop,int count)
    {
        int total = count + Random.Range(-5,5);
        
        for (int i=0;i<total; i++)
        {
            // 座標 = x隨機，1.5，y隨機
            Vector3 pos = new Vector3(Random.Range(-9,9),2.0f,Random.Range(-9,9));
            // 生成(物件,座標,角度)
            Instantiate(prop, pos, Quaternion.identity);
        }

        // 傳回 道具數量
        return total;
    }

    /// <summary>
    /// 時間倒數
    /// </summary>
    private void CountTime()
    {
        // 遊戲時間 遞減 一禎的時間
        gameTime -= Time.deltaTime;

        // 更新倒數時間介面ToString("f小數點位數")
        textTime.text = "倒數時間：" + gameTime.ToString("f2");
    }
    #endregion

    #region 事件
    private void Start()
    {
        countTotal = CreatProp(porps[0], 20);
        textCount.text = "道具數量：0/" + countTotal;
        countTotal = CreatProp(porps[1], 20);

    }

    private void Update()
    {
        CountTime();
    }

    #endregion


}
