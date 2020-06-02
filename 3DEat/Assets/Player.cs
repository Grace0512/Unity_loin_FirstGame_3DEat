using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位與屬性
    [Header("移動速度"),Range(1,1000)]
    public float speed = 10;

    [Header("跳躍高度"), Range(1, 5000)]
    public float height;


    /// <summary>
    /// 是否在地板上
    /// </summary>
    private bool isGround
    {
        get
        {
            if (transform.position.y < 0.051f) return true;
            else return false;
        }
    }

    /// <summary>
    /// 旋轉角度
    /// </summary>
    private Vector3 angle;

    // 玩家身上要取得的屬性
    // 角色身上的動畫
    private Animator ani;
    // 角色的碰撞器
    private Rigidbody rig;
    // 喇叭
    private AudioSource aud;
    private GameManager gm;

    /// <summary>
    /// 跳躍力道:從0慢慢增加
    /// </summary>
    private float jump;

    [Header("吃西瓜音效")]
    public AudioClip soundEat;

    [Header("吃番茄音效")]
    public AudioClip soundNotEat;

   

    #endregion

    #region 方法
    /// <summary>
    /// 移動:透過鍵盤
    /// </summary>
    private void Move()
    {
        #region 移動
        // 浮點數 前後值 = 輸入類別.取得軸向值("垂直"-垂直WS上下)
        float V = Input.GetAxisRaw("Vertical");
        // 左右
        float H = Input.GetAxisRaw("Horizontal");

        // 剛體,添加推力(x,y,z)
        //rig.AddForce(0, 0, speed*V);
        // 添加推力(三維向量)
        rig.AddForce(transform.forward * speed *Mathf.Abs(V));
        rig.AddForce(transform.forward * speed *Mathf.Abs(H));

        ani.SetBool("跑步開關", Mathf.Abs(V) > 0||Mathf.Abs(H) > 0);
        #endregion

        #region 轉向
        if (V == 1) angle = new Vector3(0,0,0); // 前 y=0
        else if (V == -1) angle = new Vector3(0,180,0); //後 y=180
        else if (H == 1) angle = new Vector3(0,90,0); //右 y=90
        else if (H == -1) angle = new Vector3(0,270,0); //左 y=270

        // 只要類別後面有: MonoBehaviour
        // 就可以直接使用關鍵字transform取得此物件的Transform M元件
        transform.eulerAngles = angle;


        #endregion

    }

    /// <summary>
    /// 跳躍:判斷在地板上並按下空白鍵時跳躍
    /// </summary>
    private void Jump()
    {
    // 如果在地板上 為勾選 並且 按下空白鍵
    if(isGround && Input.GetButtonDown("Jump"))
        {
            // 每次跳躍 值都從0開始
            jump = 0;
            rig.AddForce(0, height, 0);
        }
        if (!isGround)
        {
                      
            // 跳躍遞增時間.一禎時間
            jump += Time.deltaTime;
        }
        ani.SetFloat("跳躍力道",jump);
    }
    /// <summary>
    /// 碰到道具:碰到帶有標籤"食物"的物件
    /// </summary>
    private void HitProp(GameObject prop)
    {
        print("碰到的物件標籤為"+prop.name);
        if(prop.tag == "食物")
        {
            aud.Stop();
            aud.PlayOneShot(soundEat, 2); //喇叭.撥放一次音效(音效片段,音量)
            Destroy(prop); //刪除物件
        }
        else if (prop.tag == "不能吃")
        {
            aud.Stop();
            aud.PlayOneShot(soundNotEat, 2);
            Destroy(prop);
        }

        gm.GetProp(prop.tag);
    }

    #endregion

    #region 事件
    private void Start()
    {
        // GetComponent<泛型>(); 泛型方法-泛型 所有類別 rigidbody,transform,collider...
        // GetComponent<Rigidbody>(); 取得鋼體元件存放到rig
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        //foot僅限於場景上只有一個類別存在時使用
        // 例如：場景上只有一個GameManger類別時可以使用他來取得
        gm = FindObjectOfType<GameManager>();
    }

    // 固定更新頻率事件: 1秒50禎，使用物理必須在此事件內
    private void FixedUpdate()
    {
        Move();
        
    }

    private void Update()
    {
        Jump();
    }
    #endregion

    //碰撞事件：當物件碰撞開始時執行一次(沒有勾選Is Trigger)
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    //碰撞事件：當物件碰撞離開始執行一次(沒有勾選Is Trigger)

    private void OnCollisionExit(Collision collision)
    {
        
    }

    // 碰撞事件：當物件碰撞開始時持續執行(沒有勾選 Is Trigger)
    private void OnCollisionStay(Collision collision)
    {
        
    }

    // 觸發事件：當物件碰撞開始執行一次(有勾選Is Trigger)
    private void OnTriggerEnter(Collider other)
    {
        HitProp(other.gameObject);
    }

    //觸發事件：當物件碰撞離開執行一次(有勾選Is Trigger)
    private void OnTriggerExit(Collider other)
    {
        
    }
    // 觸發事件：當物件碰撞開始時持續執行(勾選 Is Trigger)　60 FPS
    private void OnTriggerStay(Collider other)
    {
        
    }

}
