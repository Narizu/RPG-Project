using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;                          // Referencia al audio.

    bool mute;                                              // Cuando se silencia el audio.
    float sfxCurrent;                                       // Volumen actual de los efectos de sonido.
    float musicCurrent;                                     // Volumen actual de la música.
    float minValue;                                         // Valor mínimo para el volumen.
    float muteValue;

    void Awake()
    {
        mute = false;                                       // Audio no silenciado.
        sfxCurrent = -20f;                                  // Volumen inicial de los efectos de sonido al máximo.
        musicCurrent = -20f;                                // Volumen inicial de la música al máximo.
        minValue = -40f;                                    // Volumen fijado al valor mínimo.
        muteValue = -80f;
    }

    public void SetSfxLvl(float sfxLvl)
    {
        if (!mute)                                          // Si el audio no está silenciado...
            masterMixer.SetFloat("sfxVol", sfxLvl);         // ...fijar el nivel de los efectos de sonido.

        sfxCurrent = sfxLvl;                                // Fijar el volumen actual de los efectos de sonido.
    }

    public void SetMusicLvl(float musicLvl)
    {
        if (!mute)                                          // Si el audio no está silenciado...
            masterMixer.SetFloat("musicVol", musicLvl);     // ...fijar el nivel de la música.

        musicCurrent = musicLvl;                            // Fijar el volumen actual de la música.
    }

    public void Mute()
    {
        if (mute)                                           // Si el audio está silenciado...
        {
            masterMixer.SetFloat("sfxVol", sfxCurrent);     // ...fijar el nivel de los efectos de sonido a su volumen actual.
            masterMixer.SetFloat("musicVol", musicCurrent); // ...fijar el nivel de la música a su volumen actual
            mute = false;                                   // ...dejar de silenciar el audio.
        }
        else                                                // Si no...
        {
            masterMixer.SetFloat("sfxVol", muteValue);      // ...fijar el nivel de los efectos de sonido al mínimo.
            masterMixer.SetFloat("musicVol", muteValue);    // ...fijar el nivel de la música al mínimo.
            mute = true;                                    // ...silenciar el audio.
        }
    }
}