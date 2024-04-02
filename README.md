# BlockBreaker

Simple game to destroy all the blocks.   

Instructions:
Open the BlockBreaker scene
Press Play
Use mouse to aim the turret
Spacebar to shoot projectiles

Game over when all blocks destroyed.

For simplicity I chose to create a singleton GameManager class to control almost everything.  

GameManager
	Update() - moves turret and checks for firing
	
	On firing projectile is loaded from the pool if available otherwise a new one is created.
	
Projectile
	Controls the lifespan of the projectile and handles going off the bottom of the screen.
	