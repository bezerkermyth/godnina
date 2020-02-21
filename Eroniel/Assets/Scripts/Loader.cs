using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject playerData;



    void Awake()
    {
        
        if (Serializer.SerializerInstance == null)

            
            Instantiate(playerData);

    }

}