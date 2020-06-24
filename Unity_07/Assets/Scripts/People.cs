using UnityEngine;
using UnityEngine.AI;

public class People : MonoBehaviour
{
    [Header("移動速度"), Range(0, 10)]
    public float speed = 1.5f;

    /// <summary>
    /// 導覽代理器
    /// </summary>
    protected NavMeshAgent agent;

    protected Animator ani;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();       //取得元件<代理器>
        ani = GetComponent<Animator>();             //取得元件<動畫控制器>

        agent.speed = speed;                        //代理器.速度 = 速度
    }


    public void Dead()
    {
        ani.SetTrigger("死亡");           //動畫控制器.設定觸發("死亡")
        agent.isStopped = true;        //停止導覽
        Destroy(gameObject, 1f);        //刪除(遊戲物件,秒數)
    }
}


