namespace SpaceShooter
open UnityEngine

type DestroyByTime() = 
    inherit MonoBehaviour()

    [<SerializeField>]
    let lifetime = Unchecked.defaultof<float32>
    member x.Start() =
        Object.Destroy(x.gameObject, lifetime)