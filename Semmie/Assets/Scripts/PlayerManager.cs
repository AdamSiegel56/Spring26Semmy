using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isInvincible;
    public float invulnTime;

    public int numberOfCoins;

    private SpriteRenderer spriteRenderer;

    public Transform spawn;
    public static PlayerManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventBus<OnCoinPickup>.OnEvent += PickupCoin;
    }
    
    public void TakeDamage(int damage)
    {
        EventBus<OnDamageEvent>.Raise(new OnDamageEvent());

        if (isInvincible) { return; }
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            StartCoroutine(Death());
        }

        StartCoroutine(DamageAnimation());

    }

    public IEnumerator DamageAnimation()
    {
        isInvincible = true;
        float timer = 0;
        while (timer < invulnTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(0.1f); // Adjust flash speed as needed
            timer += 0.1f;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    public IEnumerator Death()
    {
        EventBus<OnDeathEvent>.Raise(new OnDeathEvent());
        isInvincible = true;
        gameObject.transform.DOMove(spawn.position, 1f);
        currentHealth = maxHealth;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
        EventBus<OnReviveEvent>.Raise(new OnReviveEvent());
        
    }

    public void PickupCoin(OnCoinPickup evt)
    {
        numberOfCoins++;

        if(numberOfCoins >= 4)
        {
            EventBus<AllCoinsAquired>.Raise(new AllCoinsAquired());
        }
    }

    public void SetSpawnToCheckpoint(Vector2 newPosition)
    {
        spawn.position = newPosition;
    }



}
