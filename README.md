# Borderlands GOTY Enhanced Trainer (STEAM EDITION)
Simple Trainer. Highly Customizable source , currently configured to READ memory and allows the user to change some stuff as proof of concept

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

HOTKEYS
- NUMPAD 0 => Refill Health , Shields & Ammo
- NUMPAD 1 => NO RECOIL (Toggle ON / OFF)

## Minor Updates
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
![image](https://user-images.githubusercontent.com/80198020/121113312-d16abd80-c7df-11eb-93a0-17420e3544a5.png)
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
