# Pi3D Example Mini-project

## Overview of the Game
The idea of the project is a collection arena game in the vein of PACMAN or Snake. The player guides a rat in a closed off arena freely in a 2D plane using keyboard controls, while being able to rotate and zoom the camera around to see the field. The goal of the game is to maximize the score by gathering food, while escaping from cats trying to catch the player. The game becomes progressively harder with time by increasing the number of cats and their speed. Genre of the game is a reflex-based arcade game.

The main parts of the game are:
-	Player – rat, moved with the keyboard WASD or arrow keys
-	Camera – pivoting around the center of the playfield and rotated around with the mouse. Zooming is done with the mouse scroll
-	Food – cheese objects are spawned on the field – one in the beginning and then another one each time a player gathers the previous one. Each cheese gives 1 point to the player.
-	Enemies – cats, they are spawn in random places at the edge of the play field and moved towards the position of the player at the time of their spawning. Take 1 live from the player on collision and are destroyed if they touch the edges of the play field
-	Playfield – close off space where the player can freely move. They player cannot go out of the field.
-	Lives – the player starts with 3 lives, once all lives are removed the game ends

Game features:
- Positions of food and enemies are randomly selected each time helping with replayability.
- The difficulty of the game changes with time, making it harder
- The game keeps track of a score

## Project Parts

### Scripts
- CameraMoving – used for rotation and zooming of the camera
- ChangeScore – used for updating the UI
- EatFood – used to keep track of collisions with the food and updating score
- EnemyBehaviour – used for enemy movement and tracking enemy collisions with the player and the world
- MoveCharacter – used for moving the character using rigidbody physics and rotate the movement based on camera position
- ObjectSpawner – used for spawning enemies, keeping tracking of a timer and changing the difficulty of the game
- ScoreKeeper – keeps reference to the player lives, the score and if the game ending is triggered.

### Models & Prefabs
- A model of the cheese downloaded from [sketchfab](https://sketchfab.com/3d-models/cheese-78642517ca7e43b495e73509810fbbe1)
- Rat and cat models made with Unity primitives

| **    Task   **                                                                | **    Time it Took (in hours)   ** |
|--------------------------------------------------------------------------------|------------------------------------|
|     Setting up   Unity, making a project in GitHub                             |     0.5                            |
|     Research and   conceptualization of game idea                              |     1                              |
|     Searching for   3D models - cheese                                         |     0.5                            |
|     Building 3D   models from scratch -cat, rat, field                         |     1                              |
|     Making camera   movement controls and initial testing                      |     1                              |
|     Player   movement                                                          |     0.5                            |
|     Combining   player movement with camera orientation, bugfixing             |     1.5                            |
|     Building the   random spawning of cheeses and fixing spawning bugs         |     1                              |
|     Building   enemy random spawners, randomizing starting positions           |     2                              |
|     Making timers   and connecting enemy spawning and game difficulty          |     1.5                            |
|     Making UI   elements and research into TextMesh Pro                        |     1.5                            |
|     Collisions   and bugfixing error with multiple collision all at once       |     0.5                            |
|     Playtesting   and bugfixing fringe cases in rigidbody incorrect physics    |     1.5                            |
|     Code   documentation                                                       |     1                              |
|     Making readme                                                              |     0.5                            |
|     All                                                                        |     15.5                           |

## References
•	[How to make RTS Camera Movement in Unity](https://www.youtube.com/watch?v=cfjLQrMGEb4&t=1s&ab_channel=Brackeys)
•	[Game Architecture Tips – Unity Timer](https://www.youtube.com/watch?v=pRjTM3pzqDw&ab_channel=DapperDino)
•	[Spawning objects in only a certain area](https://forum.unity.com/threads/spawning-objects-in-only-a-certain-area.611167/) 
•	[Moving character relative to camera](https://forum.unity.com/threads/moving-character-relative-to-camera.383086/)





