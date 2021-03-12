using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimation : MonoBehaviour
{
    LineRenderer laser;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, gameObject.transform.position + offset);
        //// TODO REMOVE THIS DOODOO
        ///        //// TODO REMOVE THIS DOODOO
        ///                //// TODO REMOVE THIS DOODOO
        ///                        //// TODO REMOVE THIS DOODOO
        ///                                //// TODO REMOVE THIS DOODOO
        ///                                        //// TODO REMOVE THIS DOODOO
        ///                                                //// TODO REMOVE THIS DOODOO
        ///                                                        //// TODO REMOVE THIS DOODOO
        ///                                                                //// TODO REMOVE THIS DOODOO
        ///                                                                        //// TODO REMOVE THIS DOODOO
        ///                                                                        
        ///                                                
        ///                                                        //// TODO REMOVE THIS DOODOO
        ///                                                        //// TODO REMOVE THIS DOODOO
        ///                                                v
        ///                                                        //// TODO REMOVE THIS DOODOO
        ///                                                        //// TODO REMOVE THIS DOODOO
        ///                                        //// TODO REMOVE THIS DOODOO
        ///                        
        ///                                //// TODO REMOVE THIS DOODOO
        ///                                //// TODO REMOVE THIS DOODOO
        ///                                //// TODO REMOVE THIS DOODOO
        ///                                //// TODO REMOVE THIS DOODOO
        ///                        //// TODO REMOVE THIS DOODOO
        ///        //// TODO REMOVE THIS DOODOO
    }

    // Update is called once per frame
    void Update()
    {

    }
}

