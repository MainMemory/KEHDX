
This file is just a list of every folder, and how its copied scripts differ from their normal counterparts. If a file isn't listed, it can be assumed that it's the same. Original scripts aren't listed, of course.

(This is a temporary list, shouldn't be around for too long hopefully. I'm only keeping this for now since cleanup is still actively being updated, meaning I gotta bring over changes as well.)

# Sonic 2 Scripts

## CNZ
- Bumper: Changed to add SYZ sprites

## CPZ
- Water: Enforce Knuckles jump height when starting underwater (I don't think we really need to do this though, since there aren't any checkpoints in the stage)

## HPZ
- HPZCutSceneSetup: Standard HPZ Setup, but with most unrelated parts removed and cutscene control added in instead


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
- (a few other special cases too, i think grabber and a couple others i believe?)
- For just about the rest of them, the only change is allowing them to bear Emeralds


# Sonic CD Scripts

## Special

- uhhh it's kinda broken at the moment anyways so like 

