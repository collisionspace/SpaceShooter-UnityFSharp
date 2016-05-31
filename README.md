# SpaceShooter-UnityFSharp

I attempted to do this set of instructions on a Mac but Unity/Xamarin Studio/MonoDevelop didn’t like this so I followed these instructions on Windows 10 and it worked exactly as described below.

The following setup instructions was taken from: https://github.com/Thorium/Roll-a-ball-FSharp <br/>
Some of the instruction code will be for the Roll A Ball tutorial from Unity's website but there isn't much difference. <br/><br/>
This repo is for the Space Shooter tutorial, you'll find the tutorial here:<br/> https://unity3d.com/learn/tutorials/projects/space-shooter-tutorial
### How to make a similar solution from scratch

 * Install some version of Unity and a Visual Studio (or MonoDevelop Add-in F# language binding)
 * Create a new Unity project as usual
   (Instead of desktop I recommend to put the project something like c:\git\Roll-a-ball )
   [http://unity3d.com/learn/tutorials/projects/roll-a-ball/set-up] (http://unity3d.com/learn/tutorials/projects/roll-a-ball/set-up)
   But before proceeding to the second video...
 * With Visual Studio (or MonoDevelop or your favourite editor...) create a new F#-library under your Unity's project path. Un-tick the "Create directory for solution" to save one directory. (I used c:\git\Roll-a-ball and created project called GameLogic.) 
 * Add references to UnityEngine.dll (I did have it in C:\Program Files (x86)\Unity\Editor\Data\Managed\ )
 * From project properties, set the build "Output path" to some folder under Unity's Assets-folder, for example: ..\Assets\bin\
 * It seems that Unity 4.x still uses .NET 3.5 (or 2.0) so it is better to change the target framework to .NET 3.5. (I don't know what is the MonoBleedingEdge-folder under Unity, maybe they will update soon...)
 * If you want to be sure to use same components as Unity, replace also mscorlib.dll, System.dll, System.Core.dll from the ones that are found under Unity's folder structure (I have those in C:\Program Files (x86)\Unity\Editor\Data\Mono\lib\mono\2.0\ )
 * For FSharp.Core, the copy-local should be "true" as Unity doesn't have it. These other core-level dll's should have that "false".
 * Write some script, e.g. like this:

```

namespace RollABall
open UnityEngine

type PlayerController() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable speed = 6.0f

    member x.FixedUpdate () =
        let ``move horizontal`` = Input.GetAxis("Horizontal");
        let ``move vertical`` = Input.GetAxis("Vertical")

        let movement = Vector3(``move horizontal``, 0.0f, ``move vertical``)
        movement * speed * Time.deltaTime
        |> x.rigidbody.AddForce

```

 * Now, build in Visual Studio, then switch to Unity and open bin-folder from Assets. From Project-window, you should see your fsharp-dll and a little arrow on its right side to extend the details. When you press the arrow, you see the class(es) inside the component and you can directly drag and drop your class to your game object (like the ball in this tutorial). 
![alt tag](http://i.imgur.com/0sndBPj.png)
 * Then just continue the tutorial, but instead of creating every snippet a C#-file, just code F#, build the solution (in VS) and Unity notices the new modifications on the fly.

###F# Unity Notes

*	type ClassName() =
	inherit MonoBehaviour()

	* From what I noticed this is how all class names should start off as and should always inherit MonoBehaviour

*	[\<SerializeField>]<br/>
	let mutable speed = 6.0f

	* If you want to change a field via the GUI instead of having to rebuild the solution each time then put [\<SerializeField>] above that said variable<br/>
		![alt tag](http://i.imgur.com/X6Y1MPv.png)
