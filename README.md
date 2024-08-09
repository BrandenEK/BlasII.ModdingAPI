# Blasphemous 2 Modding API

<img src="https://img.shields.io/github/downloads/BrandenEK/BlasII.ModdingAPI/total?color=872124&style=for-the-badge">

---

## Features

- All registered mods should be displayed in the top left corner of the main menu
- Mod settings can be configured by modifying the files in the "Modding/config" folder
- Keyboard input can be configured by modifying the files in the "Modding/keybindings" folder
- Simplifies mod development by implementing standard functionality and practices

## Development

No documentation yet, but it should be pretty similar to the [blas1 version](https://github.com/BrandenEK/Blasphemous.ModdingAPI/blob/main/documentation/main.md)

To develop a mod for Blasphemous 2, run these commands to create a new template project:

```dotnet new install Blasphemous.Modding.Templates```

```dotnet new blas2mod -n ProjectName -m ModName -au AuthorName -ve GameVersion```

For example, to create a mod that adds the Boots of Pleading item, I would run the command:

```dotnet new blas2mod -n BlasII.BootsOfPleading -m "Boots of Pleading" -au Damocles -ve 1.0.5```

## Installation
This mod is available for download through the [Blasphemous Mod Installer](https://github.com/BrandenEK/Blasphemous.Modding.Installer)
- Required dependencies: None
