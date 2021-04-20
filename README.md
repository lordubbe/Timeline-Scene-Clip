# Timeline-Scene-Clip
Provides the ability to dynamically load/unload scenes with Timeline using the Scene Clip Track. You can also just snatch the Scene Name Picker, which is a handy attribute to slap on a string, allowing you to pick a scene name so you don't accidentally misspell (or try to load a scene that is not in the build settings)!

This was created and tested using Unity 2021.1.2f1, so if something changes dramatically with the Timeline system or how custom playables work this might become obsolete until updated!

You are free to do what you want with this, but I take no responsibility if anything breaks (it shouldn't but anyway). 
If you do end up using either the Scene Clips or the Scene Name Picker, let me know what you think, and if you have any suggestions! 

## Usage
Add a Scene Clip Track to your main timeline and make sure to set the SceneClipTrack.DefaultScene to be the scene in which the playable director which plays your primary timeline (so that it remains loaded). Remember that scenes you wanna control should be in the build settings (they won't show up in the Scene Name Picker if they're not there)
Then add clips as you need! You should not overlap/crossfade clips, but they can be right up against eachother.

## Limitations
Multiple Scene Clip Tracks are as of writing not supported, and might result in strange or unwanted behaviour. This also means that this system works for the usage scenario where there's a certain scene (with the timeline) that you want to keep open, and then dynamically load or change a second scene as the timeline progresses.
I do want to add support for several tracks (and thus multiple extra scenes), so [poke me](https://twitter.com/lordubbe) if you need this functionality and it might motivate me and expedite the process!
The Scene Name Picker is just a super handy attribute
