using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagCamera : MonoBehaviour
{
    [SerializeField] GameObject ModelPlayer;

    private Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - ModelPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ModelPlayer.transform.position + Offset;
    }
}
