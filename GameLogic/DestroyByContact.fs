namespace SpaceShooter
open UnityEngine

type DestroyByContact() = 
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable explosion = Unchecked.defaultof<Object>
    [<SerializeField>]
    let mutable playerExplosion = Unchecked.defaultof<Object>

    (*Uses pattern matching to find objects that have been given tags*)
    member x.OnTriggerEnter (other:Collider) =
        match other.tag with
        | "Boundary" -> ()   //regular explosion is always called when asteroid collides
        | _ -> (explosion <- Object.Instantiate(explosion,x.transform.position,x.transform.rotation)
                match other.tag with //player explosion is only called when player object collides with asteroid
                | "Player" -> (playerExplosion <- Object.Instantiate(playerExplosion,other.transform.position,other.transform.rotation))
                | _ -> ()
                Object.Destroy(other.gameObject) //these 2 lines always called
                Object.Destroy(x.gameObject))