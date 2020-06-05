using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AudioClip attackClip;                                                            // Clip de audio para cuando el enemigo ataca.

    Animator anim;                                                                          // Referencia a la animación.
    GameObject player;                                                                      // Referencia al jugador.
    PlayerHealth playerHealth;                                                              // Referencia a la salud del jugador.
    EnemyMovement enemyMovement;                                                            // Referencia al movimiento del enemigo.
    EnemyHealth enemyHealth;                                                                // Referencia a la salud del enemigo.
    EnemyStats enemyStats;                                                                  // Referencia a las estadísticas del enemigo.
    AudioSource enemyAudio;                                                                 // Referencia al audio.
    bool playerInRange;                                                                     // Cuando el jugador está cerca puede ser atacado.
    float timer;                                                                            // Tiempo para contar cuando se hace el siguiente ataque.
    float timeBetweenAttacks;                                                               // Tiempo en segundos entre cada ataque.

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();                                      // Componente del movimiento del enemigo.
        enemyHealth = GetComponent<EnemyHealth>();                                          // Componente de la salud del enemigo.
        enemyStats = GetComponent<EnemyStats>();                                            // Componente de las estadísticas del enemigo.
        anim = GetComponent<Animator>();                                                    // Componente de la animación.
        enemyAudio = GetComponent<AudioSource>();                                           // Componente del audio.
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");                                // Componente del jugador.
        playerHealth = player.GetComponent<PlayerHealth>();                                 // Componente de la salud del jugador.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)                                                     // Si colisiona con el jugador...
            playerInRange = true;                                                           // ...el jugador está en rango.
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)                                                     // Si deja de colisionar con el jugador...
            playerInRange = false;                                                          // ...el jugador ya no está en rango.
    }

    void Update()
    {
        timer += Time.deltaTime;                                                            // Añadir el tiempo desde la última vez que se llamó a esta función.
        timeBetweenAttacks = 1.5f - (enemyStats.speed * 0.002f);                            // Fijar el tiempo entre ataques.

        if (timer >= timeBetweenAttacks)                                                    // Si el tiempo supera el tiempo entre ataques...
            enemyMovement.SetAttacking(false);                                              // ...fijar atacando a falso.

        if (playerInRange && enemyHealth.currentHealth > 0)                                 // Si el jugador está en rango y este enemigo está vivo...
        {
            transform.LookAt(player.transform.position);                                    // ...mirar al jugador.
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f); // ...fijar la rotación en los ejes horizontales.

            if (timer >= timeBetweenAttacks)                                                // ...si el tiempo supera el tiempo entre ataques...
                Attack();                                                                   // ...atacar.
        }
    }

    void Attack()
    {
        timer = 0f;                                                                         // Reiniciar el tiempo.
        enemyMovement.SetAttacking(true);                                                   // Fijar atacando a verdadero.

        if (playerHealth.currentHealth > 0)                                                 // Si el jugador tiene salud...
        {
            enemyAudio.clip = attackClip;                                                   // ...poner el clip de audio de ataque.
            enemyAudio.Play();                                                              // ...encender el audio.

            anim.SetInteger("Attack", Random.Range(1, 8));
            anim.SetTrigger("AttackTrigger");
            anim.SetBool("IsAttacking", true);
        }    
    }

    public void Hit()
    {
        playerHealth.TakeDamage(enemyStats.attack);                                         // ...dañar al jugador.
        anim.SetBool("IsAttacking", false);
    }
}