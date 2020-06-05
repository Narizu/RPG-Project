using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public List<GameObject> enabledObjects = new List<GameObject>();                        // Lista de objetos habilitados en el personaje.

    public CharacterObjectGroups male;                                                      // Lista masculina.

    public CharacterObjectGroups female;                                                    // Lista femenina.

    public CharacterObjectListsAllGender allGender;                                         // Lista universal.

    int playerLevel = 1;
    int skinLevel = 0;

    void Start()
    {
        BuildLists();                                                                       // Reconstruir todas las listas.

        if (enabledObjects.Count != 0)                                                      // Deshabilitar cualquier objeto habilitado antes de borrar.
        {
            foreach (GameObject g in enabledObjects)
            {
                g.SetActive(false);
            }
        }

        enabledObjects.Clear();                                                             // Borrar lista de objetos habilitados.

        if (PlayerStats.gender == "Female")                                                 // Establecer personaje femenino predeterminado.
        {
            ActivateItem(female.head[0]);
            ActivateItem(female.eyebrow[0]);
            ActivateItem(female.facialHair[0]);
            ActivateItem(female.torso[0]);
            ActivateItem(female.arm_Upper_Right[0]);
            ActivateItem(female.arm_Upper_Left[0]);
            ActivateItem(female.arm_Lower_Right[0]);
            ActivateItem(female.arm_Lower_Left[0]);
            ActivateItem(female.hand_Right[0]);
            ActivateItem(female.hand_Left[0]);
            ActivateItem(female.hips[0]);
            ActivateItem(female.leg_Right[0]);
            ActivateItem(female.leg_Left[0]);
        }
        else                                                                                // Establecer personaje masculino predeterminado.
        {
            ActivateItem(male.head[0]);
            ActivateItem(male.eyebrow[0]);
            ActivateItem(male.facialHair[0]);
            ActivateItem(male.torso[0]);
            ActivateItem(male.arm_Upper_Right[0]);
            ActivateItem(male.arm_Upper_Left[0]);
            ActivateItem(male.arm_Lower_Right[0]);
            ActivateItem(male.arm_Lower_Left[0]);
            ActivateItem(male.hand_Right[0]);
            ActivateItem(male.hand_Left[0]);
            ActivateItem(male.hips[0]);
            ActivateItem(male.leg_Right[0]);
            ActivateItem(male.leg_Left[0]);
        }
        
        ActivateItem(allGender.headCoverings[0]);                                           // Establecer personaje universal predeterminado.
        ActivateItem(allGender.all_Hair[0]);
        ActivateItem(allGender.all_Head_Attachment[0]);
        ActivateItem(allGender.back_Attachment[0]);
        ActivateItem(allGender.shoulder_Attachment_Right[0]);
        ActivateItem(allGender.shoulder_Attachment_Left[0]);
        ActivateItem(allGender.elbow_Attachment_Right[0]);
        ActivateItem(allGender.elbow_Attachment_Left[0]);
        ActivateItem(allGender.hips_Attachment[0]);
        ActivateItem(allGender.knee_Attachement_Right[0]);
        ActivateItem(allGender.knee_Attachement_Left[0]);
        ActivateItem(allGender.weapons_R[0]);
        ActivateItem(allGender.weapons_L[0]);
    }

    void Update()
    {
        playerLevel = PlayerExperience.level;

        if (playerLevel > 10)
            playerLevel = 10;

        if (skinLevel != playerLevel - 1 && skinLevel < 9)
        {
            skinLevel = playerLevel - 1;
            LevelUp(skinLevel);
        }
    }

    public void LevelUp(int level)
    {
        if (enabledObjects.Count != 0)                                                      // Deshabilitar cualquier objeto habilitado antes de borrar.
        {
            foreach (GameObject g in enabledObjects)
            {
                g.SetActive(false);
            }
        }

        enabledObjects.Clear();                                                             // Borrar lista de objetos habilitados.

        if (PlayerStats.gender == "Female")                                                 // Establecer personaje femenino.
        {
            ActivateItem(female.head[level]);
            ActivateItem(female.eyebrow[level]);
            ActivateItem(female.facialHair[level]);
            ActivateItem(female.torso[level]);
            ActivateItem(female.arm_Upper_Right[level]);
            ActivateItem(female.arm_Upper_Left[level]);
            ActivateItem(female.arm_Lower_Right[level]);
            ActivateItem(female.arm_Lower_Left[level]);
            ActivateItem(female.hand_Right[level]);
            ActivateItem(female.hand_Left[level]);
            ActivateItem(female.hips[level]);
            ActivateItem(female.leg_Right[level]);
            ActivateItem(female.leg_Left[level]);
        }
        else                                                                                // Establecer personaje masculino.
        {
            ActivateItem(male.head[level]);
            ActivateItem(male.eyebrow[level]);
            ActivateItem(male.facialHair[level]);
            ActivateItem(male.torso[level]);
            ActivateItem(male.arm_Upper_Right[level]);
            ActivateItem(male.arm_Upper_Left[level]);
            ActivateItem(male.arm_Lower_Right[level]);
            ActivateItem(male.arm_Lower_Left[level]);
            ActivateItem(male.hand_Right[level]);
            ActivateItem(male.hand_Left[level]);
            ActivateItem(male.hips[level]);
            ActivateItem(male.leg_Right[level]);
            ActivateItem(male.leg_Left[level]);
        }
        
        ActivateItem(allGender.headCoverings[level]);                                       // Establecer personaje universal.
        ActivateItem(allGender.all_Hair[level]);
        ActivateItem(allGender.all_Head_Attachment[level]);
        ActivateItem(allGender.back_Attachment[level]);
        ActivateItem(allGender.shoulder_Attachment_Right[level]);
        ActivateItem(allGender.shoulder_Attachment_Left[level]);
        ActivateItem(allGender.elbow_Attachment_Right[level]);
        ActivateItem(allGender.elbow_Attachment_Left[level]);
        ActivateItem(allGender.hips_Attachment[level-1]);
        ActivateItem(allGender.hips_Attachment[level]);
        ActivateItem(allGender.knee_Attachement_Right[level]);
        ActivateItem(allGender.knee_Attachement_Left[level]);
        ActivateItem(allGender.weapons_R[level]);
        ActivateItem(allGender.weapons_L[level]);
    }

    void ActivateItem(GameObject go)                                                        // Habilitar el objeto del juego y agregarlo a la lista de objetos habilitados.
    {
        go.SetActive(true);                                                                 // Habilitar elemento.

        enabledObjects.Add(go);                                                             // Agregar elemento a la lista de elementos habilitados.
    }

    void BuildLists()                                                                       // Construir todas las listas de artículos.
    {
        BuildList(male.head, "Male_00_Head");                                               // Construir listas masculinas.
        BuildList(male.eyebrow, "Male_01_Eyebrows");
        BuildList(male.facialHair, "Male_02_FacialHair");
        BuildList(male.torso, "Male_03_Torso");
        BuildList(male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
        BuildList(male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
        BuildList(male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
        BuildList(male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
        BuildList(male.hand_Right, "Male_08_Hand_Right");
        BuildList(male.hand_Left, "Male_09_Hand_Left");
        BuildList(male.hips, "Male_10_Hips");
        BuildList(male.leg_Right, "Male_11_Leg_Right");
        BuildList(male.leg_Left, "Male_12_Leg_Left");

        BuildList(female.head, "Female_00_Head");                                           // Construir listas femeninas.
        BuildList(female.eyebrow, "Female_01_Eyebrows");
        BuildList(female.facialHair, "Female_02_FacialHair");
        BuildList(female.torso, "Female_03_Torso");
        BuildList(female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
        BuildList(female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
        BuildList(female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
        BuildList(female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
        BuildList(female.hand_Right, "Female_08_Hand_Right");
        BuildList(female.hand_Left, "Female_09_Hand_Left");
        BuildList(female.hips, "Female_10_Hips");
        BuildList(female.leg_Right, "Female_11_Leg_Right");
        BuildList(female.leg_Left, "Female_12_Leg_Left");

        BuildList(allGender.headCoverings, "All_00_HeadCoverings");                         // Construir todas las listas de género.
        BuildList(allGender.all_Hair, "All_01_Hair");
        BuildList(allGender.all_Head_Attachment, "All_02_Head_Attachment");
        BuildList(allGender.back_Attachment, "All_03_Back_Attachment");
        BuildList(allGender.shoulder_Attachment_Right, "All_04_Shoulder_Attachment_Right"); 
        BuildList(allGender.shoulder_Attachment_Left, "All_05_Shoulder_Attachment_Left");
        BuildList(allGender.elbow_Attachment_Right, "All_06_Elbow_Attachment_Right");
        BuildList(allGender.elbow_Attachment_Left, "All_07_Elbow_Attachment_Left");
        BuildList(allGender.hips_Attachment, "All_08_Hips_Attachment");
        BuildList(allGender.knee_Attachement_Right, "All_09_Knee_Attachement_Right");
        BuildList(allGender.knee_Attachement_Left, "All_10_Knee_Attachement_Left");
        BuildList(allGender.weapons_R, "Weapons_R");
        BuildList(allGender.weapons_L, "Weapons_L");
    }

    void BuildList(List<GameObject> targetList, string characterPart)                       // Llamado desde el método BuildLists.
    {
        Transform[] rootTransform = gameObject.GetComponentsInChildren<Transform>();

        Transform targetRoot = null;                                                        // Declarar transformación raíz objetivo.

        foreach (Transform t in rootTransform)                                              // Encontrar partes del personaje objeto principal en la escena.
        {
            if (t.gameObject.name == characterPart)
            {
                targetRoot = t;
                break;
            }
        }

        targetList.Clear();                                                                 // Borra la lista específica de todos los objetos.

        for (int i = 0; i < targetRoot.childCount; i++)                                     // Recorrer todos los objetos secundarios del objeto primario.
        {
            GameObject go = targetRoot.GetChild(i).gameObject;                              // Obtener el índice de gameobject del hijo i.

            go.SetActive(false);                                                            // Deshabilitar objeto hijo.

            targetList.Add(go);                                                             // Agregar objeto a la lista de objetos objetivo.
        }
    }
}

[Serializable]
public class CharacterObjectGroups                                                          // Clase para mantener las listas organizadas, permite el cambio simple de objetos masculinos/femeninos.
{
    public List<GameObject> head;
    public List<GameObject> eyebrow;
    public List<GameObject> facialHair;
    public List<GameObject> torso;
    public List<GameObject> arm_Upper_Right;
    public List<GameObject> arm_Upper_Left;
    public List<GameObject> arm_Lower_Right;
    public List<GameObject> arm_Lower_Left;
    public List<GameObject> hand_Right;
    public List<GameObject> hand_Left;
    public List<GameObject> hips;
    public List<GameObject> leg_Right;
    public List<GameObject> leg_Left;
}

[Serializable]
public class CharacterObjectListsAllGender                                                  // Clase para mantener las listas organizadas, permite la organización de todos los elementos de género.
{
    public List<GameObject> headCoverings;
    public List<GameObject> all_Hair;
    public List<GameObject> all_Head_Attachment;
    public List<GameObject> back_Attachment;
    public List<GameObject> shoulder_Attachment_Right;
    public List<GameObject> shoulder_Attachment_Left;
    public List<GameObject> elbow_Attachment_Right;
    public List<GameObject> elbow_Attachment_Left;
    public List<GameObject> hips_Attachment;
    public List<GameObject> knee_Attachement_Right;
    public List<GameObject> knee_Attachement_Left;
    public List<GameObject> weapons_R;
    public List<GameObject> weapons_L;
}