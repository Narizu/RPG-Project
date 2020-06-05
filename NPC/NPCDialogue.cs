using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueUI;           // Referencia al diálogo.
    public Text dialogueText;               // Referencia al texto del diálogo.

    int randomText;                         // Número aleatorio para el texto.

    void Start()
    {
        randomText = Random.Range(1, 32);   // Buscar un número aleatorio.
        
        switch (randomText)                 // Poner el texto correspondiente a ese número.
        {
            case 1:
                dialogueText.text = "Hello World!";
                break;
            case 2:
                dialogueText.text = "Subscribe to Narizu1994";
                break;
            case 3:
                dialogueText.text = "My nose itches";
                break;
            case 4:
                dialogueText.text = "Luffy will become the King of the Pirates";
                break;
            case 5:
                dialogueText.text = "Narizu is handsome";
                break;
            case 6:
                dialogueText.text = "Remember to accept the Quest";
                break;
            case 7:
                dialogueText.text = "Let's see if I die ...";
                break;
            case 8:
                dialogueText.text = "Happy Unbirthday!";
                break;
            case 9:
                dialogueText.text = "I am an NPC and I do nothing";
                break;
            case 10:
                dialogueText.text = "It was a Sunday afternoon...";
                break;
            case 11:
                dialogueText.text = "Come for me, but with your face uncovered";
                break;
            case 12:
                dialogueText.text = "A glow and PUM! and I say war is here";
                break;
            case 13:
                dialogueText.text = "You will never leave the Friendzone"; // Draceon
                break;
            case 14:
                dialogueText.text = "Are you sure about that?"; // Naide
                break;
            case 15:
                dialogueText.text = "I pray to God Oda";
                break;
            case 16:
                dialogueText.text = "I am Oden and I was born to be boiled!";
                break;
            case 17:
                dialogueText.text = "Get me out of here!";
                break;
            case 18:
                dialogueText.text = "I stay at home";
                break;
            case 19:
                dialogueText.text = "Do you want some Coronavirus?";
                break;
            case 20:
                dialogueText.text = "This videogame is GOTY!";
                break;
            case 21:
                dialogueText.text = "Shut up... please... Shut up! Shut up!!!";
                break;
            case 22:
                dialogueText.text = "I think it's around here"; // Eddy
                break;
            case 23:
                dialogueText.text = "I'm a carefree"; // Marta
                break;
            case 24:
                dialogueText.text = "They give crowns to the one who deserves a flying kick"; // Dani
                break;
            case 25:
                dialogueText.text = "I am the bone of my sword"; // Viciado
                break;
            case 26:
                dialogueText.text = "The fault is always with the jungle"; // Plomxa
                break;
            case 27:
                dialogueText.text = "I will never give up, that's my ninja way"; // Leonvio
                break;
            case 28:
                dialogueText.text = "People die... when they are forgotten";
                break;
            case 29:
                dialogueText.text = "The only way to be smarter is to face a smarter opponent";
                break;
            case 30:
                dialogueText.text = "Every day is... like a photocopy of the previous one";
                break;
            case 31:
                dialogueText.text = "You have an error in the code";
                break;
            default:
                dialogueText.text = "You have an error in the code";
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")          // Si colisiona con el jugador...
            dialogueUI.SetActive(true);     // ...activar el diálogo.
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")          // Si deja de colisionar con el jugador...
            dialogueUI.SetActive(false);    // ...desactivar el diálogo.
    }
}