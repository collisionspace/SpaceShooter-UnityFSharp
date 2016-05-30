﻿namespace SpaceShooter
open UnityEngine

type Mover() = 
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable speed = 6.0f

    member x.Start() =
        x.GetComponent<Rigidbody>().velocity <- x.transform.forward * speed