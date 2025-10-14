using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;
    private AttackArea attackAreaScript;
    private Player.BasicPlayerMovement movement;
    
    [SerializeField] private float attackCooldown = 0.5f;   // interval between attacks
    [SerializeField] private float attackTime = 0.25f;  // how long does the hitbox linger
    [SerializeField] private float attackOffsetX = 1.0f;    // horizontal offset of attack hitbox
    [SerializeField] private float attackOffsetY = 0.0f;
    
    private bool attacking = false;
    private float attackTimer = 0.0f;
    private float cooldownTimer = 0.0f;
    
    void Start()
    {
        attackAreaScript = attackArea.GetComponent<AttackArea>();
        
        if (attackAreaScript != null)
            attackAreaScript.SetOwner(gameObject); // Suicide prevention
        attackArea.SetActive(false);
        
        movement = GetComponent<Player.BasicPlayerMovement>();
    }
    
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
        
        if (attackArea != null && movement != null)
        {
            Vector3 offset = new Vector3(
                movement.FacingRight ? attackOffsetX : -attackOffsetX,
                attackOffsetY,
                0
            );
            attackArea.transform.localPosition = offset;
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
        attackTimer = 0.0f;
    }
}
