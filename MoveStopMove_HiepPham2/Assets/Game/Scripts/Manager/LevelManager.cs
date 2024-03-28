using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;

    private List<Enemy> enemys = new List<Enemy>();

    [SerializeField] Level[] levels;
    public Level currentLevel;

    private int totalEnemy;
    private bool isRevive;

    private int levelIndex;

    public int TotalCharater => totalEnemy + enemys.Count + 1;

    public void Start()
    {
        levelIndex = 0;
        OnLoadLevel(levelIndex);
        OnInit();
    }

    public void OnInit()
    {
        player.OnInit();

        for (int i = 0; i < currentLevel.enemyReal; i++)
        {
            NewEnemy(null);
        }

        totalEnemy = currentLevel.enemyTotal - currentLevel.enemyReal - 1;

        isRevive = false;

        SetTargetIndicatorAlpha(0);
    }

    public void OnReset()
    {
        player.OnDespawn();
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].OnDespawn();
        }

        enemys.Clear();
        SimplePool.CollectAll();
    }

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    public Vector3 RandomPoint()
    {
        Vector3 randPoint = Vector3.zero;

        float size = Character.ATT_RANGE + Character.MAX_SIZE + 1f;

        for (int t = 0; t < 50; t++)
        {

            randPoint = currentLevel.RandomPoint();
            if (Vector3.Distance(randPoint, player.TF.position) < size)
            {
                continue;
            }

            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < enemys.Count; i++)
                {
                    if (Vector3.Distance(randPoint, enemys[i].TF.position) < size)
                    {
                        break;
                    }
                }

                if (j == 19)
                {
                    return randPoint;
                }
            }


        }

        return randPoint;
    }

    private void NewEnemy(IState<Enemy> state)
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, RandomPoint(), Quaternion.identity);
        enemy.OnInit();
        enemy.ChangeState(state);
        enemys.Add(enemy);

        enemy.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
    }

    public void CharecterDeath(Character c)
    {
        if (c is Player)
        {
            UIManager.Ins.CloseAll();

            //revive
            if (!isRevive)
            {
                isRevive = true;
                UIManager.Ins.OpenUI<UIRevive>();
            }
            else
            {
                Fail();
            }
        }
        else
        if (c is Enemy)
        {
            enemys.Remove(c as Enemy);

            if (GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Setting))
            {
                NewEnemy(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());
            }
            else
            {
                if (totalEnemy > 0)
                {
                    totalEnemy--;
                    NewEnemy(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());
                }

                if (enemys.Count == 0)
                {
                    Victory();
                }
            }

        }

        UIManager.Ins.GetUI<UIGameplay>().UpdateTotalCharacter();
    }

    private void Victory()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIVictory>().SetCoin(player.Coin);
        player.ChangeAnim(Constant.ANIM_WIN);
    }

    public void Fail()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIFail>().SetCoin(player.Coin); 
    }

    public void Home()
    {
        UIManager.Ins.CloseAll();
        OnReset();
        OnLoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void NextLevel()
    {
        levelIndex++;
    }

    public void OnPlay()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].ChangeState(new PatrolState());
        }
    }

    public void OnRevive()
    {
        player.TF.position = RandomPoint();
        player.OnRevive();
    }

    public void SetTargetIndicatorAlpha(float alpha)
    {
        List<GameUnit> list = SimplePool.GetAllUnitIsActive(PoolType.TargetIndicator);

        for (int i = 0; i < list.Count; i++)
        {
            (list[i] as TargetIndicator).SetAlpha(alpha);
        }
    }
}
