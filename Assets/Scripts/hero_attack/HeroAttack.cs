using Player;
using UnityEngine;

[RequireComponent(typeof(BasicPlayerMovement))]
public class HeroAttack : MonoBehaviour
{
    [Header("Attack Settings")] [SerializeField]
    private GameObject attackArea;

    [SerializeField] private uint attackDamage = 10;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackDuration = 0.2f;

    [Header("Attack Offset")] [SerializeField]
    private float offsetX = 1.0f;

    [SerializeField] private float offsetY = 0.0f;

    private BasicPlayerMovement movement;
    private AttackArea attackAreaComponent;

    private Animator _animator;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private bool isAttacking;
    private float attackTimer;
    private float cooldownTimer;

    private void Awake()
    {
        movement = GetComponent<BasicPlayerMovement>();
        _animator = GetComponent<Animator>();

        if (attackArea == null)
        {
            Debug.LogError($"{nameof(HeroAttack)}: Attack area not assigned!", this);
            enabled = false;
            return;
        }

        attackArea.SetActive(false);
        attackAreaComponent = attackArea.GetComponent<AttackArea>();

        if (attackAreaComponent == null)
            Debug.LogWarning($"{nameof(HeroAttack)}: AttackArea component not found on attackArea object.");
    }

    private void Update()
    {
        HandleCooldown();
        HandleAttackInput();
        HandleAttackState();
        UpdateAttackPosition();
    }

    private void HandleCooldown()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    private void HandleAttackInput()
    {
        if (!isAttacking && cooldownTimer <= 0 && Input.GetKeyDown(KeyCode.F))
            StartAttack();
    }

    private void HandleAttackState()
    {
        if (!isAttacking) return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDuration)
            EndAttack();
    }

    private void UpdateAttackPosition()
    {
        if (movement == null || attackArea == null) return;

        float direction = movement.FacingRight ? 1f : -1f;
        attackArea.transform.localPosition = new Vector3(offsetX * direction, offsetY, 0f);
    }

    private void StartAttack()
    {
        isAttacking = true;
        attackTimer = 0f;

        attackArea.SetActive(true);
        attackAreaComponent?.SetDamage(attackDamage);

        if (_animator != null)
        {
            _animator.SetTrigger(Attack);
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        cooldownTimer = attackCooldown;
        attackTimer = 0f;

        attackArea.SetActive(false);
    }
}