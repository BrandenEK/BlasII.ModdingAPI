# BlasII Modding API

To develop a mod for Blasphemous 2, run these commands to create a new template project:

```dotnet new install Blasphemous.Modding.Templates```
```dotnet new blas2mod -n ProjectName -m ModName -au AuthorName -ve GameVersion```

For example, to create a mod that adds the Boots of Pleading item, I would run the command:

```dotnet new blas2mod -n BlasII.BootsOfPleading -m "Boots of Pleading" -au Damocles -ve 1.0.5```