using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisownSelfOnStart : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(null);
    }
}
