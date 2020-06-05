using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject attackRange;                                                      // Rango de ataque.
    public AudioClip itemClip;                                                          // Clip de audio para cuando se coge un objeto.

    AudioSource playerAudio;                                                            // Referencia al audio.
    PlayerStats playerStats;                                                            // Referencia a las estadísticas del jugador.
    float speed;                                                                        // Velocidad con la que el jugador se moverá.
    Vector3 movement;                                                                   // Vector para guardar la dirección del movimiento del jugador.
    Animator anim;                                                                      // Referencia a la animación.
    Rigidbody playerRigidbody;                                                          // Referencia al cuerpo del jugador.
    int floorMask;                                                                      // Capa en la que funcionarán los rayos.
    float camRayLength = 100f;                                                          // Longitud del rayo desde la cámara hasta la escena.
    bool attacking;                                                                     // Cuando el jugador está atacando.
    Rigidbody targetRigidbody;                                                          // Cuerpo del objetivo.
    EnemyHealth targetHealth;                                                           // Salud del objetivo.
    float animSpeed;                                                                    // Velocidad de la animación.
    GameObject item;
    bool pickItem;
    IKHands ikHands;

    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();                                      // Componente del audio.
        playerStats = GetComponent<PlayerStats>();                                      // Componente de las estadísticas del jugador.
        floorMask = LayerMask.GetMask("Floor");                                         // Crear la capa para el suelo.
        anim = GetComponent<Animator>();                                                // Componente de la animación.
        playerRigidbody = GetComponent<Rigidbody>();                                    // Componente del cuerpo del jugador.
        attacking = false;                                                              // Fijar atacando a falso.
        pickItem = false;

        if (PlayerStats.character == "Warrior")
            ikHands = GetComponent<IKHands>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");                                       // Guardar el movimiento horizontal.
        float v = Input.GetAxisRaw("Vertical");                                         // Guardar el movimiento vertical.

        Move(h, v);                                                                     // Mover al jugador por la escena.
        Turning();                                                                      // Girar al jugador para que mira al cursor del ratón.
        Animating(h, v);                                                                // Animar al jugador.

        if (Input.GetKeyDown(KeyCode.Space))                                            // Si pulsas la tecla...
        {
            if (PlayerStats.character == "Warrior")
                ikHands.enabled = false;

            anim.SetTrigger("Pickup");                                                  // ...activar animación de coger.
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);                                                         // Fijar el movimiento del jugador.

        speed = 3f + (playerStats.speed * 0.01f);                                       // Calcular la velocidad con la que el jugador se moverá.

        if (speed > 13)                                                                 // Si la velocidad es mayor que la velocidad máxima...
            speed = 13f;                                                                // ...fijar la velocidad a la velocidad máxima.

        if (attacking)                                                                  // Si el jugador está atacando...
            speed /= 2f;                                                                // ...reducir su velocidad a la mitad.

        movement = movement.normalized * speed * Time.deltaTime;                        // Normalizar el vector del movimiento y hacerlo proporcional a la velocidad por segundo.

        playerRigidbody.MovePosition(transform.position + movement);                    // Mover al jugador a su posición actual más el movimiento.
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);                 // Crear un rayo desde la cámara hasta el cursor del ratón en la escena.

        RaycastHit floorHit;                                                            // Variable para guardar la información del objeto al que el rayo golpea.

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))             // Si el rayo golpea algo en la capa del suelo...
        {
            Vector3 playerToMouse = floorHit.point - transform.position;                // ...crear un vector desde el jugador hasta el punto del suelo en el que ha golpeado el rayo del ratón.
            playerToMouse.y = 0f;                                                       // ...asegurarse de que el vector solo se mueve en el plano del suelo.

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);            // ...crear una rotación desde el jugador hasta el ratón.
            playerRigidbody.MoveRotation(newRotation);                                  // ...fijar la rotación del jugador.
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;                                              // Crear un booleano que es verdadero si el movimiento no es cero.
        anim.SetBool("IsWalking", walking);                                             // Decir a la animación si el jugador está caminando o no.

        animSpeed = 0.6f + (playerStats.speed * 0.002f);                                // Calcular la velocidad de la animación.

        if (animSpeed > 8)                                                              // Si la velocidad de la animación supera el máximo...
            animSpeed = 8f;                                                             // ...fijar la velocidad de la animación al máximo.

        anim.SetFloat("AnimationSpeed", animSpeed);                                     // Fijar la velocidad de la animación a la animación.

        anim.SetFloat("VelocityX", transform.InverseTransformDirection(movement).x);    // Calcular la velocidad en X para la animación.
        anim.SetFloat("VelocityZ", transform.InverseTransformDirection(movement).z);    // Calcular la velocidad en Z para la animación.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")                                                        // Si el jugador colisiona con un objeto...
        {
            item = other.gameObject;
            pickItem = true;
        }

        if (other.tag == "LoadLevel")                                                   // Si el jugador colisiona con el cambio de escena...
            LoadingScreen.Instance.LoadScene(SceneManager.GetActiveScene().name + "Dungeon");   // ...cargar la escena.
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
            pickItem = false;
    }

    public void SetAttacking(bool attacking)
    {
        this.attacking = attacking;                                                     // Cuando el jugador está atacando.
    }

    public void Hit()
    {
        if (PlayerStats.character == "Warrior" || PlayerStats.character == "Guardian"   // Si el jugador es Guerrero, Guardián o Bárbaro...
            || PlayerStats.character == "Barbarian")
            attackRange.SendMessage("SkillHit");                                        // ...llamar a la función de golpear.
    }

    public void Shoot()
    {
        if (PlayerStats.character == "Hunter" || PlayerStats.character == "Mage")       // Si el jugador es Cazador o Mago...
            attackRange.SendMessage("SkillShoot");                                      // ...llamar a la función de disparar.
    }

    public void Pickup()
    {
        if (pickItem)
        {
            playerAudio.clip = itemClip;                                                // ...poner el clip de audio de coger un objeto.
            playerAudio.Play();                                                         // ...encender el audio.

            ScoreManager.items++;                                                       // ...sumar uno al contador de objetos.
            Destroy(item);                                                              // ...destruir el objeto.

            pickItem = false;
        }

        if (PlayerStats.character == "Warrior")
            StartCoroutine("Wait");


    }

    public void Boost()
    {
        attackRange.SendMessage("SkillBoost");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        ikHands.enabled = true;
    }
}