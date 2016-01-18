# Elite Dangerous Mission Reporter

Mission reporting tool for Elite Dangerous game.
Uses mobile API with OCR to get information about mission name and start/end locations.

If it doesn't work well try taking screenshot from ingame while in bulletin board and cut the year digits from left top corner, 
and replace Assets/3302.png with it.


## How to use

1. Open application and pass login process (it logins to Frontier Mobile API, *niether your username nor password are stored!* cached only cookie)
2. Set your game resolution dimensions in upper right corner
3. Open the game
4. Once in bulletin board and entered to Mission acceptance screen(the one with Decline/Accept buttons)
   * Press ALT+ENTER to exit from fullscreen mode(unfortunately current version do not support screencaputring from Direct3D)
   * Press CTRL+ALT+M to record mission start
   * Press ALT+ENTER to return to fullscreen mode
5. Once mission is going to be finished, again open the screen with mission accomplishment buttons(Give Cargo, etc)
   * Press ALT+ENTER to exit from fullscreen mode
   * Press CTRL+ALT+M to record mission end
   * Press ALT+ENTER to return to fullscreen mode
6. Once all missions are recorded and you quited the game you can export results using Export button
   * Optionally you can go to Application->Settings and setup any program/script execution that can read provided json and publish results anywhere you want.
