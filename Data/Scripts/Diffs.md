
This file is just a list of every folder, and how its copied scripts differ from their normal counterparts. If a file isn't listed, it can be assumed that it's the same. Original scripts aren't listed, of course.

(This is a temporary list, shouldn't be around for too long hopefully. I'm only keeping this for now since cleanup is still actively being updated, meaning I gotta bring over changes as well.)

# Sonic 2 Scripts

## CNZ
- Bumper: Changed to add SYZ sprites

## CPZ
- Water: Enforce Knuckles jump height when starting underwater (I don't think we really need to do this though, since there aren't any checkpoints in the stage)

## HPZ
- BreakWall: Changed to allow Knuckles (stage.playerListPos == 0) to break walls
- HPZCutSceneSetup: Standard HPZ Setup, but with most unrelated parts removed and cutscene control added in instead

## Players
- Player Object: 
  - Change PLAYER_\* aliases, for Knuckles and Tikal
  - Add in ANI_VICTORY (39),
  - Add in PlayerObject_Initial[X/Y] statics
  - Remove Sonic reloading animation his animation file upon detransforming (since he's not Sonic anymore, he's Knuckles)
  - Pause code changes for custom pause menu, including blocking pausing while there's a Title Card
  - Remove transformation code
  - Add in `player.yvel += 0x20000 // missing from original` line in glide code startup
  - Allow Knuckles to jump/look up/look down during the slide glide
  - Reset player.score when starting a level
- Holding off on editing the other 2 player scripts at all until cleanup is finalised...

## Global
- Chaos Emerald, End Point, Fall Start, and Water Mover are custom objects, they don't need to be "updated" with cleanup or anything
- Act Finish: Final Menu
- Death Event: Remove unnecessary Origins code, increment `death.count`
- Debug Mode: A single notable change, just don't have the `Tails Object` spawn when Tikal should instead
- Title Card: Remove some unnecessary stuff, and make it draw Green rather than Blue

# Sonic 1 Scripts

Most of these are just changing loop points in the stage's setup script, same note for all of them. The "workaround" described is declaring a public loop point alias in Stage Setup, before the individual stage's loop alias is declared, so that the new one has higher priority and will take effect

## GHZ
- GHZ Setup: Changed for inv music loop points (not necessarily needed, with workaround in Stage Setup)
- Break Wall: Changed to allow Knuckles (stage.playerListPos == 0) to break walls

## MZ
- MZ Setup: Changed for inv music loop points (not necessarily needed, with workaround in Stage Setup)

## LZ
- LZ Setup: Changed for inv music loop points (not necessarily needed, with workaround in Stage Setup)
- Water: Enforce Knuckles jump height when starting underwater (I don't think we really need to do this though, since there aren't any checkpoints in the stage)

## SBZ
- SBZ Setup: Changed for inv music loop points (not necessarily needed, with workaround in Stage Setup)

- Minor extension: when copying over S1 SonLVL-RSDK defs, make sure to wipe Eggmobile (sheet doesn't exist in this game)

## SLZ
- SLZ Setup: Changed for inv music loop points (not necessarily needed, with workaround in Stage Setup)
- Break Wall: Changed to allow Knuckles (stage.playerListPos == 0) to break walls

## SYZ
- SYZ Setup: Changed for inv music loop points (not necessarily needed, with workaround in Stage Setup)

# Shared

## Enemies
- Added in a Shortcut to the Scripts folder, for ease of use
- Bomb: Make it leave behind an Emerald when it blows up
- (a few other special cases too, i think grabber \[oh yeah we need to fix it warping you during act results too] and a couple others i believe?)
- For just about the rest of them, the only change is allowing them to bear Emeralds


# Sonic CD Scripts

## Special

- uhhh it's kinda broken at the moment anyways so like 

