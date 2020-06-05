using UnityEngine;

public class NPCQuest : MonoBehaviour
{
    public QuestManager questManager;                   // Referencia al creador de las quest.
    public AudioClip[] talk;

    Animator anim;
    AudioSource npcAudio;

    void Start()
    {
        anim = GetComponent<Animator>();
        npcAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (anim.GetBool("IsTalking"))
            anim.SetInteger("Talk", Random.Range(1, 9));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")                      // Si colisiona con el jugador...
        {
            anim.SetBool("IsTalking", true);
            npcAudio.clip = talk[Random.Range(0, 3)];
            npcAudio.Play();
            questManager.showQuest();                   // ...activar la quest.
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")                      // Si deja de colisionar con el jugador...
        {
            questManager.hideQuest();                   // ...desactivar la quest.
            anim.SetBool("IsTalking", false);
        }
    }
}