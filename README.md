# How to setup Map Generator
---------------------------------------

![examples](https://github.com/zamecki/MapGenerator/blob/master/Documentation~/images/examples.gif)

#### Download

##### Setup
Download or clone this repository into your project in the folder Packages/com.l.m.z.mapgenerator.

Package Manager Manifest
Requirements
Git must be installed and added to your path.

Setup
The following line needs to be added to your Packages/manifest.json file in your Unity Project under the dependencies section:

    "com.l.m.z.mapgenerator": "1.0.0"


#### Creating Map Generator base game objects
---------------------------------------
In the **Hyerarchy** window do the following: _right click > 2D Object > Map Generator_

>This will create the **MapGenerator** gameobject with all the necessary scripts
>Will also create the **GridPreview**
>A **TilemapPreview** object and a **Quad** object will be children from the GridPreview
>Everything under the TilemapPreview will be disbled in Play mode

![Map Generator Path](https://github.com/zamecki/MapGenerator/blob/master/Documentation~/images/MapGeneratorPath.png)


#### Creating Map Generator Data
---------------------------------------
In the **Project** window in your desired folder _right click > Create > Map Generator Settings_
>This will create the **Map Generator Settings** gameobject with all the necessary scripts

![Map Generator Settings Path](https://github.com/zamecki/MapGenerator/blob/master/Documentation~/images/MapGeneratorDataPath.png)

Leave the **Seed** at 0 and to be a random map at every time.

#### Tile Settings Section

Select the just created **Map Generator Settings** now in the **Inspector** window locate the **Tile Settings** section
the **Tile Heights** holds 3 variables:
* Name
* Color code
* Tile base

The **Color code** is the color that will be used in the Gradient selector
The **Gradient** select will dictates which tile will be placed accordingly to the noise data
**Be sure that the ALPHA of the colors macth.**

![Map Generator Settings](https://github.com/zamecki/MapGenerator/blob/master/Documentation~/images/DataSettings.png)
![Gradient Example](https://github.com/zamecki/MapGenerator/blob/master/Documentation~/images/GradientExample.gif)

The **Tile Base** holds the tile, its possible to use custom tiles from [Unity 2D extra package](https://github.com/Unity-Technologies/2d-extras)

In the example i'm using the Terrain tile for the water and land, and random tiles for the sand.

---------------------------------------
Special thanks to [Sebastian Lague](https://github.com/SebLague) and [The Coding Train](https://www.youtube.com/user/shiffman) :v: