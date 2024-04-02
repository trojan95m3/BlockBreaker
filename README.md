# BlockBreaker

Simple game to destroy all the blocks.   Created using Unity 2022.3.17f1.

Instructions:
Open the BlockBreaker scene
Press Play
Use mouse to aim the turret
Spacebar to shoot projectiles

Game over when all blocks destroyed.

For simplicity I chose to create a singleton GameManager class to control creating the layout, the turret,
and game over logic.  

GameManager.cs
	CreateLayout() - based on the parameters determine how many block will fit and then create them. Pull a block from the 
	pool if available, otherwise create a new one. 
	
	Update() - moves turret and checks for shooting
	
	On firing a projectile is loaded from the pool if available otherwise a new one is created.
	
Projectile.cs
	Sets the velocity, controls the lifespan and handles going off the bottom of the screen.
	
Wall.cs
	Positions the wall colliders based on the size of the screen.
	
Block.cs
	Sets the color and number of hits.  Handles collision with the projectile
	
UI.cs
	Displays the GameOver text, restart button, and the console
	
	
Challenges
	Remembering how to convert screen size into 2D orthographic space
	
Potential Improvements
	Sounds!
	Special effects when a brick is destroyed
	Special ball type that goes through bricks
	Create/edit layouts with an editor tool and then play through those levels (level editor)
	Move settings to a ScriptableObject