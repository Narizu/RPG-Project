using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Transform healthUI;                                                          // Referencia a la interfaz de la salud del enemigo.
    public Slider healthSlider;                                                         // Referencia a la barra de salud.
    public Image healthFill;                                                            // Color de la barra de salud.
    public Text healthText;                                                             // Texto de la salud.
    public AudioClip[] hitClip;                                                         // Clip de audio para cuando atacan al enemigo.
    public AudioClip criticalClip;                                                      // Clip de audio para cuando atacan al enemigo con un golpe crítico.
    public AudioClip deathClip;                                                         // Clip de audio para cuando el enemigo muere.
    public float currentHealth;                                                         // Salud actual que tiene el enemigo.
    public ParticleSystem particle;

    Animator anim;                                                                      // Referencia a la animación.
    AudioSource enemyAudio;                                                             // Referencia al audio.
    CapsuleCollider capsuleCollider;                                                    // Referencia a la cápsula de colisión.
    GameObject player;                                                                  // Referencia al jugador.
    PlayerExperience playerExp;                                                         // Referencia a la experiencia del jugador.
    PlayerStats playerStats;                                                            // Referencia a las estadísticas del jugador.
    EnemyStats enemyStats;                                                              // Referencia a las estadísticas del enemigo.
    bool isDead;                                                                        // Cuando el enemigo muere.
    bool isSinking;                                                                     // Cuando el enemigo empieza a hundirse en el suelo.
    bool firstTime;                                                                     // Cuando es la primera vez.
    float sinkSpeed;                                                                    // Velocidad con la que el enemigo se hundirá en el suelo cuando muera.
    float attack;                                                                       // Ataque.
    float defense;                                                                      // Defensa.
    float magic;                                                                        // Magia.
    float damage;                                                                       // Daño causado al enemigo por el jugador.
    float percentage;                                                                   // Porcentaje aleatorio.
    float criticalHit;                                                                  // Porcentaje para dar un golpe crítico.
    float maxHealth;                                                                    // Salud máxima del enemigo.
    float propHealth;                                                                   // Salud proporcional.
    int scoreValue;                                                                     // Cantidad añadida a la puntuación del jugador cuando el enemigo muere.
    int var;                                                                            // Variable.
    float radius;                                                                       // Radio del enemigo.

    void Awake()
    {
        anim = GetComponent<Animator>();                                                // Componente de la animación.
        enemyAudio = GetComponent<AudioSource>();                                       // Componente del audio.
        capsuleCollider = GetComponent<CapsuleCollider>();                              // Componente de la cápsula de colisión.
        enemyStats = GetComponent<EnemyStats>();                                        // Componente de las estadísticas del enemigo.

        currentHealth = 7000f;                                                          // Fijar la salud actual cuando aparece el primer enemigo.
        firstTime = true;                                                               // Fijar la primera vez a verdadero.
        sinkSpeed = 2.5f;                                                               // Velocidad con la que el enemigo se hundirá en el suelo cuando muera.
        scoreValue = 1;                                                                 // Cantidad añadida a la puntuación del jugador cuando el enemigo muere.
        var = 10;                                                                       // Fijar la variable.
        radius = GetComponent<Collider>().bounds.extents.y;                             // Calcular el radio del enemigo.
        healthUI.position = transform.position + new Vector3(0f, radius * 2, 0f);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");                            // Componente del jugador.
        playerExp = player.GetComponent<PlayerExperience>();                            // Componente de la experiencia del jugador.
        playerStats = player.GetComponent<PlayerStats>();                               // Componente de las estadísticas del jugador.
        healthText.color = new Color(0, 0, 0, 1);
    }

    void Update()
    {
        if (isSinking)                                                                  // Si el enemigo debería estar hundiéndose...
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);              // ...mover el enemigo hacia abajo con la velocidad de hundimiento por segundo.

        maxHealth = enemyStats.health * var;                                            // Fijar la salud máxima del enemigo.
        healthSlider.maxValue = maxHealth;                                              // Fijar el valor máximo de la barra de salud del enemigo.

        if (firstTime)                                                                  // Si es la primera vez...
        {
            firstTime = false;                                                          // ...fijar la primera vez a falso.
            healthSlider.value = maxHealth;                                             // ...fijar el valor inicial de la barra de salud del enemigo.
            currentHealth = maxHealth;                                                  // ...fijar la salud inicial del enemigo.
        }

        healthText.text = (int)currentHealth + "/" + maxHealth;                         // Fijar texto de la salud.
    }

    public void TakeDamage(float skill)
    {
        if (isDead)                                                                     // Si el enemigo está muerto...
            return;                                                                     // ...salir de la función.

        attack = playerStats.attack * playerStats.attack;                               // Calcular el ataque inicial.
        defense = playerStats.attack + enemyStats.defense;                              // Calcular la defensa física inicial.
        attack /= defense;                                                              // Calcular el ataque final.
        magic = playerStats.magic * playerStats.magic;                                  // Calcular la magia inicial.
        defense = playerStats.magic + enemyStats.defense;                               // Calcular la defensa mágica inicial.
        magic /= defense;                                                               // Calcular la magia final.
        damage = attack + magic * skill;                                                // Calcular el daño total.

        if (damage < 1f)                                                                // Si el daño total es menor que el daño mínimo...
            damage = 1f;                                                                // ...fijar el daño total al daño mínimo.

        percentage = Random.Range(0f, 100f);                                            // Calcular un porcentaje aleatorio.
        criticalHit = 10 + (playerStats.speed * 0.01f);                                 // Calcular el porcentaje de golpe crítico.

        if (criticalHit > 20)                                                           // Si el golpe crítico está por encima del porcentaje máximo...
            criticalHit = 20f;                                                          // ...fijar el golpe crítico al porcentaje máximo.

        if (percentage < criticalHit)                                                   // Si el porcentaje aleatorio coincide con el porcentaje de golpe crítico...
        {
            anim.SetInteger("GetHit", 5);                                               // Seleccionar el golpe.
            anim.SetTrigger("HitTrigger");                                              // Activar la animación de recibir golpe.
            particle.Play();
            damage *= 1.5f;                                                             // ...multiplicar el daño total por el daño del golpe crítico.
            CreateText("-" + (int) damage, 50, new Color(1, 0, 1, 1));                  // ...crear texto del daño recibido.
            enemyAudio.clip = criticalClip;                                             // ...poner el clip de audio de golpe crítico.
            enemyAudio.Play();                                                          // ...encender el efecto de sonido de herido.
        }
        else if (percentage < 20f)                                                      // Si no si el porcentaje aleatorio coincide con el porcentaje de fallo...
        {
            damage = 0f;                                                                // ...fijar el daño total a cero.
            CreateText("Miss!", 25, new Color(1, 1, 1, 1));                             // ...crear texto del daño recibido.
        }
        else                                                                            // Si no...
        {
            anim.SetInteger("GetHit", Random.Range(1, 5));                              // Seleccionar el golpe.
            anim.SetTrigger("HitTrigger");                                              // Activar la animación de recibir golpe.
            particle.Play();
            CreateText("-" + (int)damage, 25, new Color(1, 0, 0, 1));                   // ...crear texto del daño recibido.
            enemyAudio.clip = hitClip[Random.Range(0, 2)];                              // ...poner el clip de audio de herido.
            enemyAudio.Play();                                                          // ...encender el efecto de sonido de herido.
        }

        currentHealth -= damage;                                                        // Reducir la salud actual por la cantidad de daño.

        if (currentHealth < 0)                                                          // Si la salud del jugador es menor a cero...
            currentHealth = 0;                                                          // ...fijar la salud a cero.

        healthSlider.value = currentHealth;                                             // Fijar el valor de la barra de salud a la salud actual.

        ChangeColor();                                                                  // Cambiar el color de la barra de salud.

        if (currentHealth <= 0)                                                         // Si la salud actual es menor o igual a cero...
            Death();                                                                    // ...morir.
    }

    void Death()
    {
        isDead = true;                                                                  // Fijar el enemigo a muerto.

        anim.SetBool("IsDead", true);
        anim.SetTrigger("Dead");                                                        // Decir a la animación que el enemigo está muerto.

        enemyAudio.clip = deathClip;                                                    // Poner el clip de audio de muerte.
        enemyAudio.Play();                                                              // Encender el audio.

        capsuleCollider.isTrigger = true;                                               // Quitar la colisión para que pueda ser atravesado.
    }

    public void StartSinking()
    {
        Destroy(healthUI.gameObject);

        GetComponent<NavMeshAgent>().enabled = false;                                   // Encontrar y desactivar el agente de navegación.
        GetComponent<Rigidbody>().isKinematic = true;                                   // Encontrar el componente del cuerpo y hacerlo cinemático.
        isSinking = true;                                                               // Fijar el hundimiento.
        ScoreManager.score += scoreValue;                                               // Incrementar la puntuación por el valor de puntuación del enemigo.

        if (gameObject.tag == "EnemyBoss")                                              // Si el enemigo es un jefe...
            playerExp.TakeExp(20 * Mathf.Pow(2f, enemyStats.level - 1));                // ...incrementar la experiencia del jugador.
        else
            playerExp.TakeExp(10 * Mathf.Pow(2f, enemyStats.level - 1));                // Incrementar la experiencia del jugador.

        Destroy(gameObject, 2f);                                                        // Destruir el enemigo después de 2 segundos.
    }

    void ChangeColor()
    {
        propHealth = currentHealth / maxHealth;                                         // Calcular la salud proporcional.
        float x = 1 - (1 - propHealth) * 2;                                             // Calcular el color.

        if (currentHealth >= maxHealth / 2)                                             // Si la salud está por encima del 50%...
        {
            healthFill.color = new Color(2 * (1 - propHealth), 1, 0, 1);                // ...cambiar el color de verde a amarillo.
            //healthText.color = new Color(x, x, x, 1);                                   // ...cambiar el color del texto.
        }
        else                                                                            // Si no...
        {
            healthFill.color = new Color(1, 2 * propHealth, 0, 1);                      // ...cambiar el color de amarillo a rojo.
            //healthText.color = new Color(0, 0, 0, 1);                                   // ...cambiar el color del texto.
        }
    }

    void CreateText(string text, int size, Color color)
    {
        GameObject textUIGO = new GameObject("textUIGO");                               // Objeto en el que pondremos el texto.
        textUIGO.layer = LayerMask.NameToLayer("UI");                                   // Fijar el objeto en la capa de la interfaz de usuario.
        textUIGO.transform.SetParent(healthUI);                                         // Fijar la interfaz de la salud del enemigo como padre del objeto.

        RectTransform transUI = textUIGO.AddComponent<RectTransform>();                 // Componente de la transformación del objeto.
        transUI.localPosition = new Vector3(0f, 0f, 0f);                                // Fijar la posición del objeto.
        transUI.sizeDelta = new Vector2(300f, 100f);                                    // Fijar tamaño del objeto.
        transUI.localRotation = Quaternion.Euler(0f, 0f, 0f);                           // Fijar la rotación del objeto.
        transUI.localScale = new Vector3(0f, 0f, 0f);                                   // Fijar la escala del objeto.

        Text textUI = textUIGO.AddComponent<Text>();                                    // Componente del texto del objeto.
        textUI.text = text;                                                             // Fijar el texto.
        textUI.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");    // Fijar la fuente del texto.
        textUI.fontStyle = FontStyle.Bold;                                              // Fijar el estilo del texto.
        textUI.fontSize = size;                                                         // Fijar el tamaño de la fuente del texto.
        textUI.alignment = TextAnchor.MiddleCenter;                                     // Fijar la posición del texto.
        textUI.color = color;                                                           // Fijar el color del texto.

        Animation anima = textUIGO.AddComponent<Animation>();                           // Animación del objeto.
        AnimationCurve curve;                                                           // Curva de la animación.

        AnimationClip clip = new AnimationClip();                                       // Clip de la animación.
        clip.legacy = true;                                                             // Fijar el tipo de clip.

        Keyframe[] keys;                                                                // Llaves de la animación.
        keys = new Keyframe[3];                                                         // Fijar el número de llaves a 3.
        keys[0] = new Keyframe(0f, 0f);                                                 // Llave para el incio.
        keys[1] = new Keyframe(0.5f, 25f);                                              // Llave para el medio.
        keys[2] = new Keyframe(2f, 50f);                                                // Llave para el final.
        curve = new AnimationCurve(keys);                                               // Crear la curva con las llaves.
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);                 // Fijar la curva en el clip.

        keys[0] = new Keyframe(0f, 0f);                                                 // Llave para el incio.
        keys[1] = new Keyframe(0.5f, 1f);                                               // Llave para el medio.
        keys[2] = new Keyframe(2f, 0f);                                                 // Llave para el final.
        curve = new AnimationCurve(keys);                                               // Crear la curva con las llaves.
        clip.SetCurve("", typeof(Transform), "localScale.x", curve);                    // Fijar la curva en el clip.
        clip.SetCurve("", typeof(Transform), "localScale.y", curve);                    // Fijar la curva en el clip.

        anima.AddClip(clip, clip.name);                                                 // Añadir el clip.
        anima.Play(clip.name);                                                          // Reproducir el clip.

        Destroy(textUIGO, 2f);                                                          // Destruir el objeto después de 2 segundos.
    }
}