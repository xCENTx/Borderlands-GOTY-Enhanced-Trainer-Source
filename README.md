# Borderlands-GOTY-Enhanced-Trainer-Source-(STEAM EDITION)
Simple Trainer. Highly Customizable source , currently configured to READ memory and allows the user to change some stuff as proof of concept

# Features
- Adjust Level
- Adjust Money
- Adjust Golden Keys
- Infinite Health , Shields & Ammo Toggle
- Infinite Everything Toggle ... (I mean EVERYTHING (besides ammo ... and skill time))
- Press Numpad 0 to Refill Health , Shields & Ammo

# Installation
- Download the source code
- Open "Borderlands GOTY Enhanced Trainer.sln"
- Unzip contents to folder somewhere
- Change the code to your liking or build it as is. (You will need to restore packages via nuget)
- Start Borderlands and load into the game
- Run "Borderlands GOTY Enhanced Trainer.exe"

# Minor Changes 6.8.21
- Added more offsets, Make sure to check out the Offsets Class!
- Ammo for most weapon types now refill when pressing NUMPAD 0 (Health and Shields still refill as well)
- Unlimited Health toggle now includes Unlimited Shields and Ammo for most weapon types

# Images
![image](https://user-images.githubusercontent.com/80198020/121113312-d16abd80-c7df-11eb-93a0-17420e3544a5.png)
![image](https://user-images.githubusercontent.com/80198020/121112725-cd8a6b80-c7de-11eb-9a9f-21d76c0c4dfa.png)
![image](https://user-images.githubusercontent.com/80198020/121112768-e266ff00-c7de-11eb-949f-7053271488f3.png)



## Bugs
Currently Known Bugs
```
Bug: Golden Keys
Description: 
Possible Reason: I think this simply because the value being sent is updating to what is sent instead of adding to the previous value.
Possible Fix: Read the value of `GoldenKeyUsed`. When sending keys from trainer , add from Total Keys Used. 
0 Keys
10 Keys Used
Adding 11 Keys will give 1 key

```
- If you encounter any bugs, open an issue
- You may also send me suggestions

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

Forum Thread
https://www.unknowncheats.me/forum/other-fps-games/456151-borderlands-goty-enhanced-simple-trainer.html
