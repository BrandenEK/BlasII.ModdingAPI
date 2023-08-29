# BlasII Modding API

## How to use

First download the modding tools and follow the instructions here: https://github.com/BrandenEK/BlasII.ModdingTools.  Then download this modding API and any other mods you want to play with and extract them into the Modding folder.

---

## Development

To develop a mod for Blasphemous 2, run these commands to create a new template project:

```dotnet new install Blasphemous.Modding.Templates```

```dotnet new blas2mod -n ProjectName -m ModName -au AuthorName -ve GameVersion```

For example, to create a mod that adds the Boots of Pleading item, I would run the command:

```dotnet new blas2mod -n BlasII.BootsOfPleading -m "Boots of Pleading" -au Damocles -ve 1.0.5```