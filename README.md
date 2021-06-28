# Borderlands GOTY Enhanced Trainer (STEAM EDITION)
Simple Trainer. Highly Customizable source , currently configured to READ memory and allows the user to change some stuff as proof of concept

For a full list of offsets , scroll to the bottom

6.25.2021 
THIS IS NO LONGER BEING UPDATED

## HOW TO USE
- Download the source code
- Unzip contents to folder somewhere
- Open "Borderlands GOTY Enhanced Trainer.sln"
- Change the code to your liking or build it as is. (You will need to restore packages via nuget)
- Start Borderlands and load into the game
- Run "Borderlands GOTY Enhanced Trainer.exe"

# Features
- No Recoil Mod
- Adjust Level
- Adjust Money
- Adjust Golden Keys
- Adjust Skill Points
- XP Multiplier
- Infinite Health and Shields Toggle
- Infinite Everything Toggle ... (Besides Vehicle Boost and skill time)

# HOTKEYS
- NUMPAD 0: REFILL Health, Shields & Ammo
- NUMPAD 1: NO RECOIL
- NUMPAD 2: INFINITE Health, Shields & Skill Time 
- NUMPAD 3: INFINITE Ammo
- NUMPAD 4: INFINITE Money
- NUMPAD 5: INFINITE Golden Keys

## Minor Updates
v2.0 (6.25.21) FINAL!! NEW APP ---> link 
- Added Hotkey Toggles
- NUMPAD 1 - 5 indicators on form
* check hotkeys for more info

v1.0.3 (6.10.21)
- No Recoil Toggle
- Adjust XP Multiplier 
- Adjust Skill Points
- Bug Fixes

v1.0.2 (6.9.21)
- Infinite Everything button now includes ammo
- Included No Recoil Hotkeys for ON and OFF (NUMPAD 1 & 2)
- Heavily commented throughout the code to help users navigate

v1.0.1 (6.8.21)
- Added more offsets, Make sure to check out the Offsets Class!
- Ammo for most weapon types now refill when pressing NUMPAD 0 (Health and Shields still refill as well)
- Unlimited Health toggle now includes Unlimited Shields and Ammo for most weapon types

