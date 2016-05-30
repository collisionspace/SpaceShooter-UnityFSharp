namespace SpaceShooter

open System
open System.Collections
open System.Collections.Generic

open UnityEngine

type GameController() = 
    inherit MonoBehaviour()

    
    [<SerializeField>]
    let mutable hazard = Unchecked.defaultof<Object>
    [<SerializeField>]
    let mutable spawnValues = Unchecked.defaultof<Vector3>
    [<SerializeField>]
    let mutable hazardCount = Unchecked.defaultof<int>
    [<SerializeField>]
    let mutable spawnWait = Unchecked.defaultof<float32>
    [<SerializeField>]
    let mutable startWait = Unchecked.defaultof<float32>
    [<SerializeField>]
    let mutable waveWait = Unchecked.defaultof<float32>


    member x.SpawnWaves() =
        seq {
            yield WaitForSeconds(startWait)
            while true do
                for i = 0 to hazardCount - 1 do
                    let spawnPosition = Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z)
                    let spawnRotation = Quaternion.identity
                    hazard <- Object.Instantiate(hazard, spawnPosition, spawnRotation)
                    yield WaitForSeconds(spawnWait)
                yield WaitForSeconds(waveWait)
        } :?> IEnumerator //need to cast to IEnumerator
        
    member x.Start() =
        x.StartCoroutine(x.SpawnWaves())