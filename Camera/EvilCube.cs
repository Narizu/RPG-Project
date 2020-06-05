using UnityEngine;

public class EvilCube : MonoBehaviour
{
    Transform target;
    Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = targetCamPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Floor" && other.tag != "Enemy")
            other.GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Floor" && other.tag != "Enemy")
            other.GetComponent<MeshRenderer>().enabled = true;
    }
}