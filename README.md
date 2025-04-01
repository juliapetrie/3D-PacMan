# Pacman³ https://juliapetrie.itch.io/pacman

### Updated Retro Game Jam Document
Game Title: Pacman³
Team Name: Polygon Pioneers
Team Members:
Julia Petrie 29617347
Stefan Spataro 56862295
Ivona Nicetin 87205910
Daniel Storozhuk 74456278 

#### Core Concept
- We are taking inspiration from the game Pacman. We plan to create a system with a main character and ghosts released from a box throughout each level. 
- Our re-imagined game follows Pacman from above with a dynamic camera along the Z-axis. There will be 3D features such as launch pads (another twist function), which can transport the Pacman from A to B, increasing efficiency and adding a potential escape path from ghosts. 
- We have changed the experience of the game by making it a level-based objective game. We are changing the fruit power-ups to add speed to Pacman (provide sprint power-ups). Pacman has three lives, and if lives are lost in level 2 or 3, the player must start back at the beginning of level 1. 
- Our game offers Pacman the traditional power-pellet powerups indicated by a Pacman flash animation portraying a sense of immunity and speed.
- On collision with ghosts, Pacman will be transported back to the homing position, and “Pacman flash” will be shown on landing to show an update on the immunity state. 
Ghosts will fundamentally behave the same, with ghost attack or ghost block tendencies. In collision with Pacman (when Pacman is on power pellet powerup), ghosts will enter a frightened state and return to their home position. 

#### Core Gameplay
Game Loop:
- The player-emulated Pacman is running through a map to collect green pellets (labelled fruit - formerly Pac-dots) and powerups while avoiding ghosts. 
- Players interact with the main menu welcome screen first, when they are ready, they click Start Game; upon entering the first level, they are met with a tutorial screen; after familiarizing themselves with the task, they can click “Start.” Here, they will be counted down from three to the next level. At any time, the player can choose to pause or resume the game or exit the game by going back to the main menu. 
- The game will rely on sound effects such as a consume pellet sound as Pacman collects items. Along with visual feedback of the pellets disappearing. The visual feedback will also occur when ghosts are attacked, as they will enter a frightened state and retreat toward their home base. 

#### Player Controls
Keyboard (or Controller) Inputs - Move: WASD, 
- Special Ability: eat fruit and have an immediate sprint powerup; eat pink power pellets and become invincible to ghosts; use launch pads to leap to other areas on the map. 

#### Level & Progression
Game Structure: 
- The game has three levels, which will be hand-designed into different scenes, creating a seamless user experience.
- To complete the game, the player must successfully finish all three levels by collecting all the pellets and avoiding the ghosts. 
Progression System: 
- The difficulty will increase as the ghosts speed up. The map changes, making it more challenging to collect the pellets. Occasional power-ups will be offered when the player runs through pink power pellets; upon doing so, Pacman is invincible to ghosts and has a sprint power-up. If Pac-man runs through the green fruit powerups, he becomes faster for a limited amount of time. The launch pads in level 1 are placed strategically to benefit players; they are less helpful in later levels (you don’t travel as far, or you don’t land near a power-up).
- In level 1, there are five sprint powerups and five power pellets (invincible)
- In level 2, there are three sprint powerups and four power pellet
- In level 3, there are three sprint powerups and three power pellet
- The map gets progressively trickier to navigate as the player levels up. 

#### Scoring & Win/Loss Conditions
Winning:
- In each level, eat all of the pac-dots until you beat the last level.
Losing:
- Pacman has 3 lives per level. Lives are lost when Pacman gets caught (touches) a ghost while not powered up by a ghost-eating ability. After losing three lives in the first level, level one will restart. For lives lost in level 2 or 3, the player must start back at level 1 again. 
Score System:
- Pellets are worth points; players must collect all pellets (all points for that level) to continue; there are three levels. Completing the third level means the player has completed the game, and a success message is given. 

