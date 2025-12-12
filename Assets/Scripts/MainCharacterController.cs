using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    // --- Variáveis de Movimento e Configuração ---
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f; 
    public float jumpForce = 10f; 
    
    // Variável para a velocidade atual do Rigidbody
    private float currentSpeed; 
    
    [Header("Ground Check")]
    public Transform groundCheck; 
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; 

    // --- Variáveis Visuais e Componentes ---
    [Header("Visual Settings")]
    public Transform visual; // O objeto filho que contém o Sprite/Animator
    private Animator anim; // Componente Animator
    
    // --- Variáveis de Saúde e Status ---
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    
    // Variáveis privadas
    private Rigidbody2D rb;
    private bool isGrounded;

    [HideInInspector] public bool isVictorious = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 🛑 PEGA O ANIMATOR DO OBJETO VISUAL FILHO
        if (visual != null)
        {
            anim = visual.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("ERRO: Você esqueceu de arrastar o objeto 'Visual' no Inspector!");
        }

        // Inicializa o estado
        currentHealth = maxHealth;
        currentSpeed = walkSpeed; // Define a velocidade inicial como caminhada
    }

    // Dentro da classe MainCharacterController.cs
void Update()
{
    // ... (restante do código: Verificação do Chão, Input)

    float moveInput = Input.GetAxisRaw("Horizontal");
    bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

    // --- 1. Lógica de Corrida (Define PRIORIDADE e VELOCIDADE) ---
    
    // CORREÇÃO: A Corrida só deve ser ativada se houver movimento
    bool currentlyRunning = isShiftPressed && Mathf.Abs(moveInput) > 0f && isGrounded;

    if (currentlyRunning)
    {
        currentSpeed = runSpeed;
        
        if (anim != null)
        {
            // Ativa o parâmetro de Corrida
            anim.SetBool("IsReallyRunning", true); 
        }
    }
    else
    {
        currentSpeed = walkSpeed;
        
        if (anim != null)
        {
            // Desativa o parâmetro de Corrida
            anim.SetBool("IsReallyRunning", false);
        }
    }

    // --- 2. Aplica o Movimento ---
    rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

    // --- 3. Animações de Caminhada/Idle (Verifica se NÃO está Correndo) ---
    if (anim != null)
    {
        // O isrunning (Caminhada) SÓ deve ser TRUE se o personagem estiver se movendo
        // E NÃO estiver atualmente ativando a animação de corrida (IsReallyRunning é FALSE)
        bool isWalking = Mathf.Abs(moveInput) > 0f && isGrounded && !currentlyRunning;
        
        // Se isReallyRunning for True, isrunning será False, e a transição do Animator cuidará do resto.
        anim.SetBool("isrunning", isWalking); 
        
        anim.SetBool("isjumping", Mathf.Abs(rb.linearVelocity.y) > 0.01f && !isGrounded);
    }
    
    // ... (restante do código: Flip, Pulo)
}
    // --- Lógica de Vitória ---

    public void AchieveVictory()
    {
        isVictorious = true;
        this.enabled = false;

        if (anim != null)
        {
            anim.SetTrigger("Victory");
        }
        Debug.Log("Vitória alcançada!");
    }
}