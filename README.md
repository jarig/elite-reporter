# Elite Dangerous Mission Reporter

Mission reporting tool for Elite Dangerous game.
Uses mobile API with OCR to get information about mission name and start/end locations.

If it doesn't work well try taking screenshot from ingame while in bulletin board and cut the year digits from left top corner, 
and replace Assets/3302_eng.bmp (or 3302_rus.bmp) with it.

## How to use

1. Open application and pass login process (it logins to Frontier Mobile API, *niether of your username nor password are stored/cached anywhere!* cached only cookies.)
2. Set your game resolution dimensions in upper right corner
3. Open the game
4. Once in bulletin board and entered to Mission acceptance screen(the one with Decline/Accept buttons) - press F10 (take screenshot)
5. When mission(s) is going to be finished open mission accomplishment screen (the one with buttons Give Cargo, etc) and press F10.
6. You can export results using Export button when quited the game(exported in json format).
   * Optionally you can go to Application->Settings and setup any program/script execution that can read provided json and publish results anywhere you want.


### How it looks like

![Alt text](/EliteReporter/Resources/AppPreview.png "Elite Reporter")


### Configure Exporting yourself using scripts you like!

![Alt text](/EliteReporter/Resources/ExportPreview.png "Export")
