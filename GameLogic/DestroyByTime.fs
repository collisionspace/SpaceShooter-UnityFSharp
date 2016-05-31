namespace SpaceShooter
open UnityEngine

type DestroyByTime() = 
    inherit MonoBehaviour()

    [<SerializeField>]
    let lifetime = Unchecked.defaultof<float32>

    //Destroys object after so much time
    member x.Start() =
        Object.Destroy(x.gameObject, lifetime)