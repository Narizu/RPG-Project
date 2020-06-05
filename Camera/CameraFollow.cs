using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    Transform target;                                                                                       // Posición que la cámara seguirá.
    Vector3 offset;                                                                                         // Distancia inicial del objetivo.
    float smoothing;                                                                                        // Velocidad con la que la camara seguirá.
    RaycastHit[] hits;                                                                                      // Objetos a los que golpea el rayo.
    Shader shader1;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;                                      // Fijar el jugador como objetivo de la cámara.
        offset = transform.position - target.position;                                                      // Calcular la distancia inicial.
        smoothing = 5f;                                                                                     // Velocidad con la que la camara seguirá.
        hits = null;                                                                                        // Fijar los objetos a los que golpea el rayo a nulo.
        shader1 = Shader.Find("Standard");
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;                                                    // Crear una posición a la que apunte la cámara basada en la distancia del objetivo.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);    // Interpolar desde la posición actual de la cámara hasta la posición del objetivo.

        if (SceneManager.GetActiveScene().name == "Level01Tutorial" ||
            SceneManager.GetActiveScene().name == "Level01" ||
            SceneManager.GetActiveScene().name == "Level02")
            return;

        if (hits != null)                                                                                   // Si hay objetos golpeados por el rayo...
            foreach (RaycastHit hit in hits)                                                                // ...para cada objeto golpeado por el rayo...
            {
                if (hit.collider == null)
                    continue;

                if (hit.collider.tag == "Floor" || hit.collider.tag == "Player" ||                          // ...si el objeto golpeado es el suelo o el jugador...
                    hit.collider.tag == "Enemy" || hit.collider.tag == "EnemyBoss" ||
                    hit.collider.tag == "Item" || hit.collider.tag == "Spell")                            
                    continue;                                                                               // ...continuar con el siguiente objeto.

                Renderer r = hit.collider.GetComponent<Renderer>();                                         // ...obtener el renderizado del objeto golpeado.

                if (r)                                                                                      // ...si existe el renderizado del objeto golpeado...
                {
                    r.material.shader = shader1;                                                            // ...fijar el shader del renderizado al shader estándar.
                }
            }

        Vector3 playerPos = target.transform.position + new Vector3(0f, 2f, -1f);                            // Calcular la posición del jugador y coger un punto más elevado.
            
        hits = Physics.SphereCastAll(transform.position, 1f, playerPos - transform.position,                // Obtener los objetos golpeados por el rayo.
            Vector3.Distance(transform.position, playerPos));
        
        foreach (RaycastHit hit in hits)                                                                    // Para cada objeto golpeado por el rayo...
        {
            if (hit.collider == null)
                continue;

            if (hit.collider.tag == "Floor" || hit.collider.tag == "Player" ||                              // ...si el objeto golpeado es el suelo o el jugador...
                hit.collider.tag == "Enemy" || hit.collider.tag == "EnemyBoss" ||
                hit.collider.tag == "Item" || hit.collider.tag == "Spell")                                
                continue;                                                                                   // ...continuar con el siguiente objeto.

            Renderer r = hit.collider.GetComponent<Renderer>();                                             // ...obtener el renderizado del objeto golpeado.
            Shader shader2 = Shader.Find("Transparent/Diffuse");                                            // ...crear un shader transparente.
            
            if (r)                                                                                          // ...si existe el renderizado del objeto golpeado...
            {
                shader1 = Shader.Find(r.material.shader.name);
                r.material.shader = shader2;                                                                // ...fijar el shader del renderizado al shader transparente.
                r.material.color = new Color(r.material.color.r,                                            // ...fijar el color del renderizado a semitransparente.
                    r.material.color.g, r.material.color.b, 0.25f);
            }
        }
    }
}