#### Timeline & Milestones
- ##### Week 1: Core Mechanics & Gameplay Elements
- Set up a basic Unity project <Julia>
- Implement player input and core mechanics (movement, abilities) <Stefan>
- Pacman dies(loses a life) when touched by a ghost
- Sprint ability (While Pacman is fruit powered-up)
- Implement ghost mechanics <Ivona>
- Ghost dies when touched by powered-up Pacman,
- They get progressively faster each time they die and are released
- Each ghost has its own properties (speed, difficulty)
- Including navmesh setup
- Create a sample level - MAP to test mechanics <Julia>
- Create UI elements
- The cinematic camera setup follows Pac-Man movement and is informed by the direction indicator. Camera will be positioned just above the wall so the player can see parts of the map.  <Daniel, Julia>
- Launchpad. Character movement disabled midair.<Stefan>

- ##### Week 2: Polish Mechanics & Troubleshoot Bugs & Finalize
- Create two more map levels, each with greater complexity <Julia, Stefan>
- Create a UI and menu’s <Julia>
- Create advanced ghost implementations building off of the prior week, including homing effect for ghosts and Pacman on collision <Ivona>
- Continued development of Launchpads and addition of teleportation <Stefan>
- Audio script <Daniel>
- Add effects (Pacman flash effect), sound effects/audio setup, and other polish (JUICE) <Julia>
- Implement a game manager object that oversees score, levels, game completion, and failure. <Julia>
- Tweak player and ghost properties to have the best user experience. <Team>
- Package the final build <Julia>

#### Assets
Sound & Music:
- Overall soundtrack: https://freesound.org/s/521478/
- Eating fruit: https://freesound.org/s/483505/
- Power up: https://freesound.org/s/787791/
- Chomp: https://freesound.org/s/353067/
- Try again: https://freesound.org/s/417795/
- Level up: https://freesound.org/s/337049/
UI:
- Pacman title and Lives font: https://www.dafont.com/crackman.font (CC License) 
- Loading scene, level up, end game font (CC) license) Arcade by Yuji Adachi https://www.dafont.com/arcade-ya.font
- Our team will create all additional UI icons using Adobe Illustrator.

## Commits & Highlighted Features Per Team Member
- Julia:
  - Pacman flashing effect to indicate state change of invincibility/health. When Pacman eats a power pellet, he flashes to show speed and invincibility in navigating the map. When Pacman collides with a ghost, he flashes during a 1.5f cooldown period, which provides brief protection from ghosts, preventing double deaths; the flashing also indicates a change in health.
    - https://github.com/juliapetrie/3D-PacMan/commit/2476ef85580c32be7a56beccc6ce5454d509a7d7

https://github.com/user-attachments/assets/10b58297-0d4c-4846-9be2-83cc44f6d78e


  - UI: I've created all game ui including level updates, score updates, lives left, exit, pause, play tutorial menu, countdown menu (count down controller used to pause gameplay and wait for tutorial screen to complete, logic also used for exit and pause buttons), as well as game over, game complete and next level messages to transition between scenes
    - https://github.com/juliapetrie/3D-PacMan/commit/02b4f88b0cecdaef0c6ae80830f6c8cb87da37f7
    - https://github.com/juliapetrie/3D-PacMan/commit/9af23a68f95ac2dc766952d38d26525c8723189f
  - created game manager script to bring Pacman to the next or previous levels on conditions met; also handles ui pop-ups and audio calls.
    - https://github.com/juliapetrie/3D-PacMan/commit/dc2613507c46857fcc9bbe454102a79aaf879896  

https://github.com/user-attachments/assets/c2a25e2a-c3b5-46d7-8d72-0a9ad56b1dfc






- Ivona: 
- Stefan:      
  - Player mechanics. Pacman moves rigidly with a buffered movement direction. The player can make their movement choice infinitely before getting to their turn. Utilizes raycasts from the edges of the pacman object to detect walls and determine if the player can move in the desired direction. https://github.com/juliapetrie/3D-PacMan/pull/2 - player mechanics, fruit powerup
    
  - Launchpad. Pacman can hit the launch platform and is thrown into a pre-calculated trajectory. This gives the player another escape route and therefore decreases the difficulty of the game. Attempting to change multiple parameters on the launch was tricky, so I left only the launch height which allowed me to somewhat control speed. https://github.com/juliapetrie/3D-PacMan/pull/6 - launchpad and power pellets
  - teleportation passages. Pacman can use the left and right sides of the maps to escape from the ghosts, however ghosts cannot due to the complexity of the Navmesh implementation (unlike original pacman). https://github.com/juliapetrie/3D-PacMan/pull/39 - Passages (teleport walls)
- Daniel: 

