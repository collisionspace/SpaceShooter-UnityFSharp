namespace SpaceShooter

open System
open System.Collections
open System.Collections.Generic

open UnityEngine
open UnityEngine.UI
open UnityEngine.SceneManagement


type GameController() = 
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable hazards = Unchecked.defaultof<Object[]>
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
    [<SerializeField>]
    let mutable scoreText = Unchecked.defaultof<Text>
    (*[<SerializeField>]
    let mutable restartText = Unchecked.defaultof<GUIText>*)
    [<SerializeField>]
    let mutable gameOverText = Unchecked.defaultof<Text>
    [<SerializeField>]
    let mutable restartButton = Unchecked.defaultof<GameObject>
    let mutable score = Unchecked.defaultof<int>
    let mutable gameOver = Unchecked.defaultof<bool>
    let mutable restart = Unchecked.defaultof<bool>

    (*seq is used so that yield WaitForSeconds can be used
     this loops until it's false. This loop randomly spawns asteroids
     within the coordinates as seen in spawnPosition*)
    member x.SpawnWaves() =
        seq {
            yield WaitForSeconds(startWait)
            while true do
                for i = 0 to hazardCount - 1 do
                    let hazard = Array.get hazards  (Random.Range(0, hazards.Length))
                    let spawnPosition = Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z)
                    let spawnRotation = Quaternion.identity
                    Object.Instantiate(hazard, spawnPosition, spawnRotation)
                    yield WaitForSeconds(spawnWait)
                yield WaitForSeconds(waveWait)
                if gameOver then
                    restartButton.SetActive(true)
                    //restartText.text <- "Press 'R' for Restart"
                    restart <- true
                    
        } :?> IEnumerator //need to cast to IEnumerator
        
    member x.Start() =
        gameOver <- false
        gameOverText.text <- ""
        restart <- false
        restartButton.SetActive(false)
        //restartText.text <- ""
        score <- 0
        x.UpdateScore()
        x.StartCoroutine(x.SpawnWaves())

    (*member x.Update() =
        if restart then
            if Input.GetKeyDown(KeyCode.R) then
                SceneManager.LoadScene(0)
*)
    member x.UpdateScore() =
        scoreText.text <- "Score: " + score.ToString()

    member x.AddScore(newScoreValue:int) =
        score <- score + newScoreValue
        x.UpdateScore()

    member x.GameOver() =
        gameOverText.text <- "Game Over!"
        gameOver <- true

    member x.RestartGame() =
        SceneManager.LoadScene(0)