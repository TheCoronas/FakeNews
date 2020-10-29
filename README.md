# Fact or Fantasy - A Fake News Identification Game

## Description
*Fact Or Fantasy* puts players into a world where they must interpret scrolls given to them by different characters and identify them as true or false in order to survive. The game contains five levels, each with a unique environment and a unique storyline. The scrolls can be identified as true or false by searching for aspects such as grammatical/spelling errors, inconsistencies in information, or trustworthiness of the source. This is important as it teaches players what to look for in real articles and social media posts online, ensuring the game will successfully educate players.

This game was designed to target primary school students, and has been designed as a desktop application.

## Development Stack
The development environment consisted of:
- Unity V 2019.4.8f1 (front-end development)
- mySQL
- phpMyAdmin
- Nginx
- CodeIgniter

## How To Run
To run the game from Unity, navigate to: `Assets>Scenes>Menus>Menu Scenes>start_screen`. From here, create an account, sign in, and start playing the game.

## Assets
Unless otherwise specified, all assets were obtained from the Unity Asset Store. The "Standard Assets" default Unity Package is located within `Assets>Standard Assets`.

### Level Assets
All assets relative to level design (for example: buildings, landscaping, props, skyboxes) are located within `Assets>Level Assets`. 

This directory contains folders that are aptly named after their respective Unity Asset Bundle, along with some custom assets developed within Blender.

### Character Assets
All assets relative to characters design and implementation are located within `Assets>Character Assets`. 

This directory contains folders that are aptly named after their respective Unity Asset Bundle, along with some custom assets developed within Blender.

## Audio
The folder `Assets>Audio` contains the mp3 of the royalty-free background music used for the game. It also contains several C# scripts to ensure that the audio continues to play on repeat. 

## Heads-Up-Display (HUD)
The HUD for the game, along with its necessary assets and scripts is located within `Assets>HUD`. As the HUD is a vital, and significant, part of the game, it was more consistent to isolate these assets. 

### HUD Sprites
`Assets>HUD>Sprites` contains the images used to overlay on the HUD.

### HUD Scripts
`Assets>HUD>Scripts` contains all scripts that are used to implement the functionality of the HUD and game. Each script is appropriately named and commented for developer accessibility.

## Resources
The `Assets>Resources` folder contains the Font data utilised throughout the game.

## Scenes
### Levels
Each individual level scene can be found within `Assets>Scenes>Levels`. This folder contains Levels 1 through 5, which are each constructed to implement the functionality of the game.

Scrolls are customised to each level, and are thus all individually set under their respective location within the HUD of each level.

**Note:** It is imperative that Levels 1 to 5 are all their respective numbers within the Build Settings. i.e. Level 1 is scene 1, Level 2 is scene 2 etc. This is vital to the implementation of level navigation, and the game will not function as expected if the Build Settings are altered.

### Menus
The starting menus (signup, login), along with tutorials are located within `Assets>Scenes>Menus>Menu Scenes`. The scripts for these scenes are located within `Assets>Scenes>Menus>Menu Scripts`.

## Other Scripts
Character and Camera control scripts are located within `Assets>Scripts>Character Scripts` and `Assets>Scripts>Camera Scripts` respectively. These are primarily utilised within Level 3.

## References
All resources are referenced in this file located at `Assets>Scripts>Resources`. This includes references of all images, assets and packages used and imported in Fact and Fantasy.
