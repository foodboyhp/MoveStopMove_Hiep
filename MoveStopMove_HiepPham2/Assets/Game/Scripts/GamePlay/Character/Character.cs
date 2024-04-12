using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit, IHit
{
    public const float TIME_DELAY_THROW = 0.4f;
    public const float ATT_RANGE = 5f;
    public const float MAX_SIZE = 4f;
    public const float MIN_SIZE = 1f;
    [SerializeField] private Transform indicatorPoint;
    [SerializeField] private GameObject mask;

    private string animName;
    private List<Character> targets = new List<Character>();
    private Vector3 targetPoint;
    private int score;

    protected TargetIndicator indicator;
    protected Character target;
    protected float size = 1;

    public int Score => score;
    public float Size => size;
    public bool IsDead { get; protected set; }

    public bool IsCanAttack => currentSkin.Weapon.IsCanAttack;

    public virtual void OnInit()
    {
        IsDead = false;
        score = 0;

        WearClothes();
        ClearTarget();

        indicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        indicator.SetTarget(indicatorPoint);
    }

    public virtual void WearClothes()
    {

    }

    public virtual void TakeOffClothes()
    {
        currentSkin?.OnDespawn();
        SimplePool.Despawn(currentSkin);
    }

    public virtual void OnAttack()
    {
        target = GetTargetInRange();

        if (IsCanAttack && target != null && !target.IsDead)
        {
            targetPoint = target.TF.position;
            TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
            ChangeAnim(Constant.ANIM_ATTACK);
        }

    }

    public virtual void Throw()
    {
        currentSkin.Weapon.Throw(this, targetPoint, size);
    }

    public virtual void OnDeath()
    {
        ChangeAnim(Constant.ANIM_DIE);
        LevelManager.Ins.CharacterDeath(this);
    }

    public virtual void OnMoveStop()
    {

    }

    public virtual void OnDespawn()
    {
        TakeOffClothes();
        SimplePool.Despawn(indicator);
    }

    public void OnHit(UnityAction hitAction)
    {
        if (!IsDead)
        {
            IsDead = true;
            SoundManager.Ins.PlaySoundEffect(SoundEffectState.Shoot);
            hitAction?.Invoke();
            OnDeath();
        }
    }

    public void SetMask(bool active)
    {
        mask.SetActive(active);
    }

    public void AddScore(int amount = 1)
    {
        SetScore(score + amount);
    }

    public void SetScore(int score)
    {
        this.score = score > 0 ? score : 0;
        indicator.SetScore(this.score);
        SetSize(1 + this.score * 0.1f);
    }

    protected virtual void SetSize(float size)
    {
        size = Mathf.Clamp(size, MIN_SIZE, MAX_SIZE);
        this.size = size;
        TF.localScale = size * Vector3.one;
    }

    public virtual void AddTarget(Character target)
    {
        targets.Add(target);
    }

    public virtual void RemoveTarget(Character target)
    {
        targets.Remove(target);
        this.target = null;
    }
    protected void ClearTarget()
    {
        targets.Clear();
    }

    public Character GetTargetInRange()
    {
        Character target = null;
        float distance = float.PositiveInfinity;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i] != this && !targets[i].IsDead && Vector3.Distance(TF.position, targets[i].TF.position) <= ATT_RANGE * size + targets[i].size)
            {
                float dis = Vector3.Distance(TF.position, targets[i].TF.position);

                if (dis < distance)
                {
                    distance = dis;
                    target = targets[i];
                }
            }
        }

        return target;
    }

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            currentSkin.Anim.ResetTrigger(this.animName);
            this.animName = animName;
            currentSkin.Anim.SetTrigger(this.animName);
        }
    }

    public void ResetAnim()
    {
        animName = "";
    }

    [SerializeField] protected Skin currentSkin;

    public void ChangeWeapon(WeaponType weaponType)
    {
        currentSkin.ChangeWeapon(weaponType);
    }

    public void ChangeSkin(SkinType skinType)
    {
        currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        currentSkin.ChangeAccessory(accessoryType);
    }

    public void ChangeHat(HatType hatType)
    {
        currentSkin.ChangeHat(hatType);
    }

    public void ChangePant(PantType pantType)
    {
        currentSkin.ChangePant(pantType);
    }
}
