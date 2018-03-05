# (!) Elite Dangerous has drastically changed, so this tool won't work for now, it requires massive re-work at a moment.

# Elite Dangerous Mission Reporter

Mission reporting tool for Elite Dangerous game.
Uses mobile API with OCR to get information about mission name and start/end locations.
Data can be exported anyhwere you like, for example to http://en.ed-board.net/.  
[Check an example export script for that.](https://gist.github.com/jarig/376368fc5093f841a612)


If it doesn't work well try taking screenshot from ingame while in bulletin board and cut the year digits from left top corner, 
and replace Assets/3302_eng.bmp (or 3302_rus.bmp) with it.
Example: ![Alt text](/EliteReporter/Assets/3302_eng.bmp "3302")

## How to use

1. Open application and pass login process (it logins to Frontier Mobile API, *niether of your username nor password are stored/cached anywhere!* cached only cookies.)
2. Set your game resolution dimensions in upper right corner
3. Open the game
4. Once in bulletin board and entered to Mission acceptance screen(the one with Decline/Accept buttons) - press F10 (take screenshot)
5. When mission(s) is going to be finished open mission accomplishment screen (the one with buttons Give Cargo, etc) and press F10.
6. You can export results using Export button when quited the game(exported in json format).
   * Optionally you can go to Application->Settings and setup any program/script execution that can read provided json and publish results anywhere you want.

#### Tips
* Double click on an item to change any attributes you like.
* You can add missing mission types yourself, just place .bmp files with cropped images under Assets/mTypes folder(you'll see examples there). Filename you give would be mission type name.


### How it looks like

![Alt text](/EliteReporter/Resources/AppPreview.png "Elite Reporter")


### Configure Exporting yourself using scripts you like!

![Alt text](/EliteReporter/Resources/ExportPreview.png "Export")


### <a name="edBoardExportExample"></a>Example export script for ed-board

[Gist: Elite Reporter Export settings and script for EDBoard](https://gist.github.com/jarig/376368fc5093f841a612)

How it looks like on EDBoard:
![Alt text](/EliteReporter/Resources/EdboardExample.png "EDBoard Log")
