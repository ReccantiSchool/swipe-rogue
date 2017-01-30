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
    <a href="#5.1">5.1 - Style</a>
    <a href="#5.2">5.2 - Art</a>
<a href="#6">6 - Sounds/Music</a>
    <a href="#6.1">6.1 - Style</a>
    <a href="#6.2">6.2 - Music</a>
    <a href="#6.3">6.3 - Effects</a>
<a href="#7">7 - Team</a>
	<a href="#7.1">7.1 - Members</a>
<a href="#8">8 - Additional</a>
</pre>

<div id="1"></div>
## Game Design

<div id="1.1"></div>
### Summary

Swipe Rogue is a simple game in which the player navigates a randomly-generated dungeon. The player fights enemies and collects powerups in an attempt to get as far down the dungeon as they can.


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

<div id="3"></div>
## Level Design

<div id="3.1"></div>
### Levels/Themes

- **Maze:** The Maze is the only type of level in the game and has a similar look for each floor. 

<div id="3.2"></div>
### Flow

Enemies in the maze will scale in proportion to the floor. The user will have to be mindful of the powerups they collect on each level in order to stand a chance against later enemies

<div id="4"></div>
## Development

<div id="4.1"></div>
### Code

- 

<div id="5"></div>
## Graphics

<div id="5.1"></div>
### Style

- **Simple**

<div id="5.2"></div>
### Art

- 

<div id="6"></div>
## Sounds/Music

<div id="6.1"></div>
### Style

- The music in this game will either be very simple, or there will not be any. A lot of mobile games are played silently.

<div id="6.2"></div>
### Music

- 

<div id="6.3"></div>
### Effects

- There will be simple sound effects for basic actions, like **attacking, picking up an item, or moving to another floor**

<div id="7"></div>
## Team

<div id="7.1"></div>
### Members

- 

<div id="8"></div>
## Additional