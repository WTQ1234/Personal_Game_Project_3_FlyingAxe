using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    public Text text_lv;
    public Slider slider_exp;
    public GameObject dropCoin;
    public GameObject dropHealth;

    [Title("当前等级")]
    public int level = 1;
    public int exp = 0;
    public int maxExp = 100;

    public int pushBackDamage = 10;
    public float pushBackRadious = 10;
    public float pushBackVelocity = 2f;
    public bool drawGizoms;

    public Player player;

    private Dictionary<int, LevelInfo> dict_all_info;

    public GameObject UI_PlayerFail;
    public GameObject UI_PlayerSuccess;

    private void Start()
    {
        GameController.isGameAlive = true;
        dict_all_info = ConfigManager.Instance.GetAllInfo<LevelInfo>();

        level = 1;

        ExpBar.curLevel = level;
        ExpBar.ExpCurrent = exp;
        ExpBar.ExpMax = maxExp;
    }

    // 增加最大生命值
    private void _OnLevelUp()
    {
        player.playerHealth.AddMaxHp(5);
        maxExp += 5;
        player.coinMagnet.transform.localScale *= 1.1f;
        EnemyManager.Instance.time = EnemyManager.Instance.time * 0.8f;
        //LevelInfo info = _GetLevelConfig(level);
        //if (info.addHp > 0)
        //{

        //}
    }

    [Button("AddExp")]
    public void AddExp(int _exp)
    {
        exp += _exp;
        if (exp > maxExp)
        {
            level++;
            Collider2D[] collider2DList = Physics2D.OverlapCircleAll(player.transform.position, pushBackRadious);
            foreach (Collider2D collider2D in collider2DList)
            {
                if (collider2D.gameObject.tag == "Enemy")
                {
                    Rigidbody2D rb2d = collider2D.GetComponent<Rigidbody2D>();
                    if (rb2d != null)
                    {
                        
                        Vector2 dir = collider2D.transform.position - player.transform.position;
                        dir.Normalize();
                        //rb2d.AddForce(dir * pushBackVelocity);
                        rb2d.velocity = dir * pushBackVelocity;
                    }
                }
            }

            // TODO 升级时造成伤害，导致死循环
            //Collider2D[] collider2DList_near = Physics2D.OverlapCircleAll(player.transform.position, pushBackRadious);
            //foreach (Collider2D collider2D in collider2DList_near)
            //{
            //    if (collider2D.gameObject.tag == "Enemy")
            //    {
            //        var enemy = collider2D.GetComponent<Enemy>();
            //        if (enemy != null)
            //        {
            //            enemy.TakeDamage(pushBackDamage);
            //        }
            //    }
            //}
            exp = 0;
            player.OnLevelUp();
            _OnLevelUp();
        }
        ExpBar.curLevel = level;
        ExpBar.ExpCurrent = exp;
        ExpBar.ExpMax = maxExp;
    }

    private LevelInfo _GetLevelConfig(int level)
    {
        return dict_all_info[level];
    }

    public void OnDropExp(Vector3 _pos)
    {
        GameObject enemy = ObjectPoolManager.Instance.GetObject(dropCoin);
        enemy.SetActive(true);
        enemy.transform.position = _pos;
        enemy.transform.rotation = Quaternion.identity;
    }

    public bool OnDropHealth(Vector3 _pos)
    {
        float ran = Random.Range(0f, 1f);
        if (ran < 0.2f)
        {
            GameObject enemy = ObjectPoolManager.Instance.GetObject(dropHealth);
            enemy.SetActive(true);
            enemy.transform.position = _pos;
            enemy.transform.rotation = Quaternion.identity;
            return true;
        }
        return false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        // 获取当前场景的名称
        string currentSceneName = SceneManager.GetActiveScene().name;
        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneName);
        UI_PlayerFail.SetActive(false);
        UI_PlayerSuccess.SetActive(false);
}

    private void OnDrawGizmos()
    {
        if (drawGizoms)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.transform.position, pushBackRadious);
        }
    }
}
