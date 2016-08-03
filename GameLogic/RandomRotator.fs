namespace SpaceShooter
open UnityEngine

type RandomRotator() = 
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable tumble = 0.0f

    //Creates rotations for the asteroids to look realistic 
    member x.Start() =
        x.GetComponent<Rigidbody>().angularVelocity <- Random.insideUnitSphere * tumble
