# Piously
Welcome to the official Piously GitHub repository. Here you can download the source code to Piously, contribute to fixing bugs/making improvements, and track game development progress. This game is being developed using [osu-framework.](https://github.com/ppy/osu-framework)

![Piously logo](https://github.com/harshavb/piously/blob/master/Piously.Game/Resources/Textures/Menu/logo_filled.png)

# Click the Hexagons!
Piously is a two-player strategy board game created by [Jonah Ostroff](https://math.washington.edu/people/jonah-ostroff), a math professor at the University of Washington. The two teams are white and black and they will try to convert tiles on the board to their color. The board is generated each game from seven large pieces, and the objective of the game for the two players is to connect tiles of their color to every piece of the board in a continuous line.

### Before the Game:
Black gets to build the board consisting of the 7 4-hexagon tiles, where the board as a whole must be connected, and each of the 7 pieces must touch at least two other pieces.
After black builds the board, white may place their piece on any of the 28 hexagons, after which black may place their piece on any remaining space.

### Turn Structure:
1. White gets to move first. During a player's turn, they start with a base of 3 total actions. Actions are used when:
  * Moving to an adjacent unoccupied hexagon, i.e. a hexagon with no player character/artworks (1 action)
  * Blessing (making a tile your color) the unblessed tile underneath your character (1 action)
  * Blessing the blessed tile underneath your character to flip it to your own color (2 actions)
  * Placing/removing artworks on adjacent tiles corresponding to the spells you own currently (1 action each)
  * The player need not use all of their actions in a turn, but can never reach a negative number of remaining actions.
2. Spells. Hey, these things don't cost actions! Kinda neat. Each spell you have may be cast once per turn, after which it must recharge for the next turn. These can be cast at any point within your turn, even at 0 actions remaining. Neither player has any spells to begin with; in order to obtain spells, a player must end their turn in one of the 7 4-hexagon tiles, at which point they may choose one of two spells from that tile; either its artwork or its bewitchment. They may also choose not to take a spell.
3. Bewitchments have essentially no restrictions on when/where they may be cast, but are generally less powerful. Artworks, however, come with their own object, which must be placed on an aura of your own color to be cast at all. Artworks are also restricted by their corresponding linked region; this consists of all tiles connected to the tile the artwork is on by a single connected path of auras of your own color.

### Spell Effects:
* "P" piece
  - Artwork: The Priestess. When cast, you may extend the linked region by a single aura in any hexagon adjacent to the linked region.
  - Bewitchment: Purify. You may bless the tile under an adjacent object (the other player piece or an artwork) without using an action.
* "I" piece
  - Artwork: The Illusionist. When cast, you may choose any collection of rooms containing at least one tile of the linked region. In each of these rooms, you must then flip all auras to the opposite color, including your own.
  - Bewitchment: Imprint. When cast, copy the aura pattern initially beneath the opponent's and their adjacent tiles to the corresponding area under your own piece, preserving orientation. Empty tiles are also copied, but adjacent spaces without a tile do nothing.
* "O" piece
  - Artwork: The Opportunist. When cast, you may choose one room containing a tile of the linked region. You may then recharge the spell you own corresponding to this room, allowing you to cast it once again. This cannot be used to build up extra charges on a spell.
  - Bewitchment: Overwork. Gain an additional action for each adjacent artwork or player piece (not including yourself) on top of the 3 base actions you have each turn.
* "U" piece
  - Artwork: The Usurper. When cast, you must shrink the linked region by two auras by removing them from the board, recalculating the linked region between the first and second aura. Then, you may extend the linked region twice as in the case of the Priestess.
  - Bewitchment: Upset. You may rearrange the auras beneath yourself and on adjacent tiles however you please without flipping them.
* "S" piece
  - Artwork: The Stonemason. When cast, you may choose one room containing a tile of the linked region. Then, you may move the entire tile wherever you wish, so long as the rules for boardmaking as previously described are still satisfied.
  - Bewitchment: Shovel. Summon the singleton tile to any adjacent space without a hexagon, so long as you are not standing on the singleton. Any auras and objects previously on the singleton stay on it. The singleton does not count for board connectedness or piece adjacency when using Stonemason. Casting Shovel while on the singleton has no effect.
* "L" piece
  - Artwork: The Locksmith. When cast, choose an object on a tile in the linked region (this may be the Locksmith artwork itself). Then, move this object to any unoccupied space on the board.
  - Bewitchment: Leap. Swap places with any object in your row, where a row consists of a sequence of hexagonal tiles connected by opposite edges. Leap cannot swap with objects if the row between them has a space without a hexagon.
* "Y" piece
  - Artwork: The Yeoman. When cast, you may choose any number of rooms containing a tile of the linked region. For each, you may move all objects inside the room itself to any tiles in the same room.
  - Bewitchment: Yoke. Choose any object besides your own piece on the board. Then, move your piece and your chosen object in the same direction by one tile. Both the object and your player piece must end up on a space with a hexagon.
  
# Development
This game is being developed with [osu-framework,](https://github.com/ppy/osu-framework) the C# framework used to create the popular rhythm game [osu!](https://osu.ppy.sh). The project is available as a Visual Studio solution in the repository. Simply clone it into a directory and open the **piously.sln** file on the root directory.

To contribute to the project, please see the [issues](https://github.com/harshavb/piously/issues) for a list of bugs/enhancements we are trying to add to the game. If you see an unassigned issue to which you think you can contribute, feel free to make a pull request with your changes! If you think there is a feature that the game is missing that is not marked as an issue, you are also free to make a PR, but please explain what and why you are propsing this change be made.
# Piously Website
Since Piously is going to be an online game with ranked matches and user profiles, a web-facing application will also be created during the later stages of development. We are currently not looking for any developers for the web project.
