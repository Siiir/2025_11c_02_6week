using Unity.VisualScripting;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    
    [SerializeField] private float attackCooldown = 0.5f;   // interval between attacks
    [SerializeField] private float attackTime = 0.25f;  // how long does the hitbox linger
    // [SerializeField] private float attackDamage = 5.0f;
    
    private bool attacking = false;
    private float attackTimer = 0.0f;
    private float cooldownTimer = 0.0f;
    
    private GameObject attackArea;
    private AttackArea attackAreaScript;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackAreaScript = attackArea.GetComponent<AttackArea>();
        
        if (attackAreaScript != null)
            attackAreaScript.SetOwner(gameObject); // Suicide prevention (hopefully)
        attackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer <= 0)
        {
            Attack();
        }

        if (attacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackTime)
            {
                attacking = false;
                attackArea.SetActive(false);
                cooldownTimer = attackCooldown;
                attackTimer = 0.0f;
            }
        }
    }

    private void Attack()
    {
        // Debug.Log("ATTACK!");
        attacking = true;
        attackArea.SetActive(true);
        attackTimer = 0.0f;
        
        Vector3 scale = attackArea.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(transform.localScale.x);
        attackArea.transform.localScale = scale;
    }
}
