using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image damageImage;                                                                               // Referencia a la imagen que aparece en pantalla cuando se hiere al jugador.
    public Transform canvasHUD;                                                                             // Referencia a la interfaz del jugador.
    public Slider healthSlider;                                                                             // Referencia a la barra de salud.
    public Image healthFill;                                                                                // Color de la barra de salud.
    public Text healthText;                                                                                 // Texto de la salud.
    public AudioClip[] hitClip;                                                                             // Clip de audio para cuando el jugador es herido.
    public AudioClip[] deathClip;                                                                           // Clip de audio para cuando el jugador muere.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);                                                 // Color de la imagen de daño.
    public float flashSpeed = 5f;                                                                           // Velocidad con la que la imagen de daño se desvanecerá.
    public float currentHealth;                                                                             // Salud actual que tiene el jugador.
    public ParticleSystem particle;

    Animator anim;                                                                                          // Referencia a la animación.
    AudioSource playerAudio;                                                                                // Referencia al audio.
    PlayerMovement playerMovement;                                                                          // Referencia al movimiento del jugador.
    PlayerStats playerStats;                                                                                // Referencia a las estadísticas del jugador.
    bool isDead;                                                                                            // Cuando el jugador muere.
    bool damaged;                                                                                           // Cuando el jugador recibe daño.
    bool firstTime;                                                                                         // Cuando es la primera vez.
    float timer;                                                                                            // Tiempo para contar cuando se hace la siguiente regeneración.
    float timeBetweenRegenerations;                                                                         // Tiempo en segundos entre cada regeneración.
    float timerHit;                                                                                         // Tiempo para contar cuando se empieza a regenerar.
    float timeStartRegeneration;                                                                            // Tiempo en segundos para empezar a regenerarse.
    float attack;                                                                                           // Ataque.
    float defense;                                                                                          // Defensa.
    float damage;                                                                                           // Daño causado al jugador por el enemigo.
    float maxHealth;                                                                                        // Salud máxima del jugador.
    float propHealth;                                                                                       // Salud proporcional.
    float restoredHealth;                                                                                   // Salud restaurada.
    int var;                                                                                                // Variable.
    IKHands ikHands;

    void Awake()
    {
        anim = GetComponent<Animator>();                                                                    // Componente de la animación.
        playerAudio = GetComponent<AudioSource>();                                                          // Componente del audio.
        playerMovement = GetComponent<PlayerMovement>();                                                    // Componente del movimiento del jugador.
        playerStats = GetComponent<PlayerStats>();                                                          // Componente de las estadísticas del jugador.
        currentHealth = 100f;                                                                               // Fijar una salud inicial por encima de 0 para que no se acabe el juego.
        firstTime = true;                                                                                   // Fijar la primera vez a verdadero.
        timeBetweenRegenerations = 0.1f;                                                                    // Fijar el tiempo entre regeneraciones.
        timeStartRegeneration = 3f;                                                                         // Fijar el tiempo para empezar a regenerarse.
        var = 10;                                                                                           // Fijar la variable.
        healthText.color = new Color(0, 0, 0, 1);

        if (PlayerStats.character == "Warrior")
            ikHands = GetComponent<IKHands>();
    }

    void Update()
    {
        if (damaged)                                                                                        // Si el jugador ha sido dañado...
            damageImage.color = flashColour;                                                                // ...fijar el color de la imagen de daño.

        else                                                                                                // Si no...
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);    // ...hacer una transición para limpiar el color.

        damaged = false;                                                                                    // Reiniciar el daño.
        
        maxHealth = playerStats.health * var;                                                               // Fijar la salud máxima del jugador.
        healthSlider.maxValue = maxHealth;                                                                  // Fijar el valor máximo de la barra de salud del jugador.

        if (firstTime)                                                                                      // Si es la primera vez...
        {
            firstTime = false;                                                                              // ...fijar la primera vez a falso.
            healthSlider.value = maxHealth;                                                                 // ...fijar el valor inicial de la barra de salud del jugador.
            currentHealth = maxHealth;                                                                      // ...fijar la salud inicial del jugador.
        }

        timer += Time.deltaTime;                                                                            // Añadir el tiempo desde la última vez que se llamó a esta función.
        timerHit += Time.deltaTime;                                                                         // Añadir el tiempo desde la última vez que se llamó a esta función.
        
        if (timerHit >= timeStartRegeneration && timer >= timeBetweenRegenerations                          // Si es tiempo para regenerarse y
            && currentHealth > 0 && currentHealth < maxHealth)                                              // el jugador está vivo pero no tiene la salud al máximo...
            Regeneration();                                                                                 // ...regenerarse.

        healthText.text = (int)currentHealth + "/" + maxHealth;                                             // Fijar texto de la salud.
    }

    public void TakeDamage(float enemyAttack)
    {
        anim.SetInteger("GetHit", Random.Range(1, 6));                                                      // Seleccionar el golpe.
        anim.SetTrigger("HitTrigger");                                                                      // Activar la animación de recibir golpe.

        particle.Play();

        timerHit = 0f;                                                                                      // Reiniciar el tiempo.

        damaged = true;                                                                                     // Fijar el daño en la pantalla.

        attack = enemyAttack * enemyAttack;                                                                 // Calcular el ataque inicial.
        defense = enemyAttack + playerStats.defense;                                                        // Calcular la defensa inicial.
        attack /= defense;                                                                                  // Calcular el ataque final.
        damage = attack * 2;                                                                                // Calcular el daño total.

        if (damage < 1f)                                                                                    // Si el daño total es menor que el daño mínimo...
            damage = 1f;                                                                                    // ...fijar el daño total al daño mínimo.

        CreateText("-" + (int)damage, 35, new Color(1, 0, 0, 1));                                           // Crear texto del daño recibido.

        currentHealth -= damage;                                                                            // Reducir la salud actual por la cantidad de daño.

        if (currentHealth < 0)                                                                              // Si la salud del jugador es menor a cero...
            currentHealth = 0;                                                                              // ...fijar la salud a cero.

        healthSlider.value = currentHealth;                                                                 // Fijar el valor de la barra de salud a la salud actual.

        ChangeColor();                                                                                      // Cambiar el color de la barra de salud.

        playerAudio.clip = hitClip[Random.Range(0, 5)];                                                     // Poner el clip de audio de herido.
        playerAudio.Play();                                                                                 // Encender el efecto de sonido de herido.

        if (currentHealth <= 0 && !isDead)                                                                  // Si el jugador ha perdido toda su salud y todavía no está muerto...
            Death();                                                                                        // ...morir.
    }

    public void TakeHealth()
    {
        restoredHealth = maxHealth * 0.5f;                                                                  // Calcular la salud restaurada.

        if (maxHealth - currentHealth < restoredHealth)                                                     // Si la salud que nos queda por restaurar es menor que la salud restaurada...
            restoredHealth = maxHealth - currentHealth;                                                     // ...fijar la salud restaurada a la salud que nos queda por restaurar.

        CreateText("+" + (int)restoredHealth, 45, new Color(0, 1, 0, 1));                                   // Crear texto de la salud restaurada.

        currentHealth += restoredHealth;                                                                    // Aumentar la salud actual por la salud restaurada.

        if (currentHealth > maxHealth)                                                                      // Si la salud del jugador es mayor que la salud máxima...
            currentHealth = maxHealth;                                                                      // ...fijar la salud del jugador a la salud máxima.

        healthSlider.value = currentHealth;                                                                 // Fijar el valor de la barra de salud a la salud actual.

        ChangeColor();                                                                                      // Cambiar el color de la barra de salud.
    }

    void Death()
    {
        if (PlayerStats.character == "Warrior")
            ikHands.enabled = false;

        isDead = true;                                                                                      // Fijar el jugador a muerto para que esta función no vuelva a ser llamada.

        anim.SetBool("IsDead", true);
        anim.SetTrigger("Die");                                                                             // Decir a la animación que el jugador está muerto.

        playerAudio.clip = deathClip[Random.Range(0, 2)];                                                   // Poner el clip de audio de muerte.
        playerAudio.Play();                                                                                 // Encender el audio.

        playerMovement.enabled = false;                                                                     // Apagar el movimiento del jugador.
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Regeneration()
    {
        timer = 0f;                                                                                         // Reiniciar el tiempo.

        currentHealth += (maxHealth * 0.0025f);                                                             // Aumentar la salud actual por la cantidad de regeneración.

        if (currentHealth > maxHealth)                                                                      // Si el jugador tiene más del máximo de su salud...
            currentHealth = maxHealth;                                                                      // ...fijar la salud del jugador al máximo.

        healthSlider.value = currentHealth;                                                                 // Fijar el valor de la barra de salud a la salud actual.

        ChangeColor();                                                                                      // Cambiar el color de la barra de salud.
    }

    void ChangeColor()
    {
        propHealth = currentHealth / maxHealth;                                                             // Calcular la salud proporcional.
        float x = 1 - (1 - propHealth) * 2;                                                                 // Calcular el color.

        if (currentHealth >= maxHealth / 2)                                                                 // Si la salud está por encima del 50%...
        {
            healthFill.color = new Color(2 * (1 - propHealth), 1, 0, 1);                                    // ...cambiar el color de verde a amarillo.
            //healthText.color = new Color(x, x, x, 1);                                                       // ...cambiar el color del texto.
        }
        else                                                                                                // Si no...
        {
            healthFill.color = new Color(1, 2 * propHealth, 0, 1);                                          // ...cambiar el color de amarillo a rojo.
            //healthText.color = new Color(0, 0, 0, 1);                                                       // ...cambiar el color del texto.
        }
    }

    public void CreateText(string text, int size, Color color)
    {
        GameObject textUIGO = new GameObject("textUIGO");                                                   // Objeto en el que pondremos el texto.
        textUIGO.layer = LayerMask.NameToLayer("UI");                                                       // Fijar el objeto en la capa de la interfaz de usuario.
        textUIGO.transform.SetParent(canvasHUD);                                                            // Fijar la interfaz de la salud del enemigo como padre del objeto.

        RectTransform transUI = textUIGO.AddComponent<RectTransform>();                                     // Componente de la transformación del objeto.
        transUI.localPosition = new Vector3(0f, 0f, 0f);                                                    // Fijar la posición del objeto.
        transUI.sizeDelta = new Vector2(300f, 100f);                                                        // Fijar tamaño del objeto.
        transUI.localRotation = Quaternion.Euler(0f, 0f, 0f);                                               // Fijar la rotación del objeto.
        transUI.localScale = new Vector3(0f, 0f, 0f);                                                       // Fijar la escala del objeto.

        Text textUI = textUIGO.AddComponent<Text>();                                                        // Componente del texto del objeto.
        textUI.text = text;                                                                                 // Fijar el texto.
        textUI.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");                        // Fijar la fuente del texto.
        textUI.fontStyle = FontStyle.Bold;                                                                  // Fijar el estilo del texto.
        textUI.fontSize = size;                                                                             // Fijar el tamaño de la fuente del texto.
        textUI.alignment = TextAnchor.MiddleCenter;                                                         // Fijar la posición del texto.
        textUI.color = color;                                                                               // Fijar el color del texto.

        Animation anima = textUIGO.AddComponent<Animation>();                                               // Animación del objeto.
        AnimationCurve curve;                                                                               // Curva de la animación.

        AnimationClip clip = new AnimationClip();                                                           // Clip de la animación.
        clip.legacy = true;                                                                                 // Fijar el tipo de clip.

        Keyframe[] keys;                                                                                    // Llaves de la animación.
        keys = new Keyframe[3];                                                                             // Fijar el número de llaves a 3.
        keys[0] = new Keyframe(0f, 150f);                                                                   // Llave para el incio.
        keys[1] = new Keyframe(0.5f, 175f);                                                                 // Llave para el medio.
        keys[2] = new Keyframe(2f, 200f);                                                                   // Llave para el final.
        curve = new AnimationCurve(keys);                                                                   // Crear la curva con las llaves.
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);                                     // Fijar la curva en el clip.

        keys[0] = new Keyframe(0f, 0f);                                                                     // Llave para el incio.
        keys[1] = new Keyframe(0.5f, 1f);                                                                   // Llave para el medio.
        keys[2] = new Keyframe(2f, 0f);                                                                     // Llave para el final.
        curve = new AnimationCurve(keys);                                                                   // Crear la curva con las llaves.
        clip.SetCurve("", typeof(Transform), "localScale.x", curve);                                        // Fijar la curva en el clip.
        clip.SetCurve("", typeof(Transform), "localScale.y", curve);                                        // Fijar la curva en el clip.

        anima.AddClip(clip, clip.name);                                                                     // Añadir el clip.
        anima.Play(clip.name);                                                                              // Reproducir el clip.

        Destroy(textUIGO, 2f);                                                                              // Destruir el objeto después de 2 segundos.
    }
}