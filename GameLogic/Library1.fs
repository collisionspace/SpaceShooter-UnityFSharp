namespace SpaceShooter

open UnityEngine;
open UnityEngine.UI;
open UnityEngine.EventSystems;
open System.Collections;

[<System.Serializable>]
type Boundary() =
    [<SerializeField>]
    let mutable xMin = -6.0f
    [<SerializeField>]
    let mutable xMax = 6.0f
    [<SerializeField>]
    let mutable zMin = -4.0f
    [<SerializeField>]
    let mutable zMax = 8.0f

    member x.getXMin():float32 =
        xMin
    member x.getXMax():float32 =
        xMax
    member x.getZMin():float32 =
        zMin
    member x.getZMax():float32 =
        zMax

type PlayerController() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable speed = 6.0f
    [<SerializeField>]
    let mutable tilt = 0.0f
   (* [<SerializeField>]
    let mutable xMin = 0.0f
    [<SerializeField>]
    let mutable xMax = 0.0f
    [<SerializeField>]
    let mutable zMin = 0.0f
    [<SerializeField>]
    let mutable zMax = 0.0f*)

    [<SerializeField>]
    let mutable shot = Unchecked.defaultof<GameObject>    
    [<SerializeField>]
    let mutable shotSpawn = Unchecked.defaultof<Transform>
    [<SerializeField>]
    let mutable fireRate = 0.25f
    [<SerializeField>]
    let mutable touchPad = Unchecked.defaultof<SimpleTouchPad>
    [<SerializeField>]
    let mutable areaButton = Unchecked.defaultof<SimpleTouchAreaButton>

    let mutable nextFire = 0.0f
    let mutable clone = Unchecked.defaultof<Object>
    let mutable calibrationQuaternion = Unchecked.defaultof<Quaternion>
    
    member x.Start() =
        x.CalibrateAccelerometer()

    (*This member is called every cycle. Tried to use pattern match here instead of if statement but kept 
    getting an error. This adds a wait time to firing, also creates a new shot object, and plays a audio clip*)
    member x.Update() =
        if Input.GetButton("Fire1") && Time.time > nextFire then
            nextFire <- Time.time + fireRate
            clone <- Object.Instantiate(shot,shotSpawn.position,shotSpawn.rotation)
            x.GetComponent<AudioSource>().Play()

        //Used to calibrate the Iput.acceleration input
    member x.CalibrateAccelerometer() =
        let accelerationSnapshot = Input.acceleration;
        let rotateQuaternion = Quaternion.FromToRotation(Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion <- Quaternion.Inverse(rotateQuaternion)
    
    //Get the 'calibrated' value from the Input
    member x.FixAcceleration (acceleration:Vector3):Vector3 =
        let fixedAcceleration = calibrationQuaternion * acceleration;
        fixedAcceleration

    member x.FixedUpdate () =
        //grabs input for vertical and horizontal
       // let ``move horizontal`` = Input.GetAxis("Horizontal");
        //let ``move vertical`` = Input.GetAxis("Vertical")

        //creates movement based on the input from the user
        //let movement = Vector3(``move horizontal``, 0.0f, ``move vertical``)
        let accelerationRaw = Input.acceleration
        let acceleration = x.FixAcceleration(accelerationRaw)
        let movement = Vector3(acceleration.x, 0.0f, acceleration.y)
        //let direction  = touchPad.GetDirection()
        //let movement = Vector3(direction.x, 0.0f, direction.y)
        //serts the velocity on the said object
        x.GetComponent<Rigidbody>().velocity <- movement * speed
        
        //changes the position within the given boundary
        x.GetComponent<Rigidbody>().position <- Vector3(
            Mathf.Clamp(x.GetComponent<Rigidbody>().position.x, Boundary().getXMin(),Boundary().getXMax()),  //x
            0.0f,   //y
            Mathf.Clamp( x.GetComponent<Rigidbody>().position.z,Boundary().getZMin(),Boundary().getZMax()))  //z
        
        //tilts the ships to the left or to the right
        x.GetComponent<Rigidbody>().rotation <- Quaternion.Euler(0.0f,0.0f,x.GetComponent<Rigidbody>().velocity.x * -tilt)