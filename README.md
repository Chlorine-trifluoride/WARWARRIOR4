<h1 align="center">
	<img src="https://github.com/Chlorine-trifluoride/WARWARRIOR4/raw/master/.github/media/icon.png" width="128"/>
	<br/>
	WARWARRIOR 4
</h1>

### Info

Warwarrior is a space physics game.
<br/>

<img src="https://github.com/Chlorine-trifluoride/WARWARRIOR4/raw/master/.github/media/wsmall.gif"/>


This project is a showcase of a leaderboard API.
<br/>

<img src="https://github.com/Chlorine-trifluoride/WARWARRIOR4/raw/master/.github/media/menu.gif"/>

### WarwarriorGame Windows Build

- Requirements
	- .NET Core 3.1 SDK
	- SDL2 dlls are included for Windows x64

Open Warwarrior.csproj in Visual Studio or from command line:
```
cd WarwarriorGame
dotnet run --configuration Release
```

### WarwarriorGame Linux/Unix Build

- Requires .NET Core 3.1 SDK

WARWARRIOR depends on three external libraries: libSDL2, libSDL2_image and libSDL2_ttf.
Install them using your package manager.

```bash
apt install libsdl2-2.0-0 libsdl2-image-2.0-0 libsdl2-ttf-2.0-0
```

Build and run the program with the following command:
```bash
cd WarwarriorGame
dotnet run --configuration Release
```

The post build script tries to add symlink to the libs. If it fails do it manually.

```bash
ln -s /path/to/lib /path/to/build/bin/lib
```

### API Usage

Rename LeaderboardAPI/Utils/DBConnectionHelper _RENAME.cs to **DBConnectionHelper.cs**
Open the file. Rename the class to DBConnectionHelper.
Adjust the settings to your PostgreSQL connection settings.

Build and run the API on a port of your choosing.

Open WarwarriorGame/Network/Leaderboard.cs
Edit ```const string API_PATH``` to point to your API.

## WarwarriorGame Usage

Warwarrior is a space physics game. All objects with mass interact with each other via gravity.
Collect blue shield particles to increase your shield.
Avoid getting shot, or shooting yourself around a stellar body :)

Your score is only sent to the server if you die. Not if you quit or respawn.

### Keys

| Action                         | Key                           |
| ------------------------------ | ----------------------------- |
| Accelerate / Brake		 | W / S			 |
| Turn Left / Right		 | A / D			 |
| Fire                           | Spacebar                      |
| R                              | Respawn                       |
| Exit to menu			 | Escape			 |
| Turn Engine On / Off		 | Z / X			 |
