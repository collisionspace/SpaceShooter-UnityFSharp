namespace SpaceShooter
open UnityEngine

type DestroyByBoundary() = 
    inherit MonoBehaviour()

    //destroys object that exits the current boundaries
    member x.OnTriggerExit (other:Collider) =
        Object.Destroy(other.gameObject)