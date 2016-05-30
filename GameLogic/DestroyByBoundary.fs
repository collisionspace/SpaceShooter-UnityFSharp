namespace SpaceShooter
open UnityEngine

type DestroyByBoundary() = 
    inherit MonoBehaviour()

    member x.OnTriggerExit (other:Collider) =
        Object.Destroy(other.gameObject)