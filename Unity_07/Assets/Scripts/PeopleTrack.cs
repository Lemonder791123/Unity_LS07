
using System.Linq;      //引用 系統.查詢語言 Lin Query
using UnityEngine;


/// <summary>
/// 追蹤人類
/// </summary>
public class PeopleTrack : People
{
    /// <summary>
    /// 目標
    /// </summary>
    protected Transform target;

    /// <summary>
    /// 人類陣列
    /// </summary>
    public People[] people;

    /// <summary>
    /// 距離陣列
    /// </summary>
    public float[] distance;



    protected virtual  void Start()
    {
        //人類陣列 = 透過類型尋找物件<泛型>
        people = FindObjectsOfType<People>();
        //距離陣列的數量=人類陣列的數量
        distance = new float[people.Length];
        //設定目的地(原點)，避免一開始導覽錯亂
        agent.SetDestination(Vector3.zero);
    }

    private void Update()
    {
        Track();
    }


    /// <summary>
    /// 追蹤方法
    /// </summary>
    protected virtual void Track()
    {
        //儲存所有人跟此物件的距離
        for (int i = 0; i < people.Length; i++)
        {
            if (people[i] == null || people[i].transform.name == "薑餅" || people[i].transform.name == "警察")
            {
                if (people[i] == null) distance[i] = 1000;  //如果人類死亡 距離改為 1000
                else distance[i] = 999;      //與殭屍物件的距離改為999否則"自己"距離為0判斷為最近距離
                    continue;           //繼續 - 跳過並執行下一次迴圈
            }
            // Vector3.Distance(a,b)      座標.距離(物件座標,人類陣列座標)
            distance[i] = Vector3.Distance(transform.position, people[i].transform.position);
        }

        //判斷最近
        float min = distance.Min();                         //最小值 = 距離.最小值
        int index = distance.ToList().IndexOf(min);         //索引值 = 距離.轉清單.取得索引值(最小值)
        target = people[index].transform;                   //目標 = 人類[索引值].變形
        //追蹤最近目標
        agent.SetDestination(target.position);

        if (agent.remainingDistance <= 1f && min !=999) Hitpeople();     //判斷 距離 < 1 並且距離不是 999 傷害人類

    }

    private float timerHits;

    /// <summary>
    /// 傷害人類
    /// </summary>
    private void Hitpeople()
    {
        if (timerHits>=1f)                                  //如果 時間>=1秒
        {
            timerHits = 0;                                  //計時器 歸零
            agent.isStopped = true;                     //代理器 停止
            ani.SetTrigger("攻擊");                     //攻擊
            target.GetComponent<People>().Dead();       //人類 死亡
        }
        else
        {
            agent.isStopped = false;                    //否則 - 代理器 開始
            timerHits += Time.deltaTime;                    //計時器 累加
        }
    }

      


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "火球")
        {
            Dead();
        }
    }
}
