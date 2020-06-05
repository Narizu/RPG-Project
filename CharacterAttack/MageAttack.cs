using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MageAttack : MonoBehaviour
{
    public GameObject player;                                                                       // Referencia al jugador.
    public Button skill1Button;                                                                     // Botón de la habilidad 1.
    public Button skill2Button;                                                                     // Botón de la habilidad 2.
    public Button skill3Button;                                                                     // Botón de la habilidad 3.
    public Button skill4Button;                                                                     // Botón de la habilidad 4.
    public Button skill5Button;                                                                     // Botón de la habilidad 5.
    public Rigidbody spell;                                                                         // Hechizo para la habilidad 1.
    public Rigidbody spellSkill;                                                                    // Hechizo para la habilidad 3.
    public Rigidbody spellUlti;                                                                     // Hechizo para la habilidad 5.
    public AudioClip[] attackClip;
    public Transform spellSpawn;
    public GameObject particle;
    public GameObject s2particle;
    public GameObject s4particle;
    public GameObject particleUlti;

    PlayerMovement playerMovement;                                                                  // Referencia al movimiento del jugador.
    PlayerHealth playerHealth;                                                                      // Referencia a la salud del jugador.
    PlayerMagic playerMagic;                                                                        // Referencia a la magia del jugador.
    PlayerStats playerStats;                                                                        // Referencia a las estadísticas del jugador.
    EnemyPanelManager enemyPanelManager;                                                            // Referencia al panel del enemigo.
    Collider selected;                                                                              // Referencia al enemigo seleccionado.
    AudioSource attackAudio;                                                                        // Referencia al audio.
    Animator anim;                                                                                  // Referencia a la animación.
    Rigidbody spellSelected;                                                                        // Hechizo seleccionado.
    float timer;                                                                                    // Tiempo para contar cuando se hace el siguiente ataque.
    float timeBetweenAttacks;                                                                       // Tiempo en segundos entre cada ataque.
    int shootableMask;                                                                              // Capa en la que el jugador puede hacer daño.
    string skillSelected;                                                                           // Habilidad seleccionada.
    float timerSkill2;                                                                              // Tiempo para contar cuando se hace la habilidad 2.
    float timerSkill3;                                                                              // Tiempo para contar cuando se hace la habilidad 3.
    float timerSkill4;                                                                              // Tiempo para contar cuando se hace la habilidad 4.
    float timerSkill5;                                                                              // Tiempo para contar cuando se hace la habilidad 5.
    float cdSkill2;                                                                                 // Tiempo para lanzar la habilidad 2.
    float cdSkill3;                                                                                 // Tiempo para lanzar la habilidad 3.
    float cdSkill4;                                                                                 // Tiempo para lanzar la habilidad 4.
    float cdSkill5;                                                                                 // Tiempo para lanzar la habilidad 5.
    int magicSkill1;                                                                                // Magia para lanzar la habilidad 1.
    int magicSkill2;                                                                                // Magia para lanzar la habilidad 2.
    int magicSkill3;                                                                                // Magia para lanzar la habilidad 3.
    int magicSkill4;                                                                                // Magia para lanzar la habilidad 4.
    int magicSkill5;                                                                                // Magia para lanzar la habilidad 5.
    Text skill1Text;                                                                                // Texto de la habilidad 1.
    Text skill2Text;                                                                                // Texto de la habilidad 2.
    Text skill3Text;                                                                                // Texto de la habilidad 3.
    Text skill4Text;                                                                                // Texto de la habilidad 4.
    Text skill5Text;                                                                                // Texto de la habilidad 5.
    int boost;

    void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();                                     // Componente del movimiento del jugador.
        playerHealth = player.GetComponent<PlayerHealth>();                                         // Componente de la salud del jugador.
        playerMagic = player.GetComponent<PlayerMagic>();                                           // Componente de la magia del jugador.
        playerStats = player.GetComponent<PlayerStats>();                                           // Componente de las estadísticas del jugador.
        enemyPanelManager = GameObject.Find("EnemyPanel").GetComponent<EnemyPanelManager>();        // Componente del panel del enemigo.
        attackAudio = GetComponent<AudioSource>();                                                  // Componente del audio.
        anim = player.GetComponent<Animator>();                                                     // Componente de la animación.
        shootableMask = LayerMask.GetMask("Shootable");                                             // Crear la capa para las cosas a las que se puede hacer daño.
        selected = null;                                                                            // Fijar el seleccionado a nulo.
        skillSelected = "null";                                                                     // Fijar la habilidad seleccinada.
        magicSkill1 = 10;                                                                           // Fijar la magia de la habilidad 1.
        magicSkill2 = 300;                                                                          // Fijar la magia de la habilidad 2.
        magicSkill3 = 225;                                                                          // Fijar la magia de la habilidad 3.
        magicSkill4 = 450;                                                                          // Fijar la magia de la habilidad 4.
        magicSkill5 = 900;                                                                          // Fijar la magia de la habilidad 5.
        skill1Text = skill1Button.GetComponentInChildren<Text>();                                   // Obtener el texto del botón.
        skill2Text = skill2Button.GetComponentInChildren<Text>();                                   // Obtener el texto del botón.
        skill3Text = skill3Button.GetComponentInChildren<Text>();                                   // Obtener el texto del botón.
        skill4Text = skill4Button.GetComponentInChildren<Text>();                                   // Obtener el texto del botón.
        skill5Text = skill5Button.GetComponentInChildren<Text>();                                   // Obtener el texto del botón.
        timer = 1.5f;
        timerSkill2 = 24f;
        timerSkill3 = 12f;
        timerSkill4 = 36f;
        timerSkill5 = 48f;
    }

    void Update()
    {
        timer += Time.deltaTime;                                                                    // Añadir el tiempo desde la última vez que se llamó a esta función.
        timeBetweenAttacks = 1.5f - (playerStats.speed * 0.002f);                                   // Fijar el tiempo entre ataques.

        if (timeBetweenAttacks < 0.1f)                                                              // Si el tiempo entre ataques es menor que el tiempo mínimo entre ataques...
            timeBetweenAttacks = 0.1f;                                                              // ...fijar el tiempo entre ataques al tiempo mínimo entre ataques.

        timerSkill2 += Time.deltaTime;                                                              // Añadir el tiempo desde la última vez que se llamó a esta función.
        timerSkill3 += Time.deltaTime;                                                              // Añadir el tiempo desde la última vez que se llamó a esta función.
        timerSkill4 += Time.deltaTime;                                                              // Añadir el tiempo desde la última vez que se llamó a esta función.
        timerSkill5 += Time.deltaTime;                                                              // Añadir el tiempo desde la última vez que se llamó a esta función.
        cdSkill2 = 24f - (playerStats.speed * 0.02f);                                               // Fijar el tiempo entre habilidades.
        cdSkill3 = 12f - (playerStats.speed * 0.02f);                                               // Fijar el tiempo entre habilidades.
        cdSkill4 = 36f - (playerStats.speed * 0.02f);                                               // Fijar el tiempo entre habilidades.
        cdSkill5 = 48f - (playerStats.speed * 0.02f);                                               // Fijar el tiempo entre habilidades.

        if (cdSkill2 < 10)                                                                          // Si el tiempo para la habilidad es menor que el tiempo mínimo...
            cdSkill2 = 10f;                                                                         // ...fijar el tiempo para la habilidad al tiempo mínimo.

        if (cdSkill3 < 1)                                                                           // Si el tiempo para la habilidad es menor que el tiempo mínimo...
            cdSkill3 = 1f;                                                                          // ...fijar el tiempo para la habilidad al tiempo mínimo.

        if (cdSkill4 < 20)                                                                          // Si el tiempo para la habilidad es menor que el tiempo mínimo...
            cdSkill4 = 20f;                                                                         // ...fijar el tiempo para la habilidad al tiempo mínimo.

        if (cdSkill5 < 30)                                                                          // Si el tiempo para la habilidad es menor que el tiempo mínimo...
            cdSkill5 = 30f;                                                                         // ...fijar el tiempo para la habilidad al tiempo mínimo.

        ButtonInfo();                                                                               // Actualizar la información de los botones.

        if (Input.GetKeyDown(KeyCode.Mouse0))                                                       // Si se pulsa el botón...
        {
            RaycastHit hit;                                                                         // ...crear el rayo.

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100,    // ...si el rayo colisiona con un objeto...
                shootableMask, QueryTriggerInteraction.Ignore))
            {
                selected = hit.collider;                                                            // ...fijar el enemigo seleccionado.
                enemyPanelManager.showEnemyPanel();                                                 // ...mostrar el panel del enemigo.
            }
            else                                                                                    // ...si no...
            {
                selected = null;                                                                    // ...fijar el seleccionado a nulo.
                enemyPanelManager.hideEnemyPanel();                                                 // ...ocultar el panel del enemigo.
            }
        }

        if (selected != null)                                                                       // Si el enemigo seleccionado no es nulo...
        {
            EnemyHealth selectedHealth = selected.GetComponent<EnemyHealth>();                      // ...obtener la salud del enemigo.
            enemyPanelManager.updateEnemyPanel(selected.gameObject);                                // ...actualizar el panel del enemigo.

            if (selectedHealth.currentHealth <= 0)                                                  // ...si el enemigo no tiene salud...
            {
                selected = null;                                                                    // ...fijar el seleccionado a nulo.
                enemyPanelManager.hideEnemyPanel();                                                 // ...ocultar el panel del enemigo.
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerExperience.level >= 3)                        // Si se pulsa el botón y el jugador es del nivel necesario...
            skillSelected = "s2";                                                                   // ...fijar la habilidad seleccionada.

        if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerExperience.level >= 5)                        // Si se pulsa el botón y el jugador es del nivel necesario...
            skillSelected = "s3";                                                                   // ...fijar la habilidad seleccionada.

        if (Input.GetKeyDown(KeyCode.Alpha4) && PlayerExperience.level >= 7)                        // Si se pulsa el botón y el jugador es del nivel necesario...
            skillSelected = "s4";                                                                   // ...fijar la habilidad seleccionada.

        if (Input.GetKeyDown(KeyCode.Alpha5) && PlayerExperience.level >= 9)                        // Si se pulsa el botón y el jugador es del nivel necesario...
            skillSelected = "s5";                                                                   // ...fijar la habilidad seleccionada.

        if (timer >= timeBetweenAttacks && playerHealth.currentHealth > 0 && !anim.GetBool("IsAttacking"))                          // Si es tiempo para atacar y el jugador está vivo...
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Mouse1))               // ...si se pulsa el botón......
            {
                skillSelected = "null";                                                             // ...fijar la habilidad seleccionada.

                if (playerMagic.currentMagic >= magicSkill1)                                        // ...si el jugador tiene magia...
                    Skill1();                                                                       // ...atacar.
                else                                                                                // ...si no...
                    playerHealth.CreateText("Not enough magic!", 25, new Color(0, 1, 1, 1));        // ...crear texto de no hay suficiente magia.

                return;                                                                             // ...salir de la función.
            }

            if (skillSelected == "s2")                                                              // ...si la habilidad está seleccionada...
            {
                skillSelected = "null";                                                             // ...fijar la habilidad seleccionada.

                if (timerSkill2 >= cdSkill2)                                                        // ...si ha pasado el tiempo de carga...
                    if (playerMagic.currentMagic >= magicSkill2)                                    // ...si el jugador tiene magia...
                        Skill2();                                                                   // ...atacar.
                    else                                                                            // ...si no...
                        playerHealth.CreateText("Not enough magic!", 25, new Color(0, 1, 1, 1));    // ...crear texto de no hay suficiente magia.

                return;                                                                             // ...salir de la función.
            }

            if (skillSelected == "s3")                                                              // ...si la habilidad está seleccionada...
            {
                skillSelected = "null";                                                             // ...fijar la habilidad seleccionada.

                if (timerSkill3 >= cdSkill3)                                                        // ...si ha pasado el tiempo de carga...
                    if (playerMagic.currentMagic >= magicSkill3)                                    // ...si el jugador tiene magia...
                        Skill3();                                                                   // ...atacar.
                    else                                                                            // ...si no...
                        playerHealth.CreateText("Not enough magic!", 25, new Color(0, 1, 1, 1));    // ...crear texto de no hay suficiente magia.

                return;                                                                             // ...salir de la función.
            }

            if (skillSelected == "s4")                                                              // ...si la habilidad está seleccionada...
            {
                skillSelected = "null";                                                             // ...fijar la habilidad seleccionada.

                if (timerSkill4 >= cdSkill4)                                                        // ...si ha pasado el tiempo de carga...
                    if (playerMagic.currentMagic >= magicSkill4)                                    // ...si el jugador tiene magia...
                        Skill4();                                                                   // ...atacar.
                    else                                                                            // ...si no...
                        playerHealth.CreateText("Not enough magic!", 25, new Color(0, 1, 1, 1));    // ...crear texto de no hay suficiente magia.

                return;                                                                             // ...salir de la función.
            }

            if (skillSelected == "s5")                                                              // ...si la habilidad está seleccionada...
            {
                skillSelected = "null";                                                             // ...fijar la habilidad seleccionada.

                if (timerSkill5 >= cdSkill5)                                                        // ...si ha pasado el tiempo de carga...
                    if (playerMagic.currentMagic >= magicSkill5)                                    // ...si el jugador tiene magia...
                        Skill5();                                                                   // ...atacar.
                    else                                                                            // ...si no...
                        playerHealth.CreateText("Not enough magic!", 25, new Color(0, 1, 1, 1));    // ...crear texto de no hay suficiente magia.

                return;                                                                             // ...salir de la función.
            }

            if (selected != null)                                                                   // ...si el seleccionado no es nulo...
            {
                skillSelected = "null";                                                             // ...fijar la habilidad seleccionada.

                if (playerMagic.currentMagic >= magicSkill1 && InRange())                           // ...si el jugador tiene magia y está en rango...
                    Skill1();                                                                       // ...atacar.

                return;                                                                             // ...salir de la función.
            }
        }
    }

    void Skill1()
    {
        anim.SetTrigger("AttackTrigger");                                                           // Activar la animación de ataque.
        anim.SetBool("IsAttacking", true);                                                          // Fijar la animación de ataque a verdadero.
        anim.SetInteger("Attack", Random.Range(1, 4));                                              // Seleccionar un ataque.

        particle.SetActive(true);
        spellSelected = spell;                                                                      // Fijar hechizo seleccionado.

        timer = 0f;                                                                                 // Reiniciar el tiempo.
        attackAudio.clip = attackClip[0];
        attackAudio.Play();                                                                         // Encender el audio de ataque.
        playerMovement.SetAttacking(true);                                                          // Fijar atacando a verdadero.
        playerMagic.TakeMagic(magicSkill1);                                                         // Reducir la magia del jugador.
    }

    void Skill2()
    {
        anim.SetTrigger("AttackTrigger");                                                           // Activar la animación de ataque.
        anim.SetBool("IsAttacking", true);                                                          // Fijar la animación de ataque a verdadero.
        anim.SetInteger("Attack", 12);                                                              // Seleccionar un ataque.

        boost = 2;

        timer = 0f;                                                                                 // Reiniciar el tiempo.
        timerSkill2 = 0f;                                                                           // Reiniciar el tiempo de carga.
        attackAudio.clip = attackClip[1];
        attackAudio.Play();                                                                         // Encender el audio de ataque.
        playerMovement.SetAttacking(true);                                                          // Fijar atacando a verdadero.
        playerMagic.TakeMagic(magicSkill2);                                                         // Reducir la magia del jugador.
    }

    void Skill3()
    {
        anim.SetTrigger("AttackTrigger");                                                           // Activar la animación de ataque.
        anim.SetBool("IsAttacking", true);                                                          // Fijar la animación de ataque a verdadero.
        anim.SetInteger("Attack", Random.Range(4, 6));                                              // Seleccionar un ataque.

        particle.SetActive(true);
        spellSelected = spellSkill;                                                                 // Fijar hechizo seleccionado.

        timer = 0f;                                                                                 // Reiniciar el tiempo.
        timerSkill3 = 0f;                                                                           // Reiniciar el tiempo de carga.
        attackAudio.clip = attackClip[2];
        attackAudio.Play();                                                                         // Encender el audio de ataque.
        playerMovement.SetAttacking(true);                                                          // Fijar atacando a verdadero.
        playerMagic.TakeMagic(magicSkill3);                                                         // Reducir la magia del jugador.
    }

    void Skill4()
    {
        anim.SetTrigger("AttackTrigger");                                                           // Activar la animación de ataque.
        anim.SetBool("IsAttacking", true);                                                          // Fijar la animación de ataque a verdadero.
        anim.SetInteger("Attack", 12);                                                              // Seleccionar un ataque.

        s4particle.SetActive(true);
        boost = 4;

        timer = 0f;                                                                                 // Reiniciar el tiempo.
        timerSkill4 = 0f;                                                                           // Reiniciar el tiempo de carga.
        attackAudio.clip = attackClip[3];
        attackAudio.Play();                                                                         // Encender el audio de ataque.
        playerMovement.SetAttacking(true);                                                          // Fijar atacando a verdadero.
        playerMagic.TakeMagic(magicSkill4);                                                         // Reducir la magia del jugador.
    }

    void Skill5()
    {
        anim.SetTrigger("AttackTrigger");                                                           // Activar la animación de ataque.
        anim.SetBool("IsAttacking", true);                                                          // Fijar la animación de ataque a verdadero.
        anim.SetInteger("Attack", 13);                                                              // Seleccionar un ataque.

        particleUlti.SetActive(true);
        particle.SetActive(true);
        spellSelected = spellUlti;                                                                  // Fijar hechizo seleccionado.

        timer = 0f;                                                                                 // Reiniciar el tiempo.
        timerSkill5 = 0f;                                                                           // Reiniciar el tiempo de carga.
        attackAudio.clip = attackClip[4];
        attackAudio.Play();                                                                         // Encender el audio de ataque.
        playerMovement.SetAttacking(true);                                                          // Fijar atacando a verdadero.
        playerMagic.TakeMagic(magicSkill5);                                                         // Reducir la magia del jugador.
    }

    public void SkillShoot()
    {
        Rigidbody arrowInstance = Instantiate(spellSelected, spellSpawn.position, transform.rotation);   // Crear una flecha con la posición y la rotación.
        arrowInstance.velocity = 10 * transform.forward;                                            // Aplicar una fuerza para mover la flecha.

        anim.SetBool("IsAttacking", false);                                                         // Fijar la animación de ataque a falso.
        playerMovement.SetAttacking(false);                                                         // Fijar atacando a falso.

        particle.SetActive(false);
        StartCoroutine("Wait");
    }

    public void SkillBoost()
    {
        if (boost == 2)
        {
            playerHealth.TakeHealth();                                                              // Curar.
            s2particle.GetComponent<ParticleSystem>().Play();
        }
        else if (boost == 4)
        {
            playerStats.energy = 7;                                                                 // Aumentar la energía.
            StartCoroutine("WaitSeconds");                                                          // Empezar corutina.
        }

        anim.SetBool("IsAttacking", false);                                                         // Fijar la animación de ataque a falso.
        playerMovement.SetAttacking(false);                                                         // Fijar atacando a falso.
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(10f);                                                       // Esperar varios segundos.
        playerStats.energy = 5;                                                                     // Reducir la energía.
        s4particle.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        particleUlti.SetActive(false);
    }

    bool InRange()
    {
        return Vector3.Distance(transform.position, selected.transform.position) < 5.5f;            // Cuando el jugador está en rango.
    }

    void ButtonInfo()
    {
        if (timer >= timeBetweenAttacks)                                                            // Si es tiempo de atacar...
        {
            skill1Button.interactable = true;                                                       // ...activar el botón.
            skill1Text.text = "Skill 1";                                                            // ...fijar la habilidad.
        }
        else                                                                                        // Si no...
        {
            skill1Button.interactable = false;                                                      // ...desactivar el botón.
            skill1Text.text = "" + (int)(timeBetweenAttacks - timer);                               // ...fijar el tiempo.
        }

        if (PlayerExperience.level < 3)                                                             // Si el jugador no tiene el nivel necesario...
        {
            skill2Button.interactable = false;                                                      // ...desactivar el botón.
            skill2Text.text = "Level 3";                                                            // ...fijar el nivel.
        }
        else if (timerSkill2 >= cdSkill2)                                                           // Si no si es tiempo de atacar...
        {
            skill2Button.interactable = true;                                                       // ...activar el botón.
            skill2Text.text = "Skill 2";                                                            // ...fijar la habilidad.
        }
        else                                                                                        // Si no...
        {
            skill2Button.interactable = false;                                                      // ...desactivar el botón.
            skill2Text.text = "" + (int)(cdSkill2 - timerSkill2);                                   // ...fijar el tiempo.
        }

        if (PlayerExperience.level < 5)                                                             // Si el jugador no tiene el nivel necesario...
        {
            skill3Button.interactable = false;                                                      // ...desactivar el botón.
            skill3Text.text = "Level 5";                                                            // ...fijar el nivel.
        }
        else if (timerSkill3 >= cdSkill3)                                                           // Si no si es tiempo de atacar...
        {
            skill3Button.interactable = true;                                                       // ...activar el botón.
            skill3Text.text = "Skill 3";                                                            // ...fijar la habilidad.
        }
        else                                                                                        // Si no...
        {
            skill3Button.interactable = false;                                                      // ...desactivar el botón.
            skill3Text.text = "" + (int)(cdSkill3 - timerSkill3);                                   // ...fijar el tiempo.
        }

        if (PlayerExperience.level < 7)                                                             // Si el jugador no tiene el nivel necesario...
        {
            skill4Button.interactable = false;                                                      // ...desactivar el botón.
            skill4Text.text = "Level 7";                                                            // ...fijar el nivel.
        }
        else if (timerSkill4 >= cdSkill4)                                                           // Si no si es tiempo de atacar...
        {
            skill4Button.interactable = true;                                                       // ...activar el botón.
            skill4Text.text = "Skill 4";                                                            // ...fijar la habilidad.
        }
        else                                                                                        // Si no...
        {
            skill4Button.interactable = false;                                                      // ...desactivar el botón.
            skill4Text.text = "" + (int)(cdSkill4 - timerSkill4);                                   // ...fijar el tiempo.
        }

        if (PlayerExperience.level < 9)                                                             // Si el jugador no tiene el nivel necesario...
        {
            skill5Button.interactable = false;                                                      // ...desactivar el botón.
            skill5Text.text = "Level 9";                                                            // ...fijar el nivel.
        }
        else if (timerSkill5 >= cdSkill5)                                                           // Si no si es tiempo de atacar...
        {
            skill5Button.interactable = true;                                                       // ...activar el botón.
            skill5Text.text = "Skill 5";                                                            // ...fijar la habilidad.
        }
        else                                                                                        // Si no...
        {
            skill5Button.interactable = false;                                                      // ...desactivar el botón.
            skill5Text.text = "" + (int)(cdSkill5 - timerSkill5);                                   // ...fijar el tiempo.
        }
    }
}