namespace SpaceShooter
open UnityEngine

type DestroyByContact() = 
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable explosion = Unchecked.defaultof<Object>
    [<SerializeField>]
    let mutable playerExplosion = Unchecked.defaultof<Object>
    [<SerializeField>]
    let mutable scoreValue = Unchecked.defaultof<int>

    let mutable gameController = Unchecked.defaultof<GameController>

    member x.Start() =
        let gameControllerObject = GameObject.FindWithTag("GameController")
        if gameControllerObject <> null then
            gameController <- gameControllerObject.GetComponent<GameController>()
        else 
            Debug.Log("Can't find GameController Script")

    (*Uses pattern matching to find objects that have been given tags*)
    member x.OnTriggerEnter (other:Collider) =
       if other.CompareTag ("Boundary") || other.CompareTag ("Enemy") then
            Debug.Log("Boundary or Enemy")
        else
            if explosion <> null then
                explosion <- Object.Instantiate(explosion, x.transform.position, x.transform.rotation);

            if other.CompareTag("Player") then
                Object.Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver()

            gameController.AddScore (scoreValue);
            Object.Destroy(other.gameObject);
            Object.Destroy(x.gameObject);
            (*
        match other.tag with
        | "Boundary" -> ()   //regular explosion is always called when asteroid collides
        | "Enemy" -> ()
        | "Player" -> (((playerExplosion <- Object.Instantiate(playerExplosion,other.transform.position,other.transform.rotation))
                        gameController.GameOver()))
        | _ -> ((if explosion <> null then
                    (explosion <- Object.Instantiate(explosion,x.transform.position,x.transform.rotation)))
                match other.tag with
                | "Player" -> (((Object.Instantiate(playerExplosion,other.transform.position,other.transform.rotation))
                                gameController.GameOver()))
                | _ ->()
                gameController.AddScore(scoreValue)
                Object.Destroy(other.gameObject) //these 2 lines always called
                Object.Destroy(x.gameObject)
                )*)
        (*match other.tag with //player explosion is only called when player object collides with asteroid
        | "Player" -> ((playerExplosion <- Object.Instantiate(playerExplosion,other.transform.position,other.transform.rotation))
                        gameController.GameOver())
        | _ -> ()*)
       (* gameController.AddScore(scoreValue)
        Object.Destroy(other.gameObject) //these 2 lines always called
        Object.Destroy(x.gameObject)*)