## MEDIA
![image](https://user-images.githubusercontent.com/80198020/121612413-5d6f2600-ca28-11eb-8552-644522e91679.png)
![image](https://user-images.githubusercontent.com/80198020/121112725-cd8a6b80-c7de-11eb-9a9f-21d76c0c4dfa.png)
![image](https://user-images.githubusercontent.com/80198020/121112768-e266ff00-c7de-11eb-949f-7053271488f3.png)

## Bugs
Currently Known Bugs
```
Bug: Golden Keys
Description: Key Amount not matching in game amount
Possible Reason: I think this simply because the value being sent is updating to what is sent instead of adding to the previous value.
Possible Fix: Read the value of `GoldenKeyUsed`. When sending keys from trainer , add from Total Keys Used. 
> 0 Keys
> 10 Keys Used
> Adding 11 Keys will give 1 key
```
- If you encounter any bugs, open an issue
- You may also send me suggestions

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## Offsets
```
//Player Info
Money = "0x025C1D90,0x68,0x550,0x1F8,0x350";                        		//Int
GoldenKeyTotal = "0x025C1D90,0x68,0x550,0x1B8,0x1070,0x64,0x300";   		//Int
GoldenKeyUsed = "0x025C1D90,0x68,0x550,0x1B8,0x1070,0x64,0x31C";    		//Int
SkillPoints = "0x025C1D90,0x68,0x550,0x1F8,0x348";                  		//Int
SkillCooldown = "0x025C1D90,0x68,0x550,0x1F8,0x36C,0x270,0x98";     		//Float
Level = "0x025C1D90,0x68,0x550,0x1F8,0x32C";                        		//Int
Experience = "0x025C1D90,0x68,0x550,0x1F8,0x358,0x268,0x98";        		//Float
Health = "0x025C1D90,0x68,0x550,0x1F8,0x364,0x98";                  		//Float
MaxHealth = "0x025C1D90,0x68,0x550,0x1F8,0x364,0x80";               		//Float
Shield = "0x025C1D90,0x68,0x550,0x390,0x290,0x98";                  		//Float
MaxShield = "0x025C1D90,0x68,0x550,0x390,0x290,0x80";               		//Float
SkillCooldownTimerMax = "0x025C1D90,0x68,0x550,0x1F8,0x36C,0x270,0x80"; 	//Float
XPMultiplier = "0x025C1D90,0x68,0x550,0x1F8,0x358,0x268,0x194"; 			//Float
MaxXP = "0x025C1D90,0x68,0x550,0x1F8,0x358,0x268,0x80";						//Float

//Player Position
CameraPitch = "0x02555AA8,0xC4,0x630,0x10,0x9C"; 		//Int
CameraYaw = "0x02555AA8,0xC4,0x630,0x10,0xA0"; 			//Int
PlayerX = "0x025C1DA0,0x18,0x40,0x90"; 					//Float
PlayerY = "0x025C1DA0,0x18,0x40,0x94"; 					//Float
PlayerZ = "0x025C1DA0,0x18,0x40,0x98"; 					//Float
DeltaX = "0x02555C38,0x124,0x0,0x17C"; 					//Float
DeltaY = "0x02555C38,0x124,0x0,0x180"; 					//Float
DeltaZ = "0x02555C38,0x124,0x0,0x184"; 					//Float
MovementSpeed = "0x02555C38,0x124,0x0,0x37C"; 			//Float
BaseMovementSpeed = "0x02555C38,0x124,0x0,0x380"; 		//Float
isSprinting? (BOOL) = "0x02555C38,0x124,0x0,0x38C"; 	//Int
JumpHeight = "0x02555C38,0x124,0x0,0x3B8"; 				//Float
BaseJumpHeight = "0x02555C38,0x124,0x0,0x3BC"; 			//Float
ViewHeight = "0x02555C38,0x124,0x0,0x424"; 				//Float

//Backpack
BackpackItemsMax = "0x02543508,0x8,0x20,0x1C,0x288"		//Int
WeaponDecks = "0x02543508,0x8,0x20,0x1C,0x28C"			//Int
BackpackItems = "0x02543508,0x8,0x20,0x1C,0x2B4"		//Int

//Ammo
RevolverAmmo = "0x02542680,0x98"						//Float
RevolverAmmoMax = "0x02542680,0x80"						//Float
SMGAmmo = "0x02542678,0x98"								//Float
SMGAmmoMax = "0x02542678,0x80"							//Float
CarbineAmmo = "0x02542690,0x98";						//Float
CarbineAmmoMax = "0x02542690,0x80"						//Float
ShotgunShells = "0x02542670,0x98"						//Float
ShotgunShellsMax = "0x02542670,0x80"					//Float
SniperRifleAmmo = "0x02542668,0x98"						//Float
SniperRifleAmmoMax = "0x025426680x80"					//Float
Grenades = "0x025C1D90,0x50,0x280,0x98";				//Float
GrenadesMax = "0x025C1D90,0x50,0x280,0x80";				//Float
RepeaterPistol Ammo = "0x025C1D90,0x50,0x2A0,0x98";		//Float
RepeaterPistol Max = "0x025C1D90,0x50,0x2A0,0x80";		//Float
LauncherAmmo = "0x02542660,0x98";						//Float
LauncherAmmoMax = "0x02542660,0x80";					//Float

//Weapon Proficiencies
/XP ("BorderlandsGOTY.exe"+025C4DA8,6D0,10,***) or ("BorderlandsGOTY.exe"+025C4DA8,630,10,***) || Note
PistolLVL = 
PistolCurrentXP = 0x025C4DA8,0x6D0,0x10,0xD84		//Int
PistolRequiredXP = 0x025C4DA8,0x6D0,0x10,0xD88		//Int
SMGLVL = 
SMGCurrentXP = 0x025C4DA8,0x6D0,0x10,0xDA4			//Int
SMGRequiredXP = 0x025C4DA8,0x6D0,0x10,0xDA8			//Int
ShotgunLVL = ???
ShotgunCurrentXP = 0x025C4DA8,0x6D0,0x10,0xD94		//Int
ShotgunRequiredXP = 0x025C4DA8,0x6D0,0x10,0xD98		//Int
CarbineLVL = ???
CarbineCurrentXP = 0x025C4DA8,0x6D0,0x10,0xDB4		//Int
CarbineRequiredXP = 0x025C4DA8,0x6D0,0x10,0xDB8		//Int
SniperLVL = ???
SniperCurrentXP = 0x025C4DA8,0x6D0,0x10,0xDC4		//Int
SniperRequiredXP =  0x025C4DA8,0x6D0,0x10,0xDC8		//Int
LauncherLVL = ???
LauncherCurrentXP = 0x025C4DA8,0x6D0,0x10,0xDD4		//Int
LauncherRequiredXP = 0x025C4DA8,0x6D0,0x10,0xDD8	//Int
ElementalLVL = ???
ElementalCurrentXP = 0x025C4DA8,0x6D0,0x10,0xDE4	//Int
ElementalRequiredXP = 0x025C4DA8,0x6D0,0x10,0xDE8	//Int
```
