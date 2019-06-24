using UnityEngine;
public class Knife : Weapon
{
    [SerializeField] private AnimatorEventHelper animatorEventHelper;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private GameObject targetHitSound = null;

    public override void OnActive()
    {
        base.OnActive();
        GameManager.instance.notificationCenter.FireGunMagAmmoChange(0, 0);
        animatorEventHelper.OnAnimationEventTrigger += HandleOnAnimationEventTrigger;
    }

    private void HandleOnAnimationEventTrigger()
    {
        Collider[] _hitColliders = Physics.OverlapSphere(transform.position,1f, hitLayer);

        for (int i = 0; i < _hitColliders.Length; i++)
        {       
            Damageable _damageble = _hitColliders[i].GetComponent<Damageable>();
            if (_damageble != null)
            {
                _damageble.DoDamage(damage, "Knife");

                if (targetHitSound != null)
                    Instantiate(targetHitSound);
            }
        }

    }

    public override void OnInActive()
    {
        base.OnInActive();
        animatorEventHelper.OnAnimationEventTrigger -= HandleOnAnimationEventTrigger;
    }


    /// <summary>
    /// Triggers the gun to shoot Once
    /// </summary>
    protected override void OnAttack()
    {
        GameManager.instance.notificationCenter.FirePlayerAnimation(PlayerAnimation.knifeAttack);
    }
}
