# Swipe Rogue Design Document

**Start Date:** February 1st 2017

## Table of Contents

<pre>
<a href="#1">1 - Game Design</a>
    <a href="#1.1">1.1 - Summary</a>
    <a href="#1.2">1.2 - Inspiration</a>
<a href="#2">2 - Technical</a>
    <a href="#2.1">2.1 - Scenes</a>
    <a href="#2.2">2.2 - Controls</a>
    <a href="#2.3">2.3 - Mechanics</a>
<a href="#3">3- Level Design</a>
    <a href="#3.1">3.1 - Levels/Themes</a>
    <a href="#3.2">3.2 - Flow</a>
<a href="#4">4 - Development</a>
    <a href="#4.1">4.1 - Code</a>
<a href="#5">5 - Graphics</a>
    <a href="#5.1">5.1 - Art</a>
    <a href="#5.2">5.2 - Assets</a>
<a href="#6">6 - Sounds/Music</a>
    <a href="#6.1">6.1 - Music</a>
    <a href="#6.2">6.2 - Effects</a>
<a href="#7">7 - Team</a>
	<a href="#7.1">7.1 - Members</a>
	<a href="#7.2">7.2 - Roles</a>
<a href="#8">8 - Stretch Goals</a>
<a href="#9">9 - Issues</a>
	<a href="#9.1">MoveTo Function in MovementManager throwing an error</a>

</pre>

<div id="1"></div>

## Game Design

<div id="1.1"></div>

### Summary

Swipe Rogue is a simple game in which the player navigates a randomly-generated dungeon. The player fights enemies and collects powerups in an attempt to get as far down the dungeon as they can.
- Light Hearted easy going game

<div id="1.2"></div>

### Inspiration

I really enjoy simple mobile games like Threes and Jetpack Joyride. These games have simple controls and are easy to pick up and play. I also really enjoy roguelike games and RPGs, since I like the idea of leveling up and going on an adventure. Swipe Rogue is an attempt to make a roguelike RPG that can easily be played on mobile


<div id="2"></div>

## Technical


<div id="2.1"></div>

### Scenes

- **Title Scene:** The title screen simply displays a button that will start the game when clicked.
- **Gameplay Scene:** The gameplay screen displays the roome the player is currently in, as well as their health and their strength.
- **Floor Transition Scene:** This is a simple fade-in, fade-out that alerts the user to the fact that they've changed rooms
- **Death Scene:** When the player is defeated, the Death Scene is displayed. It displays the player's stats and the level of the dungeon they reached.


<div id="2.2"></div>

### Controls

- **Swiping:** Swiping will move the player to the next room in the direction of the swipe, as long as there is an open doorway in that direction.
- **Tapping:** Tapping will allow the user to interact with an object in the room. There will only be one item in a room at a given time. This could be an enemy, or a powerup


<div id="2.3"></div>

### Mechanics

- **Moving Between Rooms:** The player can move to a room with an open door by swiping in the direction of that door
- **Picking up an powerup:** The player can pick up an powerup by tapping on it. This will automatically apply the powerup. Powerups include: **Health Restore, Health Increase, and Strength Increase**
- **Combat:** The enemies have attack timers and will launch an unavoidable attack when the timer runs out. The player must swipe repeatedly at the enemies to kill them before their attacks kill the player. 
- **Timer:** Each floor has a timer, if the player doesn't leave the floor before the timer runs out then the player dies. 

<div id="3"></div>

## Level Design

<div id="3.1"></div>

### Levels/Themes

- **Maze:** The Maze is floor of randomly-generated rooms within the level.  
- **Puzzles:** Each room will have some sort of small effect or challenge to keep the player engaged in the action of the game. 
- **Healing Rooms:** There will be a room that has a possibility of appearing on each floor. This room contains the fountain of youth and drinking from it restores the players health. However the spring can only be used once per floor. 
- **Treasure Rooms:** Rooms containing chests that have gold in them. This loot can be used to purchase more characters or weapons

<div id="3.2"></div>

### Flow

Enemies in the maze will scale in proportion to the floor. The user will have to be mindful of the powerups they collect on each level in order to stand a chance against later enemies

<div id="4"></div>

## Development

<div id="4.1"></div>

### Process
Our Development Process Started with a paper prototype. We had the player pick up a room card and a room effect card. The room cards had specific paths and the room effects varied from adding/removing time to having to play a quick game. 
We found that the variation in the rooms and the timer made for fun gameplay.

<div id="4.2"></div>

### Code

-**Unity 5:** Our game is being produced in the Unity game engine for mobile. 
-**Code Structure:** Our code follows a hierachical structure. Most sub-classes will be inhieriting from 3 Parent classes: Item (anything the player can interact with), Character (Any character in the game, player or otheriwse) and Room(different room types).

<div id="5"></div>

## Graphics

<div id="5.1"></div>

### Art

- 2D pixel art
- Aztec/Mayan inspired. 
- 16th century Conquistadors

<div id="5.2"></div>

## Assets

- Door (Open and Locked) x 8
- Rooms x 3 (Walls and floors)
- Player 
- Monster 
- Treasure Box
- Spring
- Gold
- Weapons x 3
- Stairs

<div id="6"></div>

## Sounds/Music

<div id="6.1"></div>

### Music

- Latin American/Spanish 
- Flamenco Music


<div id="6.2"></div>

### Effects

- There will be simple sound effects for basic actions, like **attacking, picking up an item, or moving to another floor**


<div id="7"></div>

## Team

Our Team is made up of three people. While our group roles on paper are clearly defined, we tend to overlap the roles often.

<div id="7.1"></div>

### Members

- Zachary Wilken
- Sharlene Mendez
- Ben Wilcox

<div id="7.2"></div>

### Roles

- **Creative Director:** **Ben Wilcox**
- **Game Designer:** **Zachary Wilken**, Sharlene Mendez
- **Producer:** **Sharlene Mendez**
- **Game Programmer:** **Ben Wilcox**, Zachary Wilken
- **Audio Engineer:** **Sharlene Mendez**
- **Art/Animation:** **Sharlene Mendez**

<div id="8"></div>

## Stretch Goals

- Different Playable Characters
- **Boss Floors:** Have a floor that only has one room and one monster. This monster would require you to swipe in different patterns to defeat it. 
- At the moment the plan is to use freely available creative commons sounds and music. However if we have time, we hope to create our own custom sounds and music for the game.

<div id="9"></div>

## Issues

<div id="9.1"></div>

### MoveTo Function in MovementManager throwing an error

This issue is caused by the Room prefab missing the required room script. To fix it, perform the following steps:

1. Navigate to the Room prefab under `Assets/Prefabs/Room` and click on it
2. If it says it's missing a script, click on the script tag in the Inspector and select the Room.cs script under `Assets/Scripts/Room`
3. Run the project and see if it works

