using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamFollow : MonoBehaviour
{
    [SerializeField] private GameObject findPlayer;

    Vector3 offset = new Vector3(0, 6, -5.5f);
    // Start is called before the first frame update
   
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = findPlayer.transform.position + offset;
    }
}
