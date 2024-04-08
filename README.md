# BlockBreaker

Simple game to destroy all the blocks.   Created using Unity 2022.3.17f1.

Instructions:
Open the BlockBreaker scene
Press Play
Use mouse to aim the turret
Spacebar to shoot projectiles

Game over when all blocks destroyed.

GameManager singleton defines the actions that objects can register to.  An object can call one of the public 
functions to invoke the action and notify whoever is registered.  Other options would be to have a single Event function
and pass a string or enum with the event type.  You could also use interfaces and injection to link the objects
together and still keep implementation separate.


GameManager.cs
	Start() - Invokes OnGameStart()
	
Turret.cs
	Controls the movement of the turret and the firing.  Pools the projectiles.
	
Projectile.cs
	Sets the velocity, controls the lifespan and handles going off the bottom of the screen.  Calls
	Turret.RemoveProjectile to destroy the projectile.	
	
Wall.cs
	Positions the wall colliders based on the size of the screen.
	
LayoutManager.cs
	Registers to OnGameStart to create the layout.  Pools the blocks.  Calls BlockDestroyed() when a block
	is destroyed and GameOver() when all blocks destroyed
	
Block.cs
	Sets the color and number of hits.  Handles collision with the projectile.  Calls LayoutManager to
	remove the blocks
	
UI.cs
	Displays the GameOver text, restart button, and the console.  Registers to events to display the messages.
	
Potential Improvements
	Sounds!
	Special effects when a brick is destroyed
	Special ball type that goes through bricks
	Create/edit layouts with an editor tool and then play through those levels (level editor)
	Move settings to a ScriptableObject