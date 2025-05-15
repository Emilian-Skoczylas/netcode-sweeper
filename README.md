# Minesweeper
Welcome to Netcode Sweeper project - my implementation of the classic game Minesweeper built in Unity.

I decided to refresh the graphic layer gently and went for a cyberpunk style.

# Demo
You can play play the game [here](https://emicode.site).

# Quick Look
![image](https://github.com/user-attachments/assets/5d98a9dd-e543-4193-89a2-aa115592d296)
![image](https://github.com/user-attachments/assets/6d9edcb0-1cd2-46d5-adb8-92cdd9b66fde)

# Gameplay overview
1. Select difficulty level
   * Begginer - 8x8 board, 10 mines, risk of mine: 15,625%
   * Advanced - 16x16 board, 40 mines, risk of mine: 15,625%
   * Expert - 30x16 board, 99 mines, risk of mine: 20,625%
     
2. Board is revealed and when you click on any cell - the game begins. The numbers represent how many mines are adjactent to given cell.
   
4. At the top of UI you can see:
   * How many mines are left (according to the flags placed)
   * Restart button
   * Stopwatch
     
5. To reveal a cell you click with the left mouse button and to flag with the right mouse button
   
7. At any time you can end the game by clicking the icon at the bottom left, return to the main menu and select another mode or mute the music

# Win/Lose Condition
  * When you reveal a cell under which there is a mine - you immediately lose
  * You win by revealing all the cells under which there were no mines

# Technologies Used
* **Unity 6**: for all game logic
* **Photoshop** to create tiles and board graphic

In addition, I used free assets for the background and music.

# Future Steps
The game itself is interesting and simple - that's the point of it, so it's prone to modifications that have no limit. My plan is:
  * Added soft SFX
  * Ability to play on your own boards according to the entered width, heigth and number of mines
  * Almost impossible to win mode - for the biggest freaks
  * Hint system that can be used a limited number of times depending on the difficulty

# Thank you for your time, I hope you enjoy the game!
